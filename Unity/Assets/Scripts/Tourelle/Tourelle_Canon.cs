using UnityEngine;
using System.Collections;

public class Tourelle_Canon : MonoBehaviour {

    [SerializeField]
    private Transform target;
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(target);
    }

}
