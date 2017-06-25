using UnityEngine;
using System.Collections;

public class BlockMagneticMov : MonoBehaviour
{
    [SerializeField]
    private int length;

    [SerializeField]
    private Transform chaine;

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private Rigidbody2D characterBody;

    [SerializeField]
    private bool departADroite;

    private bool Active = false;

    private bool Inside = false;

    private void Start()
    {
        if (departADroite) this.transform.localPosition = new Vector3(length*3, 0, 0);
        chaine.transform.localScale = new Vector3(length, 1, 1) ;
    }

    private void Update()
    {
        var InitialPosition = this.transform.localPosition;
        var ParentPosition = this.transform.parent.position;
        var CharaPosition = character.transform.localPosition;

        if(departADroite)
        {
            if (Active && Inside && InitialPosition.x > 0)
            {
                this.transform.localPosition = new Vector3(InitialPosition.x - .2f, 0, 0);

                if (CharaPosition.y < ParentPosition.y - 2.5)
                {
                    characterBody.velocity = new Vector2(0, 2.0f * Utils.facteurTemps );
                }
            }
        }

        else
        {
            if (Active && Inside && InitialPosition.x < length * 3)
        {
            this.transform.localPosition = new Vector3(InitialPosition.x + .2f, 0, 0);

            if (CharaPosition.y < ParentPosition.y - 2.5)
            {
                characterBody.velocity = new Vector2(0, 2.0f * Utils.facteurTemps );
            }
        }
        }
        
    }

    private void OnMouseDown()
    {
        Active = true;
    }

    private void OnMouseUp()
    {
        Active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Inside = false;
    }
}
