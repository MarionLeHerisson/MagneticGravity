using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenes : MonoBehaviour
{

    [SerializeField]
    private NiveauJsonSO niveauJsonSO;

    [SerializeField]
    private Text codeNiveauText;

    [SerializeField]
    private Text codeNiveauStatusText;

    // Charger un niveau
    public void LoadLevel()
    {
        var LevelAttributes = GetComponent<LevelAttributes>();
        SceneManager.LoadScene(LevelAttributes.indexNiveau);
    }

    // Retourner au Menu
    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    // Réinitialisation du niveau actuel
    public void Reload()
    {
        Utils.partie.bonusActuel = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    //Charger le niveau suivant
    public void NextLevel()
    {
        Utils.LoadNextLevel();
    }

    // Charge le niveau JSON
    public void LoadJSONLevel(int quelNiveauCharger = -1) {

        // Définit quel JSON charger à partir des JSON actuellements sauvegarés dans le Scriptable Object
        
        if (niveauJsonSO && quelNiveauCharger >= Utils.JSON_AUCUN) {
            niveauJsonSO.quelJSONCharger = quelNiveauCharger;
        }

        Utils.partie.bonusActuel = 0;
        SceneManager.LoadScene("Level_Custom");
    }

    // Charge l'éditeur de niveaux
    public void LoadLevelEditor(int quelNiveauCharger = -1) {

        // Définit quel JSON charger à partir des JSON actuellements sauvegarés dans le Scriptable Object
        
        if (niveauJsonSO && quelNiveauCharger >= Utils.JSON_AUCUN) {
            niveauJsonSO.quelJSONCharger = quelNiveauCharger;
        }

        Utils.partie.bonusActuel = 0;
        SceneManager.LoadScene("Level_Editor");
    }
    
    // Télécharge un niveau à partir d'un code
    public void DownloadLevel(string code = "") {

        codeNiveauStatusText.text = "";

        if (!codeNiveauText && code == "") return;

        if (codeNiveauText) code = codeNiveauText.text.ToString();

        if (code == "") {
            codeNiveauStatusText.text = "Code invalide";
            return;
        }

        codeNiveauStatusText.text = "Téléchargement en cours...";

        APITelechargerNiveau(code);

    }

    IEnumerator TelechargementDuNiveau(WWW www) {

        yield return www;
        Debug.Log(www.text);
        if (www.error == null) {

            codeNiveauStatusText.text = "";

            niveauJsonSO.jsonTelecharge = www.text;
            LoadJSONLevel(Utils.JSON_TELECHARGE);
        }
        else {

            codeNiveauStatusText.text = "Erreur : " + www.error;
        }
    }

    public void APITelechargerNiveau(string code) {

        string url = Utils.LIEN_TELECHARGE_NIVEAU + "?code=" + code;
        
        WWW www = new WWW(url);

        StartCoroutine(TelechargementDuNiveau(www));
    }
}