using UnityEngine;
using System.Collections;

public class Ralenti : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float dureeRalenti = 10.0f;

    [SerializeField]
    private float facteurRalenti = 0.3f;

    [SerializeField]
    private ControlesPerso controlesPerso;
    [SerializeField]
    private GameObject haloBleu0;
    [SerializeField]
    private GameObject haloBleu1;

    private float finRalenti = 0.0f;
    private bool ralentir = false;

    public void SetDuree(float duree) {
        dureeRalenti = duree;
    }

    public float GetDuree() {
        return dureeRalenti;
    }

    // Use this for initialization
    void Start() {

        Utils.AnnuleRalenti();
    }

    // Update is called once per frame
    void Update() {

        if (!ralentir) return;

        if (Time.fixedTime >= finRalenti) {

            //controlesPerso.MulVelocite(1.0f / facteurRalenti);

            Utils.AnnuleRalenti();
            haloBleu0.SetActive(false);
            haloBleu1.SetActive(false);
            ralentir = false;

        }

    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (!controlesPerso) return;
        if (!enabled) return;
        if (!this.gameObject.activeSelf) return;
        if (ralentir) return;

        ralentir = true;

        Utils.SetRalenti(facteurRalenti);

        //controlesPerso.MulVelocite(facteurRalenti);

        //finRalenti = Time.fixedTime + dureeRalenti;// * facteurRalenti;
        finRalenti = Time.fixedTime + dureeRalenti * facteurRalenti;

        haloBleu0.SetActive(true);
        haloBleu1.SetActive(true);

        target.SetActive(false);
    }
}
