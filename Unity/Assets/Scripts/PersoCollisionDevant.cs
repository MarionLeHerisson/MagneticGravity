using UnityEngine;
using System.Collections;

public class PersoCollisionDevant : MonoBehaviour {

    [SerializeField]
    private ControlesPerso controlesPerso;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Grimpette
    public void OnCollisionEnter2D(Collision2D collision) { // Grimpette

        if (!enabled) return;
        controlesPerso.collisionDevant(true);
    }

    public void OnCollisionExit2D(Collision2D collision) { // Grimpette

        if (!enabled) return;
        controlesPerso.collisionDevant(false);
    }
}
