using UnityEngine;
using System.Collections;

public class BlockStart : MonoBehaviour {

    [SerializeField]
    private ControlesPerso controlesPerso;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col) {
        if (!enabled) return;
        controlesPerso.estArrete = true;
    }

    public void OnTouchDown() {
        if (!enabled) return;
        controlesPerso.estArrete = false;
    }

    public void OnMouseDown() {
        if (!enabled) return;
        controlesPerso.estArrete = false;
    }
}
