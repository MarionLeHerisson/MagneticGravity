using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlockMort : MonoBehaviour {

    [SerializeField]
    private Transform persoTransform;

    [SerializeField]
    private Transform blockDepartTransform;

    [SerializeField]
    private ControlesPerso controlesPerso;

    [SerializeField]
    private Tourelle_ShootSpeed[] tourelle_Shoot;

    [SerializeField]
    private Ball_Respawn[] tourelle;

    private bool reset = false;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
    }

    void FixedUpdate() {

        if (reset) {

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
            /*var positionDepart = blockDepartTransform.position;

            persoTransform.position = new Vector2(positionDepart.x, positionDepart.y + 2f);

            controlesPerso.initialise();

            foreach (var tour in tourelle) {
                tour.Init();
            }

            foreach (var tour_shoot in tourelle_Shoot) {
                tour_shoot.Desactivation(false);
            }

            reset = false;*/
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (!enabled) return;
        reset = true;
    }

}
