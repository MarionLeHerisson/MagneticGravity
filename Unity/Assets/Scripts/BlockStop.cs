using UnityEngine;
using System.Collections;

public class BlockStop : MonoBehaviour {

    [SerializeField]
    private GameObject persoGameObject;

    private Rigidbody2D persoRigidbody;

    [SerializeField]
    private ControlesPerso controlesPerso;
    
    // Use this for initialization
    void Start()
    {

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
