using UnityEngine;
using System.Collections;

public class LevelCustom : MonoBehaviour {

    [SerializeField]
    string jsonWorldLevel;

    [SerializeField]
    NiveauJSON niveauJSON;

    [SerializeField]
    LoadScenes loadScenes;

    [SerializeField]
    GameObject boutonEditeur;

    [SerializeField]
    private NiveauJsonSO niveauJsonSO;
    
    
    // Partage

    public void partageFacebook() {
        Utils.PartageFacebook(niveauJSON.code);
    }

    public void partageTwitter() {
        Utils.PartageTwitter(niveauJSON.code);
    }

    public void partageGooglePlus() {
        Utils.PartageGooglePlus(niveauJSON.code);
    }

    // Use this for initialization
    void Start () {

        if (boutonEditeur != null) boutonEditeur.SetActive(true);

        if (jsonWorldLevel != "" && jsonWorldLevel != null) {
            niveauJSON.Charge(jsonWorldLevel);
            return;
        }


        if (niveauJSON) {
            //niveauJSON.Charge(exempleJSON);
            
            switch (niveauJsonSO.quelJSONCharger) {

                case Utils.JSON_AUCUN: // Aucun, niveau vide par défaut
                    niveauJSON.Charge("");
                    break;

                case Utils.JSON_EDITOR: // JSON actuellement chargé dans l'éditeur de niveaux
                    niveauJSON.Charge(niveauJsonSO.jsonEditor);
                    break;

                case Utils.JSON_MES_NIVEAUX: // JSON d'un niveau sauvegardé en local
                    niveauJsonSO.jsonEditor = niveauJsonSO.jsonTelecharge;
                    niveauJSON.Charge(niveauJsonSO.jsonMesNiveaux);
                    break;

                case Utils.JSON_TELECHARGE: // JSON d'un niveau téléchargé
                    //boutonEditeur.SetActive(false);
                    niveauJsonSO.jsonEditor = niveauJsonSO.jsonTelecharge;
                    niveauJSON.Charge(niveauJsonSO.jsonTelecharge);
                    break;

                default:
                    niveauJSON.Charge("");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
