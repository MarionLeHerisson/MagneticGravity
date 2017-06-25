using UnityEngine;
using System.Collections;

public class ControlesPerso : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D cibleRigidbody;

    [SerializeField]
    private TextMesh polariteText;

    [SerializeField]
    public float vitesseInitX;

    [SerializeField]
    public bool estArrete;

    [SerializeField]
    public bool polariteNegative;

    [SerializeField]
    public bool enTrainDeGrimper = false;
    public bool enTrainDeGrimperPrec = false;

    private float tpsCommenceGrimper = -10.0f;
    private float tpsFiniGrimper = 0.0f;
    private float tpsEpuise = 0.0f;
    private bool peutGrimper = true;
    private bool epuise = false;

    private Vector3 anspos;

    // Définit la polarité
    public void setPolariteNegative(bool pol) {
        polariteNegative = pol;
        majPolariteText();
    }

    void majPolariteText() {

        if (polariteNegative) {
            polariteText.text = "-";
        }
        else {
            polariteText.text = "+";
        }
    }

    public void initialise() {
        if (vitesseInitX < 0) vitesseInitX = -vitesseInitX;
        estArrete = true;
        polariteNegative = false;
        majPolariteText();
    }

    public void collisionDevant(bool actif) {
        
        enTrainDeGrimperPrec = enTrainDeGrimper;
        enTrainDeGrimper = actif;
        
        if (actif) {
            tpsCommenceGrimper = Time.fixedTime;
        }
        else {
            tpsFiniGrimper = Time.fixedTime;
        }
    }

    // Use this for initialization
    void Start () {

        vitesseInitX = 12.0f * Utils.facteurTemps;

        if (estArrete) {
            cibleRigidbody.velocity = new Vector2(0, 0);
        }
        else {
            cibleRigidbody.velocity = new Vector2(vitesseInitX, 0f);
        }

        majPolariteText();
    }

	// Update is called once per frame
	void Update () {

    }

    public void MulVelocite(float fac) {

        var velocite = cibleRigidbody.velocity;
        cibleRigidbody.velocity = new Vector2(velocite.x * fac, velocite.y * fac);
    }

    void FixedUpdate() {
        
        if (vitesseInitX < 0) vitesseInitX = -12.0f * Utils.facteurTemps;
        else vitesseInitX = 12.0f * Utils.facteurTemps;

        var velociteCible = cibleRigidbody.velocity;

        // Grimpe
        
        if (tpsFiniGrimper - tpsCommenceGrimper < 0.6f) {
            peutGrimper = true;
        }
        else {
            peutGrimper = false;
        }

        if (peutGrimper && !epuise) {
            if (velociteCible.x < 0.0001 && !estArrete) {
                velociteCible.y = 3f * Utils.facteurTemps;

                if (Time.fixedTime - tpsCommenceGrimper > 0.5f) {
                    tpsEpuise = Time.fixedTime;
                    epuise = true;
                }
            }
        }

        if (epuise) {
            if (Time.fixedTime - tpsEpuise > 1.0f) {
                epuise = false;
                if (enTrainDeGrimper) {
                    tpsCommenceGrimper = Time.fixedTime;
                }
            }
            else if (Mathf.Abs(velociteCible.y - 0.0f) < 0.001f) {
                tpsEpuise -= 0.1f;
            }
        }

        // Vient de finir de grimper

        if (enTrainDeGrimperPrec && !enTrainDeGrimper && Time.fixedTime - tpsFiniGrimper > 0.1) {
            velociteCible.y = -3f * Utils.facteurTemps;
            enTrainDeGrimperPrec = false;
        }

        if (estArrete) {
            cibleRigidbody.velocity = new Vector2(0, 0);
        }
        else {
            cibleRigidbody.velocity = new Vector2(vitesseInitX, velociteCible.y);
        }

    }


}
