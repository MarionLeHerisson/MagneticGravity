using UnityEngine;
using System.Collections;

public class BlockEnd : MonoBehaviour
{

    [SerializeField]
    private ControlesPerso controlesPerso;

    [SerializeField]
    private GameObject panelSuccess;

    [SerializeField]
    private Tourelle_ShootSpeed[] tourelle_Shoot;

    [SerializeField]
    private Ball_Respawn[] tourelle;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!enabled) return;

        foreach (var tour in tourelle) {
            tour.Init();
        }

        foreach (var tour_shoot in tourelle_Shoot) {
            tour_shoot.Desactivation(false);
        }

        controlesPerso.estArrete = true;
        panelSuccess.SetActive(true);

        Utils.FinishLevel();
    }
}
