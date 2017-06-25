using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Niveau JSON SO")]
public class NiveauJsonSO : ScriptableObject {
    
    public string jsonEditor; // JSON actuellement chargé dans l'éditeur de niveaux
    public string jsonMesNiveaux; // JSON d'un niveau sauvegardé en local
    public string jsonTelecharge; // JSON d'un niveau téléchargé

    public int quelJSONCharger; // Quel JSON charger au chargement de Level_Custom ? Voir la classe statique Utils
                                // 0 : aucun
                                // 1 : jsonEditor
                                // 2 : jsonMesNiveaux
                                // 3 : jsonTelecharge
}
