using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Tourelle_Ball_ExposerUnityEvent : UnityEvent<Tourelle_Ball_Exposer>
{

}

public class Tourelle_Ball : MonoBehaviour
{

    [SerializeField]
    private bool polariteNegative;

    [SerializeField]
    [Range(1f, 10f)]
    private float lifeTime;

    [SerializeField]
    [Range(0.1f, 0.5f)]
    private float ballSpeed;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private ControlesPerso controlesPerso;

    [SerializeField]
    private TextMesh polariteText;

    [SerializeField]
    private Transform checkpointPosition;

    [SerializeField]
    private Ball_Respawn ballUsed;

    [SerializeField]
    private Tourelle_Ball_Exposer tourelleBallExposer;

    [SerializeField]
    private Tourelle_Ball_ExposerUnityEvent ballsManagement;

    [SerializeField]
    private Tourelle_ShootSpeed[] tourelle_Shoot;

    [SerializeField]
    private Ball_Respawn[] tourelle;

    /*private bool PolariteDepart;

    void PolariteDepartAttribut()
    {
        if (polariteNegative == true) PolariteDepart = true;
        else PolariteDepart = false;
    }*/

    void Start()
    {
        //PolariteDepartAttribut();
        MajPolariteText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(BallDied());

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, ballSpeed * Utils.facteurTemps * Time.deltaTime / 0.02f);
        MajPolariteText();
    }

    void MajPolariteText()
    {
        if (polariteNegative == true) polariteText.text = "-";
        else polariteText.text = "+";
    }

    public void OnMouseDown()
    {
        if (polariteNegative == true)
        {
            polariteNegative = false;
            MajPolariteText();
        }
        else
        {
            polariteNegative = true;
            MajPolariteText();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        //Regarde si le personnage doit mourir ou non
        if (polariteNegative != controlesPerso.polariteNegative)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);

            /*var positionCheckpoint = checkpointPosition.position;

            target.position = new Vector2(positionCheckpoint.x, positionCheckpoint.y + 2f);
            Debug.Log("T'es Mort");
            controlesPerso.initialise();
            foreach (var tour in tourelle)
            {
                if (PolariteDepart == true) polariteNegative = true;
                else polariteNegative = false;
                MajPolariteText();
                tour.Init();
            }

            foreach (var tour_shoot in tourelle_Shoot)
            {
                tour_shoot.Desactivation(false);
            }*/
        }

        else ballsManagement.Invoke(tourelleBallExposer);
    }

    IEnumerator BallDied()
    {

        yield return new WaitForSeconds(lifeTime);
        ballUsed.ResetBall(tourelleBallExposer);

    }
}