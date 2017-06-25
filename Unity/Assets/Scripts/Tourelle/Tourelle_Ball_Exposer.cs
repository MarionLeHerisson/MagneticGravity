using UnityEngine;
using System.Collections;

public class Tourelle_Ball_Exposer : MonoBehaviour
{

    [SerializeField]
    private Transform ballPosition;

    public Transform BallPosition
    {
        get { return ballPosition; }
        set { BallPosition = value; }
    }
}
