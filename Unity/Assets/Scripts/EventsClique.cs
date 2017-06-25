using UnityEngine;
using System.Collections;

public class EventsClique : MonoBehaviour {

    [SerializeField] Camera came;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //test clic gauche

        if (Input.GetMouseButtonDown(0)) {
            
            var ray = came.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {

                // hitInfo.point;

            }
        }


    }
}
