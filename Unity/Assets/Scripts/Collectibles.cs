using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col) {
        if (!enabled) return;
        Utils.partie.bonusActuel++;
        Destroy(target);
    }
}
