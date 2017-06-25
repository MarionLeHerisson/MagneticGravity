using UnityEngine;
using System.Collections;

public class BlockMagneticHorizontal : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D persoRigidbody;

    [SerializeField]
    private ControlesPerso controlesperso;

    [SerializeField]
    private bool intensiteForte = false;

    [SerializeField]
    private bool polariteNegative;

    [SerializeField]
    private TextMesh polariteText;

    private bool enCollision = false;

    // Use this for initialization
    void Start () {
        enCollision = false;
        majPolariteText();
    }
	
    float calculeIntensite(float persoX, float blocX) {

        float intensite = 1.0f;
        if (intensiteForte) intensite = 2.0f;
        
        intensite *= 3.0f / Mathf.Abs(persoX - blocX);

        // Si le perso est à droite du bloc, inverser la polarité

        if (persoX < blocX) intensite *= -1f;
        
        return intensite;

    }

    void majPolariteText() {

        if (polariteNegative) {
            if (intensiteForte) polariteText.text = "- -";
            else polariteText.text = "-";
        }
        else {
            if (intensiteForte) polariteText.text = "++";
            else polariteText.text = "+";
        }

    }

    public void OnMouseDown() {
        if (!enabled) return;
        polariteNegative = !polariteNegative;
        majPolariteText();
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

            var intensite = calculeIntensite(persoRigidbody.position.x, this.transform.position.x);


            // Si la polarité du perso est différente de celle du bloc, inverser l'intensité

            if (polariteNegative != controlesperso.polariteNegative)
                persoRigidbody.velocity += new Vector2(-intensite, 0f);
            else
                persoRigidbody.velocity += new Vector2(intensite, 0f);
            

            // Limite la vélocité max Y

            var persoVelocite = persoRigidbody.velocity;

            if (persoVelocite.x > 8.0) {

                persoRigidbody.velocity = new Vector2(8.0f, persoVelocite.y);
            }
            else if (persoVelocite.x < -8.0) {

                persoRigidbody.velocity = new Vector2(8.0f, persoVelocite.x);
            }
        }
    }
}
