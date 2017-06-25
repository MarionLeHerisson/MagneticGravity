using UnityEngine;
using System.Collections;

public class BlockPolarityReverse : MonoBehaviour {

    [SerializeField]
    private ControlesPerso controlesPerso;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col) {
        controlesPerso.setPolariteNegative(!controlesPerso.polariteNegative);
    }
}
