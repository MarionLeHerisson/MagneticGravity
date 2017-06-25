using UnityEngine;
using System.Collections;

public class BlockMagnetic : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D persoRigidbody;

    [SerializeField]
    private ControlesPerso controlesperso;

    [SerializeField]
    private bool intensiteForte = false;

    [SerializeField]
    private bool polariteNegative;

    [SerializeField]
    private GameObject screen;

    [SerializeField]
    private GameObject screenplus;

    private Animator screenAnim;

    private Animator screenAnimplus;

    private bool enCollision = false;

    private float velociteYMax = 0.0f;

    // Use this for initialization
    void Start () {
        screenAnim = screen.GetComponent<Animator>();
        screenAnimplus = screenplus.GetComponent<Animator>();
        enCollision = false;
        majPolariteText();
    }
	
    float calculeIntensite(float persoY, float blocY) {

        float intensite = 1.0f;

        if (intensiteForte) {
            intensite = 2.0f;
            velociteYMax = 30.0f * Utils.facteurTemps;
        }
        else {
            velociteYMax = 8.0f * Utils.facteurTemps;
        }
        
        intensite *= 3.0f / Mathf.Abs(persoY - blocY);

        // Si le perso est sous le bloc, inverser la polarité

        if (persoY < blocY) intensite *= -1f;
        
        return intensite * Utils.facteurTemps /** Time.timeScale*/ * Time.deltaTime / 0.02f;

    }

    public void majPolariteText() {

        // Si bloc inactif, pas d'animation

        if (!this.gameObject.activeSelf) {
            if (polariteNegative) {
                if (intensiteForte) {
                    screen.SetActive(false);
                    screenplus.SetActive(true);
                }
                else {
                    screen.SetActive(true);
                    screenplus.SetActive(false);
                }
            }
            else {
                if (intensiteForte) {
                    screen.SetActive(false);
                    screenplus.SetActive(true);
                }
                else {
                    screen.SetActive(true);
                    screenplus.SetActive(false);
                }
            }
            return;
        }

        // Animations

        if (screenAnim == null) screenAnim = screen.GetComponent<Animator>();
        if (screenAnimplus == null) screenAnimplus = screenplus.GetComponent<Animator>();

        if (polariteNegative) {
            if (intensiteForte)
            {
                screen.SetActive(false);
                screenplus.SetActive(true);
                screenAnimplus.SetBool("PolNeg", true);
            }

            else
            {
                screen.SetActive(true);
                screenplus.SetActive(false);
                screenAnim.SetBool("PolNeg", true);
            }

            if (screen.activeSelf) {
                screenAnim.SetBool("PolPos", false);
                screenAnim.SetBool("PolNeg", true);
            }
            if (screenplus.activeSelf) {
                screenAnimplus.SetBool("PolPos", false);
                screenAnimplus.SetBool("PolNeg", true);
            }
        }

        else {
            if (intensiteForte)
            {
                screen.SetActive(false);
                screenplus.SetActive(true);
                screenAnimplus.SetBool("PolNeg", false);
            } 

            else
            {
                screen.SetActive(true);
                screenplus.SetActive(false);
                screenAnim.SetBool("PolNeg", false);
            }
            if (screen.activeSelf) {
                screenAnim.SetBool("PolPos", true);
                screenAnim.SetBool("PolNeg", false);
            }
            if (screenplus.activeSelf) {
                screenAnimplus.SetBool("PolPos", true);
                screenAnimplus.SetBool("PolNeg", false);
            }
        }
    }

    public void SetPolariteNegative(bool pol) {
        polariteNegative = pol;
        majPolariteText();
    }

    public void SetIntensiteForte(bool forte) {
        intensiteForte = forte;
        majPolariteText();
    }

    public bool GetPolariteNegative() {
        return polariteNegative;
    }

    public bool GetIntensiteForte() {
        return intensiteForte;
    }

    public void OnMouseDown() {
        if (!enabled) return;
        SetPolariteNegative(!polariteNegative);
    }
    
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col) {
        if (!enabled) return;
        enCollision = true;

    }

    public void OnTriggerExit2D(Collider2D col) {
        if (!enabled) return;
        enCollision = false;
    }

    void FixedUpdate() {
        

        // Si perso dans le rayon d'action de l'aimant

        if (enCollision) {

            var intensite = calculeIntensite(persoRigidbody.position.y, this.transform.position.y);


            // Si la polarité du perso est différente de celle du bloc, inverser l'intensité

            if (polariteNegative != controlesperso.polariteNegative)
                persoRigidbody.velocity += new Vector2(0f, -intensite);
            else
                persoRigidbody.velocity += new Vector2(0f, intensite);
            

            // Limite la vélocité max Y

            var persoVelocite = persoRigidbody.velocity;

            if (persoVelocite.y > velociteYMax) {

                persoRigidbody.velocity = new Vector2(persoVelocite.x, velociteYMax);
            }
            else if (persoVelocite.y < -velociteYMax) {

                persoRigidbody.velocity = new Vector2(persoVelocite.x, -velociteYMax);
            }
        }
    }
}
