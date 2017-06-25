using UnityEngine;
using System.Collections;

public class BlockReverse : MonoBehaviour
{

    [SerializeField]
    private GameObject persoGameObject;

    [SerializeField]
    private Camera cameraTarget;

    private ControlesPerso persoControles;
    private Transform persoTransform;
    private ControlesCamera cameraControles;

    private bool camSwap=false;
    float PreviousMovCamX;

    // Use this for initialization
    void Start()
    {
        persoControles = persoGameObject.GetComponent<ControlesPerso>();
        persoTransform = persoGameObject.GetComponent<Transform>();
        cameraControles = cameraTarget.GetComponent<ControlesCamera>();
        PreviousMovCamX = cameraControles.movementCameraX;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (camSwap && cameraControles.movementCameraX > -PreviousMovCamX)
        {
            cameraControles.movementCameraX -= 0.5f;
            

            if (cameraControles.movementCameraX <= -PreviousMovCamX)
            {
                PreviousMovCamX = -PreviousMovCamX;
                cameraControles.movementCameraX = PreviousMovCamX;
                camSwap = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {

        if (!enabled) return;
        persoControles.vitesseInitX = -persoControles.vitesseInitX;
        persoTransform.Rotate(0,180,0);
        camSwap = true;

        if (persoTransform.position.x > this.transform.position.x && persoControles.vitesseInitX < 0.0f) {

            persoTransform.position = new Vector3(persoTransform.position.x - 1.0f, persoTransform.position.y, persoTransform.position.z);
        }
        if (persoTransform.position.x < this.transform.position.x && persoControles.vitesseInitX > 0.0f) {

            persoTransform.position = new Vector3(persoTransform.position.x + 1.0f, persoTransform.position.y, persoTransform.position.z);
        }
    }
}