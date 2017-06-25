using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelEditor : MonoBehaviour {

    struct VecInt2D {
        public int x;
        public int y;
        public bool valide;
    };
    
    private string capture = "";

    private const float largeurBloc = 3.0f;
    private const float hauteurBloc = 1.5f;
    private const float largeurBloc05 = 1.5f;
    private const float hauteurBloc05 = 0.75f;

    private const int BLOC_SIMPLE = 1;
    private const int BLOC_START = 2;
    private const int BLOC_END = 3;
    private const int BLOC_MAGNETIC = 4;
    private const int BLOC_POLARITY_REVERSE = 5;
    private const int BLOC_RUN_REVERSE = 6;
    private const int BLOC_STOP = 7;
    private const int BLOC_TRAP = 8;
    private const int BLOC_CHECKPOINT = 9;
    private const int BLOC_COLLECTIBLE = 10;
    private const int BLOC_TURRET = 11;
    private const int BLOC_RALENTI = 12;

    [SerializeField]
    private NiveauJsonSO niveauJsonSO;

    [SerializeField]
    LoadScenes loadScenes;

    [SerializeField]
    private ControlesPerso controlesperso;

    [SerializeField]
    private Camera cameraScene;

    [SerializeField]
    private NiveauJSON niveauJSON;

    [SerializeField]
    private InputField niveauSaveTitre;

    [SerializeField]
    private Text niveauSaveStatut;

    [SerializeField]
    public GameObject panelParametresBlocMagnetique;

    [SerializeField]
    public GameObject panelParametresBlocTourelle;

    [SerializeField]
    public GameObject panelParametresBlocRalenti;

    [SerializeField]
    public Toggle parametresBlocMagnetiqueIntensiteForte;

    [SerializeField]
    public Toggle parametresBlocMagnetiquePolariteNegative;

    [SerializeField]
    public Slider parametresBlocTourelleBallsByShot;

    [SerializeField]
    public Slider parametresBlocTourelleWaitBalls;

    [SerializeField]
    public Slider parametresBlocTourelleWaitShoot;

    [SerializeField]
    public Slider parametresBlocRalentiDuree;

    [SerializeField]
    public GameObject canvas;

    [Serializable]
    public class JSONSaveLevel {
        public string code;
        public string error;
    }

    public GameObject objetDrag;
    public int[,] grilleObjets;
    private bool poubelleActive = false;

    // Use this for initialization
    void Start () {

        grilleObjets = new int[128, 128];
        
        if (niveauJSON) {

            switch (niveauJsonSO.quelJSONCharger) {

                case Utils.JSON_AUCUN: // Aucun, niveau vide par défaut
                    niveauJSON.Charge("");
                    break;

                case Utils.JSON_EDITOR: // JSON actuellement chargé dans l'éditeur de niveaux
                    niveauJSON.Charge(niveauJsonSO.jsonEditor);
                    break;

                case Utils.JSON_MES_NIVEAUX: // JSON d'un niveau sauvegardé en local
                    niveauJSON.Charge(niveauJsonSO.jsonMesNiveaux);
                    break;

                case Utils.JSON_TELECHARGE: // JSON d'un niveau téléchargé
                    niveauJSON.Charge(niveauJsonSO.jsonTelecharge);
                    break;

                default:
                    niveauJSON.Charge("");
                    break;
            }

            MajGrille();
            MajTitre();
            MajCamera();
        }
        controlesperso.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
	
	}

    ////////////// Paramètres de blocs //////////////


    // Désactive tous les panneaux de paramètres des blocs
    public void CacheTousLesParametres() {

        panelParametresBlocMagnetique.SetActive(false);
        panelParametresBlocTourelle.SetActive(false);
        panelParametresBlocRalenti.SetActive(false);
    }

    // Met à jour le panneau des paramètres Bloc Tourelle
    public void MajPanneauBlocMagnetique() {

        if (!objetDrag) return;

        var compBlocMagnetique = objetDrag.GetComponent<BlockMagnetic>();

        if (!compBlocMagnetique) return;

        var intensiteNegative = compBlocMagnetique.GetIntensiteForte(); // Passer par une variable car le isOn du trigger ou le value des slider appelle automatiquement onvaluechange et affecte les valeurs pas encore récupérées.
        var polariteNegative = compBlocMagnetique.GetPolariteNegative();

        parametresBlocMagnetiqueIntensiteForte.isOn = intensiteNegative;
        parametresBlocMagnetiquePolariteNegative.isOn = polariteNegative;
    }

    // Met à jour le bloc tourelle en fonction des paramètres
    public void MajBlocMagnetiqueParams() {

        if (!objetDrag) return;

        var compBlocMagnetique = objetDrag.GetComponent<BlockMagnetic>();

        if (!compBlocMagnetique) return;

        compBlocMagnetique.SetIntensiteForte(parametresBlocMagnetiqueIntensiteForte.isOn);
        compBlocMagnetique.SetPolariteNegative(parametresBlocMagnetiquePolariteNegative.isOn);
    }

    // Met à jour le panneau des paramètres Bloc Magnétique
    public void MajPanneauBlocTourelle() {
        
        if (!objetDrag) return;

        var compBlocTourelle = objetDrag.GetComponentInChildren<Tourelle_ShootSpeed>();

        if (!compBlocTourelle) return;

        var ballsByShot = compBlocTourelle.GetBallsByShot();
        var waitBalls = compBlocTourelle.GetWaitBetweenBalls();
        var waitShoot = compBlocTourelle.GetWaitBetweenShoot();

        parametresBlocTourelleBallsByShot.value = ballsByShot;
        parametresBlocTourelleWaitBalls.value = waitBalls;
        parametresBlocTourelleWaitShoot.value = waitShoot;

        parametresBlocTourelleBallsByShot.GetComponent<SliderToText>().MajTexte();
        parametresBlocTourelleWaitBalls.GetComponent<SliderToText>().MajTexte();
        parametresBlocTourelleWaitShoot.GetComponent<SliderToText>().MajTexte();
    }

    // Met à jour le bloc magnétique en fonction des paramètres
    public void MajBlocTourelleParams() {

        if (!objetDrag) return;

        var compBlocTourelle = objetDrag.GetComponentInChildren<Tourelle_ShootSpeed>();
        
        if (!compBlocTourelle) return;

        compBlocTourelle.SetBallsByShot((int)(parametresBlocTourelleBallsByShot.value));
        compBlocTourelle.SetWaitBetweenBalls(parametresBlocTourelleWaitBalls.value);
        compBlocTourelle.SetWaitBetweenShoot(parametresBlocTourelleWaitShoot.value);
    }


    // Met à jour le panneau des paramètres Bloc Magnétique
    public void MajPanneauBlocRalenti() {

        if (!objetDrag) return;

        var ralenti = objetDrag.GetComponentInChildren<Ralenti>();

        if (!ralenti) return;

        var duree = ralenti.GetDuree();

        parametresBlocRalentiDuree.value = duree;

        parametresBlocRalentiDuree.GetComponent<SliderToText>().MajTexte();
    }

    // Met à jour le bloc magnétique en fonction des paramètres
    public void MajBlocRalentiParams() {

        if (!objetDrag) return;

        var ralenti = objetDrag.GetComponentInChildren<Ralenti>();

        if (!ralenti) return;

        ralenti.SetDuree((float)(parametresBlocRalentiDuree.value));
    }


    ////////////// Grille //////////////


    // Vire ou non un objet dans la grile en fonction de sa position

    public bool VireDeLaGrille(Vector3 pos) {

        VecInt2D vecInt = Vector3ToVecInt2D(pos);

        // Si les coordonnées sont dans la zone d'édition et qu'il n'y a pas déjà un bloc à cette case de la grille
        if (vecInt.valide && grilleObjets[vecInt.x, vecInt.y] == 1) {

            // Màj la grille en indiquant qu'un bloc s'y trouve pas
            grilleObjets[vecInt.x, vecInt.y] = 0;

            return true;
        }

        return false;
    }


    // Ajoute ou non un objet dans la grile en fonction de sa position

    public bool AjouteDansLaGrille(Vector3 pos) {

        VecInt2D vecInt = Vector3ToVecInt2D(pos);

        // Si les coordonnées sont dans la zone d'édition et qu'il n'y a pas déjà un bloc à cette case de la grille
        if (vecInt.valide && grilleObjets[vecInt.x, vecInt.y] == 0) {

            // Màj la grille en indiquant qu'un bloc s'y trouve
            grilleObjets[vecInt.x, vecInt.y] = 1;

            return true;
        }

        return false;
    }


    // Convertit la position d'un bloc vers des coordonnées de grille

    private VecInt2D Vector3ToVecInt2D(Vector3 pos) {

        VecInt2D vecInt = new VecInt2D();

        int x = (int)((pos.x + largeurBloc05) / largeurBloc);
        int y = (int)((pos.y + hauteurBloc05) / hauteurBloc);

        vecInt.x = x;
        vecInt.y = y;

        if (x >= 0 && y >= 0
            && x < 128 && y < 128) {

            vecInt.valide = true;
        }
        else {
            vecInt.valide = false;
        }

        return vecInt;
    }


    // Convertit les coordonnées de la souris vers les coordonnées de la scène

    public Vector3 SourisVersMonde() {

        var pos = Input.mousePosition;
        pos.z = 50.0f;
        pos = cameraScene.ScreenToWorldPoint(pos);
        pos.z = 0.0f;
        return pos;
    }


    // Limite les coordonnees de l'objet à une case libre de la grille

    public Vector3 ColleSurLaGrille(Vector3 pos) {
        
        VecInt2D vecInt = Vector3ToVecInt2D(pos);
        
        // Si la grille est libre à cet endroit
        if (vecInt.valide
            && grilleObjets[vecInt.x, vecInt.y] == 0) {
            return new Vector3(vecInt.x * largeurBloc, vecInt.y * hauteurBloc, 0f);
        }
        // Sinon
        return new Vector3(pos.x, pos.y, -1.0f);
    }

    private GameObject PiocheBlockSimple() {
        foreach (var bloc in niveauJSON.blocsSimples) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockMagnetique() {
        foreach (var bloc in niveauJSON.blocsMagnetics) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockPolarityReverse() {
        foreach (var bloc in niveauJSON.blocsPolarityReverses) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockRunReverse() {
        foreach (var bloc in niveauJSON.blocsRunReverses) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockStop() {
        foreach (var bloc in niveauJSON.blocsStops) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockTrap() {
        foreach (var bloc in niveauJSON.blocsTraps) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockCheckpoint() {
        foreach (var bloc in niveauJSON.blocsCheckpoints) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockCollectible() {
        foreach (var bloc in niveauJSON.blocsCollectibles) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockTurret() {
        foreach (var bloc in niveauJSON.blocsTurrets) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }
    private GameObject PiocheBlockRalenti() {
        foreach (var bloc in niveauJSON.blocsRalenti) {
            if (!bloc.gameObject.activeSelf) return bloc.gameObject;
        }
        return null;
    }


    // Glisse un bloc du menu vers la scène

    public void StartDragBlock(int typeDeBloc = 0) {

        objetDrag = null;

        switch (typeDeBloc) {
            
            case BLOC_SIMPLE: // 1
                objetDrag = PiocheBlockSimple();
                break;
    
            case BLOC_MAGNETIC: // 4
                objetDrag = PiocheBlockMagnetique();
                break;

            case BLOC_POLARITY_REVERSE: // 5
                objetDrag = PiocheBlockPolarityReverse();
                break;

            case BLOC_RUN_REVERSE: // 6
                objetDrag = PiocheBlockRunReverse();
                break;

            case BLOC_STOP: // 7
                objetDrag = PiocheBlockStop();
                break;

            case BLOC_TRAP: // 8
                objetDrag = PiocheBlockTrap();
                break;

            case BLOC_CHECKPOINT: // 9
                objetDrag = PiocheBlockCheckpoint();
                break;

            case BLOC_COLLECTIBLE: // 10
                objetDrag = PiocheBlockCollectible();
                break;

            case BLOC_TURRET: // 11
                objetDrag = PiocheBlockTurret();
                break;

            case BLOC_RALENTI: // 12
                objetDrag = PiocheBlockRalenti();
                break;

            default:
                break;
        }

        if (objetDrag) {
            objetDrag.SetActive(true);

            if (typeDeBloc == BLOC_MAGNETIC) objetDrag.GetComponent<BlockMagnetic>().majPolariteText();

            objetDrag.transform.position = ColleSurLaGrille(SourisVersMonde());
        }
    }

    public void DragBlock() {
        if (objetDrag) {
            objetDrag.transform.position = ColleSurLaGrille(SourisVersMonde());
        }
    }

    public void EndDragBlock() {

        if (objetDrag) {

            VecInt2D vecInt = Vector3ToVecInt2D(objetDrag.transform.position);

            // Si les coordonnées sont dans la zone d'édition et qu'il n'y a pas déjà un bloc à cette case de la grille
            if (vecInt.valide && grilleObjets[vecInt.x, vecInt.y] == 0) {

                // Màj la grille en indiquant qu'un bloc s'y trouve
                grilleObjets[vecInt.x, vecInt.y] = 1;
            }
            else {
                objetDrag.gameObject.SetActive(false);
            }
            
        }

        objetDrag = null;
    }


    // Met à jour le champ titre avec le json chargé

    public void MajTitre() {

        niveauSaveTitre.text = niveauJSON.GetTitre();

    }

    public void MajCamera() {

        var posStart = niveauJSON.blocStart.transform.position;
        var posCam = cameraScene.transform.position;

        cameraScene.transform.position = new Vector3(posStart.x, posStart.y, posCam.z);

    }

    // Met à jour la grille en fonction du niveau JSON chargé

    public void MajGrille() {

        VecInt2D vecInt;

        // Màj la grille avec le bloc de départ
        vecInt = Vector3ToVecInt2D(niveauJSON.blocStart.transform.position);
        grilleObjets[vecInt.x, vecInt.y] = 1;

        // Màj la grille avec le bloc de fin
        vecInt = Vector3ToVecInt2D(niveauJSON.blocEnd.transform.position);
        grilleObjets[vecInt.x, vecInt.y] = 1;

        // Màj la grille avec les blocs simples
        foreach (var bloc in niveauJSON.blocsSimples) {
            if (bloc.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les blocs magnétiques
        foreach (var bloc in niveauJSON.blocsMagnetics) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les blocs polarity reverse
        foreach (var bloc in niveauJSON.blocsPolarityReverses) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les blocs run reverse
        foreach (var bloc in niveauJSON.blocsRunReverses) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les blocs checkpoint
        foreach (var bloc in niveauJSON.blocsCheckpoints) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les blocs stop
        foreach (var bloc in niveauJSON.blocsStops) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les blocs de mort
        foreach (var bloc in niveauJSON.blocsTraps) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les collectibles
        foreach (var bloc in niveauJSON.blocsCollectibles) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }

        // Màj la grille avec les ralentis
        foreach (var bloc in niveauJSON.blocsRalenti) {
            if (bloc.gameObject.activeSelf) {
                vecInt = Vector3ToVecInt2D(bloc.transform.position);
                if (vecInt.valide) {
                    grilleObjets[vecInt.x, vecInt.y] = 1;
                }
            }
        }
    }


    // Poubelle

    public void SetPoubelleActive(bool val) {

        poubelleActive = val;
    }

    public bool GetPoubelleActive() {

        return poubelleActive;
    }



    // Sauvegarde

    IEnumerator TeleversementDuNiveau(WWW www) {

        yield return www;
        Debug.Log(www.text);

        string etatSauv = "Sauvegarde en ligne impossible.";

        if (www.error == null && www.text != "") {
            
            try {
                JSONSaveLevel jsonSaveLevel = JsonUtility.FromJson<JSONSaveLevel>(www.text);
                
                if (jsonSaveLevel.error == null) {
                    
                    if (jsonSaveLevel.code != "") {

                        niveauJSON.code = jsonSaveLevel.code;
                        etatSauv = "Sauvegardé";
                    }
                    else {
                        etatSauv = "Erreur : code de retour manquant.";
                    }
                }
                else {
                    etatSauv = jsonSaveLevel.error;
                }
            }
            catch (Exception e) {
                Debug.Log(e);
            }
        }
        
        niveauSaveStatut.text = etatSauv;
    }

    public void APIEnvoyerNiveau(string json) {

        string url = Utils.LIEN_SAUVEGARDE_NIVEAU;

        WWWForm form = new WWWForm();

        form.AddField("json", json);

        WWW www = new WWW(url, form);

        StartCoroutine(TeleversementDuNiveau(www));
    }


    // Sauvegarde le niveau en JSON

    public void Sauvegarde() {

        niveauSaveStatut.text = "";

        string titre = niveauSaveTitre.text.ToString();

        if (titre == "") {
            niveauSaveStatut.text = "Titre incorrect";
            return;
        }

        var code = niveauJSON.code;

        string json = niveauJSON.ExporteJSON(grilleObjets, titre, 0, code, SystemInfo.deviceUniqueIdentifier, capture);
        
        niveauJsonSO.jsonEditor = json;

        niveauSaveStatut.text = "Sauvegarde en ligne en cours...";

        APIEnvoyerNiveau(json);

        Debug.Log(json);
    }


    // Jouer le niveau JSON

    public void Jouer() {

        string titre = niveauSaveTitre.text.ToString();

        if (titre == "") {
            niveauSaveStatut.text = "Titre incorrect";
            return;
        }

        var code = niveauJSON.code;

        string json = niveauJSON.ExporteJSON(grilleObjets, titre, 0, code, SystemInfo.deviceUniqueIdentifier, capture);

        niveauJsonSO.jsonEditor = json;

        loadScenes.LoadJSONLevel(Utils.JSON_EDITOR);
        
    }


    // Capture

    public void BtnSave() {

        if (canvas == null) return;

        canvas.SetActive(false);
        StartCoroutine(Capturer());

    }

    public IEnumerator Capturer() {

        yield return new WaitForEndOfFrame();
        
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        //Texture2D min = ScaleTexture(tex, 160, 90);
        //Texture2D min = ScaleTexture(tex, 80, 45);
        Texture2D min = ScaleTexture(tex, 100, 60);

        capture = System.Convert.ToBase64String(min.EncodeToPNG());

        canvas.SetActive(true);

    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight) {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
        float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
        for (int px = 0; px < rpixels.Length; px++) {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth),
                              incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }

}
