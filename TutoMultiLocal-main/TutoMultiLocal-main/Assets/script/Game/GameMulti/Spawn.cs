using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : Spawn.cs
//Date    : 26.09.2022
//Version : Alpha
public class Spawn : MonoBehaviour
{


    public GameObject powerUp;
    public float Radius = 1;
    GameMananger gm;
    public GameObject explosionAlea;
    public bool fullMapBuff = false;

    public bool mapBuffActivated = true;
    bool waitInvok;

    private void Update()
    {
        //if ()
        //{
        //    InvokeRepeating("SpawnRandomBuff", 1, 1);

        //}
     

        if (gm.gameSpeed <= 0 && waitInvok && mapBuffActivated)
        {
         
            CancelInvoke("SpawnRandomBuff");
            waitInvok = false;
        }
        else if(!waitInvok && gm.gameSpeed >= 1 && mapBuffActivated)
        {
            CancelInvoke("SpawnRandomBuff");
            InvokeRepeating("SpawnRandomBuff", 1, 7);
            waitInvok = true;
        }
   
    }

    private void Awake()
    {

     string validationItem = PlayerPrefs.GetString("itemActif");
     
        gm = GameObject.Find("GameManager").GetComponent<GameMananger>();
        if (validationItem == "Yes")
        {
            mapBuffActivated = true;
        }
        else
        {
            mapBuffActivated = false;
        }

    }
    private void Start()
    {
        if (fullMapBuff == false)
        {
            if (mapBuffActivated)
            {
                
            InvokeRepeating("SpawnRandomBuff", 1, 7);
            }
        }        
    }



    /// <summary>
    /// instancie les items dans la map de maniere aléatoire en fonction du gizmo
    /// </summary>
    void SpawnRandomBuff()
    {
        Vector3 randomPos = Random.insideUnitCircle * Radius;
        Instantiate(powerUp,randomPos, Quaternion.identity);
    }

    /// <summary>
    /// fait apparaitre des explosion de maniere aleatoire (pas fonctionelle)
    /// </summary>
    void explosion()
    {
        Vector3 randomExplosion = Random.insideUnitCircle * Radius;
        Instantiate(explosionAlea, randomExplosion, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);   
    }
}
