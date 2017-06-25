using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LevelEditorMoveCamera : MonoBehaviour {
    
    [SerializeField]
    private Transform cameraSceneTransform;

    [SerializeField]
    private Camera cameraMove;

    [SerializeField]
    private LevelEditor levelEditor;


    private Vector3 posSourisClique;
    private Vector3 posCameraClique;
    private bool premierClic = false;
    private float tpsPremierClic = 0.0f;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private Vector3 SourisVersMonde(Camera cam) {

        var pos = Input.mousePosition;
        pos.z = 50.0f;
        pos = cam.ScreenToWorldPoint(pos);
        pos.z = 0.0f;
        return pos;
    }

    void OnMouseDown() {

        // Si on est dans le menu de gauche, ne pas bouger la caméra
        if (Input.mousePosition.x < Screen.width / 5.0f ) return;

        //if (EventSystem.current.IsPointerOverGameObject()) return; // Marche pas avec le tactile

        premierClic = true;
        tpsPremierClic = Time.time;

        cameraMove.transform.position = cameraSceneTransform.position;
        posSourisClique = SourisVersMonde(cameraMove);
        posCameraClique = cameraSceneTransform.position;
    }

    void OnMouseDrag() {

        if (!premierClic) return;
        
        Vector3 pos = posCameraClique + posSourisClique - SourisVersMonde(cameraMove);

        Vector3 camPos = cameraSceneTransform.position;

        pos.z = camPos.z;
        
        cameraSceneTransform.position = pos;
    }

    void OnMouseUp() {

        if (!premierClic) return;
        premierClic = false;

        // Si on a cliqué rapidement, cacher le panneau des paramètres
        if (Time.time - tpsPremierClic < 0.2f) {

            levelEditor.CacheTousLesParametres();
        }

        // Met à jour la caméra temporaire
        cameraMove.transform.position = cameraSceneTransform.position;
    }
}
