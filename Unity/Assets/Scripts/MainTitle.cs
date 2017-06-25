using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MainTitle : MonoBehaviour {

    [SerializeField]
    private GameObject[] caseNiveauxAffiches;

    [SerializeField]
    private LoadScenes loadScenesScript;

    [SerializeField]
    private GameObject[] mondesPanel;


    [Serializable]
    public class JSONLevels {
        public string code;
        public string title;
        public int score;
        public int hardness;
        public string img;
    }

    [Serializable]
    public class JSONListeNiveaux {
        public JSONLevels[] levels;
    }

    private int indiceListeNiveauxAffiches = 0;
    private JSONListeNiveaux listeNiveaux;

    void Start() {

        Utils.ChargerPartie();

    }

    void Update() {
        
    }

    void CollectiblesObtained()
    {

    }

    // Chargement d'une liste de niveaux

    public void AfficheListeNiveaux() {

        APIListeNiveaux(SystemInfo.deviceUniqueIdentifier);
    }

    public void AfficheListeNiveauxCommunaute() {

        APIListeNiveaux();
    }

    public void TelechargeNiveauDeLaListeAffichee(int i) {

        if (i + indiceListeNiveauxAffiches < listeNiveaux.levels.Length) {

            var level = listeNiveaux.levels[i + indiceListeNiveauxAffiches];
            var code = level.code;

            loadScenesScript.DownloadLevel(code);
        }


    }

    public void Liste5NiveauxSuivPrec(int suivPrec) {

        int decalage = suivPrec * caseNiveauxAffiches.Length; // -1 * 5 ou 1 * 5

        indiceListeNiveauxAffiches += decalage;

        if (indiceListeNiveauxAffiches < 0) indiceListeNiveauxAffiches = 0;
        if (indiceListeNiveauxAffiches > listeNiveaux.levels.Length - 5) indiceListeNiveauxAffiches = listeNiveaux.levels.Length - 5;

        Liste5Niveaux();
    }

    private void Liste5Niveaux() {

        for (int i = 0; i < caseNiveauxAffiches.Length; i++) {

            var caseNiveau = caseNiveauxAffiches[i];

            if (i + indiceListeNiveauxAffiches < listeNiveaux.levels.Length) {

                var level = listeNiveaux.levels[i + indiceListeNiveauxAffiches];

                caseNiveau.SetActive(true);

                var textes = caseNiveau.GetComponentsInChildren<Text>();

                foreach (var texte in textes) {

                    switch(texte.name) {

                        case "Title":
                            texte.text = level.title;
                            break;

                        case "Code":
                            texte.text = level.code;
                            break;

                        case "Score":
                            texte.text = ""+level.score;
                            break;

                        case "Hardness":
                            texte.text = ""+level.hardness;
                            break;

                    }

                    var images = caseNiveau.GetComponentsInChildren<Image>();
                    
                    if (images != null && images.Length > 0) {

                        foreach (var image in images) {
                            
                            if (image.name == "Min") {
                                
                                if (level.img != null && level.img != "") {
                                    
                                    //image.gameObject.SetActive(true);
                                    Texture2D texture = new Texture2D(1, 1);
                                    texture.LoadImage(Convert.FromBase64String(level.img));
                                    texture.Apply();
                                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
                                }
                                else {
                                    //image.gameObject.SetActive(false);
                                    Texture2D texture = new Texture2D(2, 2);
                                    texture.Apply();
                                    image.sprite = Sprite.Create(texture, new Rect(0, 0, 2, 2), new Vector2());
                                }
                            }
                        }
                    }

                }
            }
            else {

                caseNiveau.SetActive(false);
            }

        }
    }

    IEnumerator ListeDeNiveaux(WWW www) {

        yield return www;

        if (www.error == null) {
            
            string json = www.text;

            try {
                listeNiveaux = JsonUtility.FromJson<JSONListeNiveaux>(json);

                indiceListeNiveauxAffiches = 0;

                Liste5Niveaux();

            }
            catch (Exception e) {
                Debug.Log(e);
            }

        }
        else {
            Debug.Log("erreur web");
        }
    }

    void APIListeNiveaux(string mac = "") {

        string url;

        if (mac != null && mac != "") {
            url = Utils.LIEN_LISTE_NIVEAUX + "?mac=" + mac;
        }
        else {
            url = Utils.LIEN_LISTE_NIVEAUX;
        }

        Debug.Log(url);

        WWW www = new WWW(url);

        StartCoroutine(ListeDeNiveaux(www));
    }


    // Jouer

    public void DebloqueTout() {

        Utils.partie.DebloqueTousLesNiveaux();
        SelectionneMonde();
    }

    public void Reinitialise() {

        Utils.partie.Initialise();
        Utils.SauvegarderPartie();
        SelectionneMonde();
    }

    public void SelectionneMonde(int monde = -1) {

        if (monde == -1) monde = Utils.partie.mondeActuel;
        else monde--;

        int dernierMonde = Utils.partie.GetDernierMonde();

        if (dernierMonde >= monde && monde < mondesPanel.Length) {

            foreach (var mondePanel in mondesPanel) {
                mondePanel.SetActive(false);
            }

            mondesPanel[monde].SetActive(true);
            Utils.partie.mondeActuel = monde;

            // Grise les niveaux non disponibles

            for (var i = 0; i < 5; i++) {

                var mondeNiveau = Utils.partie.mondes[monde, i];
                var btnNiveau = mondesPanel[monde].transform.Find("Level_" + (i + 1));
                var inactif = btnNiveau ? btnNiveau.Find("Inactif") : null;
                var collectlvl = btnNiveau ? btnNiveau.Find("Collect_lvl") : null;
                var reussi = btnNiveau ? btnNiveau.Find("Reussi") : null;

                if (inactif) {
                    inactif.gameObject.SetActive(true);
                }
                if (collectlvl) {
                    var txt = collectlvl.gameObject.GetComponent<Text>();
                    string[] txtnums = txt.text.Split('/');
                    txt.text = mondeNiveau.bonus.ToString() + '/' + txtnums[1];
                }
                if (reussi) {
                    reussi.gameObject.SetActive(mondeNiveau.reussi);
                }
            }

            for (var i = 0 ; i < 5 ; i++) {

                var mondeNiveau = Utils.partie.mondes[monde, i];
                var btnNiveau = mondesPanel[monde].transform.Find("Level_" + (i + 1));
                var inactif = btnNiveau ? btnNiveau.Find("Inactif") : null;

                if (inactif) {
                    inactif.gameObject.SetActive(false);
                    if (mondeNiveau.reussi == false) break;
                }
            }
        }
    }

    public void SelectionneNiveau(int niveau) {

        var monde = Utils.partie.mondeActuel;
        niveau--;

        int dernierMonde = Utils.partie.GetDernierMonde();
        int dernierNiveau = Utils.partie.GetDernierNiveau();

        if (dernierMonde >= monde && monde < mondesPanel.Length
            && dernierNiveau >= niveau && niveau < Utils.partie.mondes.Length) {
            
            Utils.LoadLevel(monde, niveau);
        }
    }

    public void quitter() {
        Application.Quit();
    }

    // Partage

    public void partageFacebook() {
        Utils.PartageFacebook();
    }

    public void partageTwitter() {
        Utils.PartageTwitter();
    }

    public void partageGooglePlus() {
        Utils.PartageGooglePlus();
    }
}
