using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{

    Player p;
    public LayerMask layer;
    [Range(0.5f, 1.0f)]
    public float intelligence;
    public float randomMax = 0.3f;


    private void Start()
    {
        p = GetComponent<Player>();
        //on fait tourner en boucle notre fonction décision
        InvokeRepeating("TakeDecision", Random.Range(0.2f, 1.5f), intelligence);
        //InvokeRepeating("Urgence", Random.Range(0.2f, 1.5f), intelligence);

    }


    void Urgence()
    {
        //fait un truc si ya un mur a moins de 2 unité en ignorant tout le reste
    }
   public void TakeDecision()
    {
        //pour stocker la distance des murs de gauche, droite et en face
        RaycastHit2D forwardHit;
        RaycastHit2D rightHit;
        RaycastHit2D leftHit;


        //calcul de la direction droite par rapport a en face
        Vector2 dirRight = p.GetRightDir();

        //on envoie des rayon dans les 3 direction en ne prenant pas en compte les layer egaux au notre soit "player"
        forwardHit = Physics2D.Raycast(transform.position, p.dir, Mathf.Infinity, ~layer);
        rightHit = Physics2D.Raycast(transform.position,dirRight, Mathf.Infinity, ~layer);
       leftHit = Physics2D.Raycast(transform.position, -dirRight, Mathf.Infinity, ~layer);

        //si ya un mur devant nous

        if (forwardHit.distance < 1)
        {
            //on tourne ou ya le plus d'espace


            if (leftHit.distance > rightHit.distance)
            {
                p.TurnLeft();
            }
            else
            {
                p.TurnRight();
            }

        }
        else
        {
            if(Random.value < Random.Range(.1f, randomMax))
            {


                if (leftHit.distance > 1)
                {

                }
                else if(rightHit.distance > 1)
                {
                    p.TurnRight();
                }
            }
        }



    }


}
