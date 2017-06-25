using UnityEngine;
using System.Collections;

public class ControlesCamera : MonoBehaviour {

    [SerializeField]
    private Transform persoTransform;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    public float movementCameraX;

    [SerializeField]
    private Transform extremiteDroite;

    private bool onBloque =false;
    private bool cameraMoveUp = false;
    private bool cameraMoveDown = false;

    private float PreviousPersoX;
    private float PreviousPersoY;
    private float PreviousCameraY;

    private float retardCamX = 0.0f;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start ()
    {
        PreviousPersoY = persoTransform.position.y;
        PreviousCameraY = cameraTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Suivi Y

        var persoPosition = persoTransform.position;
        var cameraPosition = cameraTransform.position;

        var posYcible = persoPosition.y + 4.0f;
        
        var posXfinal = persoPosition.x;
        var posYfinal = cameraPosition.y + (posYcible - cameraPosition.y) * 0.001f;
        
        if (persoPosition.y + 4.0f < cameraTransform.position.y) {
            posYfinal = persoPosition.y + 4.0f;
        }
        if (persoPosition.y - 4.0f > cameraTransform.position.y) {
            posYfinal = persoPosition.y - 4.0f;
        }

        cameraTransform.position = new Vector3(posXfinal, posYfinal, cameraPosition.z);

        //

        CameraBoundary();
        //SoftTravelling();

        // Rotation

        var retardRefX = persoPosition.x;

        retardCamX = retardCamX + (retardRefX - retardCamX) * 0.01f;

        if (persoPosition.x - retardCamX > 14.0f) retardCamX = persoPosition.x - 14.0f;
        if (persoPosition.x - retardCamX < -14.0f) retardCamX = persoPosition.x + 14.0f;

        var rotationY = -(retardCamX - persoPosition.x) * 1.0f;

        if (rotationY > 20.0f) rotationY = 20.0f;
        if (rotationY < -20.0f) rotationY = -20.0f;

        cameraTransform.rotation = Quaternion.Euler(0, rotationY, 0);
        cameraTransform.position = new Vector3(retardCamX - retardRefX + cameraTransform.position.x, cameraTransform.position.y, cameraPosition.z);

    }

    void CameraBoundary()
    {
        var persoPosition = persoTransform.position;
        var cameraPosition = cameraTransform.position;

        if (!extremiteDroite) {
            cameraTransform.position = new Vector3(persoPosition.x + movementCameraX, cameraPosition.y, cameraPosition.z);
            return;
        }

        //var ExtremePositionCamera = extremiteDroite.position.x - 8f;

        if (extremiteDroite && extremiteDroite.position.x - persoTransform.position.x < 16f)
        {
            onBloque = true;
        }

        if (onBloque && extremiteDroite.position.x - persoTransform.position.x < 18f)
        {
            cameraTransform.position = new Vector3(extremiteDroite.position.x - 8f, cameraPosition.y, cameraPosition.z);
        }

        if(!onBloque) cameraTransform.position = new Vector3(persoPosition.x + movementCameraX, cameraPosition.y, cameraPosition.z);

        if (extremiteDroite.position.x - persoTransform.position.x >= 18f)
        {
            onBloque = false;
        }
    }

    void SoftTravelling()
    {
        var persoPosition = persoTransform.position;
        var cameraPosition = cameraTransform.position;

        if (persoPosition.y > PreviousPersoY + 5.75f && cameraMoveDown == false)
        {
            cameraMoveUp = true;
            PreviousPersoY += 5.75f;
        }

        if (persoPosition.y < PreviousPersoY - 5.75f && cameraMoveUp == false)
        {
            cameraMoveDown = true;
            PreviousPersoY -= 5.75f;
        }

        if (cameraMoveDown)
        {
            cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y - (5.75f / 30), cameraPosition.z);
            if (cameraPosition.y <= PreviousCameraY - 5.75f)
            {
                cameraMoveDown = false;
                PreviousCameraY -= 5.75f;
            }
        }
        else if (cameraMoveUp)
        {
            cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y + (5.75f / 30), cameraPosition.z);
            if (cameraPosition.y >= PreviousCameraY + 5.75f)
            {
                cameraMoveUp = false;
                PreviousCameraY += 5.75f;
            }

        } 

        
    }
}
