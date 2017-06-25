using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LevelEditorBlockDrag : MonoBehaviour {

    struct VecInt2D {
        public int x;
        public int y;
        public bool valide;
    };

    [SerializeField]
    private LevelEditor levelEditor;

    
    private Vector3 anciennePos;
    private bool premierClic = false;
    private float tpsPremierClic = 0.0f;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void Clique() {

        // Cache tous les paramètres

        levelEditor.CacheTousLesParametres();

        // Affiche le panneau correspondant au bloc et met à jour les paramètres du panneau

        if (this.GetComponent<BlockMagnetic>()) {

            levelEditor.panelParametresBlocMagnetique.SetActive(true);
            levelEditor.MajPanneauBlocMagnetique();
        }

        else if (this.GetComponentInChildren<Tourelle_ShootSpeed>()) {

            levelEditor.panelParametresBlocTourelle.SetActive(true);
            levelEditor.MajPanneauBlocTourelle();
        }

        else if (this.GetComponentInChildren<Ralenti>()) {

            levelEditor.panelParametresBlocRalenti.SetActive(true);
            levelEditor.MajPanneauBlocRalenti();
        }
    }

    void OnMouseDown() {

        if (EventSystem.current.IsPointerOverGameObject()) return;

        premierClic = true;
        tpsPremierClic = Time.time;

        anciennePos = this.gameObject.transform.position;

        levelEditor.objetDrag = this.gameObject;

    }

    void OnMouseDrag() {

        if (!premierClic) return;
        
        Vector3 pos = levelEditor.SourisVersMonde();

        pos = levelEditor.ColleSurLaGrille(pos);

        this.gameObject.transform.position = pos;
    }

    void OnMouseUp() {

        if (!premierClic) return;
        premierClic = false;


        // Si on cliqué rapidement :affiche les paramètres

        if (Time.time - tpsPremierClic < 0.2f) {

            Clique();
        }



        // Si déclic au-dessus de la poubelle active, virer le bloc

        // Si on est dans le menu de gauche, ne pas bouger la caméra
        
        //if (EventSystem.current.IsPointerOverGameObject()) { // Marche pas avec le tactile
        if (Input.mousePosition.x < Screen.width / 5.0f) {

            if (levelEditor.GetPoubelleActive()) {

                // Ne pas supprimer le bloc de départ ni de fin

                if (!this.GetComponent<BlockStart>() && !this.GetComponent<BlockEnd>()) {

                    levelEditor.VireDeLaGrille(anciennePos);
                    this.gameObject.SetActive(false);

                    return;
                }

            }
        }


        // Si l'ajout dans la grille est un succès
        if (levelEditor.AjouteDansLaGrille(this.gameObject.transform.position)) {

            // Enlever de la grille l'ancienne position
            levelEditor.VireDeLaGrille(anciennePos);
        }
        else {
            // Sinon repositionner l'objet à sa position initiale
            this.gameObject.transform.position = anciennePos;
        }

    }
}
