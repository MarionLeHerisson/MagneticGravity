using UnityEngine;
using System.Collections;

public class ClimbAction : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D persoTransform;

    [SerializeField]
    private float intensiteY;

    private bool alreadyClimb = true;
    private int blocTouch;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (!enabled) return;
        if (blocTouch == 0)
        {
            alreadyClimb = false;
        }

        ++blocTouch;
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (!enabled) return;
        --blocTouch;

        if (blocTouch == 0)
        {
            alreadyClimb = true;
            Debug.Log(alreadyClimb);

        }

        else if (blocTouch > 0)
        {
            alreadyClimb = false;
            Debug.Log(alreadyClimb);
        }

    }

    private void Climb()
    {

        var deltaClimb = intensiteY * Time.deltaTime * 100;

        if (alreadyClimb == false)
        {
            persoTransform.velocity = new Vector2(0f, deltaClimb);
        }

    }


    void FixedUpdate()
    {
        Climb();
    }


}