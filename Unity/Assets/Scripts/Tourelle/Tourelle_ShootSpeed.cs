using UnityEngine;
using System.Collections;

public class Tourelle_ShootSpeed : MonoBehaviour {

    [SerializeField]
    private Ball_Respawn ballUsed;

    [SerializeField]
    [Range(1, 3)]
    private int ballsByShot;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float waitBetweenBalls;

    [SerializeField]
    [Range(0.5f,5f)]
    private float waitBetweenShoot;

    [SerializeField]
    private bool OnBattleField;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(ShootCoroutine());
    }

    public void SetBallsByShot(int nb) {
        if (nb < 1) nb = 1;
        if (nb > 3) nb = 3;
        ballsByShot = nb;
    }

    public void SetWaitBetweenBalls(float nb) {
        if (nb < 0.2f) nb = 0.2f;
        if (nb > 2f) nb = 2f;
        waitBetweenBalls = nb;
    }

    public void SetWaitBetweenShoot(float nb) {
        if (nb < 0.5f) nb = 0.5f;
        if (nb > 5f) nb = 5f;
        waitBetweenShoot = nb;
    }

    public int GetBallsByShot() {
        return ballsByShot;
    }

    public float GetWaitBetweenBalls() {
        return waitBetweenBalls;
    }

    public float GetWaitBetweenShoot() {
        return waitBetweenShoot;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnBattleField)
        {
            OnBattleField = false;
        }

        else
        {
            OnBattleField = true;
        }
    }

    public void Desactivation(bool Activation)
    {
        OnBattleField = Activation;
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (OnBattleField)
            {
                for (int i = 0; i < ballsByShot; i++)
                {
                    var ballUsing = ballUsed.UseBall();
                    ballUsing.gameObject.SetActive(true);
                    yield return new WaitForSeconds(waitBetweenBalls / Utils.facteurTemps);
                }

                yield return new WaitForSeconds(waitBetweenShoot / Utils.facteurTemps);
            }
            yield return new WaitForSeconds(0);
        }
        
    }
}
