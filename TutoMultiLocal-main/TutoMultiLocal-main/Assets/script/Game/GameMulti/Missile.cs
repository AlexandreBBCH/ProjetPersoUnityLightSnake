using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : Missile.cs
//Date    : 26.09.2022
//Version : Alpha
public class Missile : MonoBehaviour
{



    Rigidbody2D rb;
    public int speed;
    Vector2 direction;
    Player player;
    Cam cam;
    public GameObject boomParticles;
    GameMananger gm;
    public string playerName;

    public AudioClip boomSfx;
    int directionMissileX;
    int directionMissileY;

    //cest ici que l ont joue le son de l impact
    public void PlayBoumSfx()
    {
        GetComponent<AudioSource>().volume = (float)PlayerPrefs.GetInt("volumeBruitage") / 100;
        GetComponent<AudioSource>().PlayOneShot(boomSfx);
    }

    private void Awake()
    {
        directionMissileX = PlayerPrefs.GetInt("MissilePosX" + playerName);
        directionMissileY = PlayerPrefs.GetInt("MissilePosY" + playerName);
    }

    private void Start()
    {
        //valeur par defaut
  
        cam = Camera.main.GetComponent<Cam>();
        rb = GetComponent<Rigidbody2D>();
        shot();

    }


    //gere la rapidité du missile
    private void LateUpdate()
    {

       
        if (direction != null)
        {
       
            rb.velocity = direction * speed;

        }
       
        //rb.velocity = direction * speed * gm.gameSpeed;
    }





    //tir en fonction de la direction du regard du joueur
    void shot()
    {
    

        if (directionMissileX == 1 && directionMissileY == 0)
            {
         
                direction = Vector2.right;
            }
            if (directionMissileX == 0 && directionMissileY == -1)
        {
        
            direction = Vector2.down;
            }
            if (directionMissileX == 0 && directionMissileY == 1)
        {
          
            direction = Vector2.up;
            }
            if (directionMissileX == -1 && directionMissileY == 0)
        {
           
            direction = Vector2.left;
            }

        
     
    }


    //Detruit un mur au contact
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag != "bg" && collision.gameObject.tag != "border")
        {
      
            cam.PlayBoumSfx();
            cam.Shake(0.75f, 2f, 30);
            collision.gameObject.SetActive(false);
            Instantiate(boomParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            PlayBoumSfx();

        }
        else
        {
        
            cam.PlayBoumSfx();
            cam.Shake(0.75f, 2f, 30);
            Instantiate(boomParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            PlayBoumSfx();
     
        }



        //Destroy(collision.gameObject);
    }

    //empeche le spam de missile
    IEnumerator wait(int sec)
    {
        Debug.Log("att" + sec);
        yield return new WaitForSeconds(sec);
    }

}


