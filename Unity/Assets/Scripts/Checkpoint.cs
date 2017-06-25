using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private Transform previousStart;

    [SerializeField]
    private Transform newStart;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!enabled) return;
        var previousPosition = previousStart.position;
        var newPosition = newStart.position;

        previousStart.position = new Vector2(newPosition.x, newPosition.y);
        newStart.position = new Vector2(previousPosition.x, previousPosition.y);
    }
}
