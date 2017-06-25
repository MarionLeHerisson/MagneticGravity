using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball_Respawn : MonoBehaviour
{

    [SerializeField]
    private Transform ballOriginPosition;

    [SerializeField]
    private Tourelle_Ball_Exposer[] listBall;

    private Queue<Tourelle_Ball_Exposer> availableBalls;

    public void Init()
    {
        availableBalls = new Queue<Tourelle_Ball_Exposer>(listBall.Length);
        foreach(var ball in listBall)
        {
            ResetBall(ball);
        }
    }

    public void ResetBall(Tourelle_Ball_Exposer ball)
    {
        ball.BallPosition.position = ballOriginPosition.position;
        ball.gameObject.SetActive(false);
        availableBalls.Enqueue(ball);
    }

    public Tourelle_Ball_Exposer UseBall()
    {
        if(availableBalls == null)
        {
            Init();
        }

        return availableBalls.Dequeue();
    }
}
