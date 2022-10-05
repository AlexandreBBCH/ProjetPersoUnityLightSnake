using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine.UI;
using System;

//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : Player.cs
//Date    : 26.09.2022
//Version : Alpha
public class Player : MonoBehaviour
{
  
    //Variable gerant le joueur et ces intéraction
    Rigidbody2D rb;
    public int speed; 
    public Vector2 dir;
    GameObject wallPrefab;// mur a instancier
    Vector2 lastPos;
    Collider2D lastWallCol;
    PowerUp effect;
    ActivatePlayers ActivateP;
    float wallWidth = 1;
    public GameObject boomParticles;
    bool canActivateBoost = true;
    public string playerName;
    Cam cam;
    GameMananger gm;
    Menu menu;
    [HideInInspector]
    public bool isAlive = true;
    public int boost = 1;
    //public SpriteRenderer wallColor;
    Vector2 initialPos;
    Vector2 dirRight;
    public LayerMask layerToGiveToWall;
    public bool invert= false;
    public bool inBoost = false;
    public Vector2 saveDirectionRight;
    int missilePosX;
    int missilePosY;
    public int score = 0;
    //A activé quand les IA auront été codé
    public bool isAbot = false;
 
    // Lien avec le missile
    public GameObject Missile;
    public int shotMissile = 1;

    //gestion maintient touche
    float time;
    bool timerStart = false;
    bool verifBoost = false;
    int tempsAttente = 120;
    public bool waitAction = false;

    //a activé en test
    public bool immortal = false;

    //affichage
    public Sprite oldPoweUpBoost;
    public Sprite oldIconStop;
    public Sprite oldIconInvert;
    public Sprite oldIconInfinityShot;
    public Sprite oldIconApocalypse;
    int nbPlayers;
    int playerAlive;
    public GameObject[] allBoost;
    public GameObject[] allShot;
    public LayerMask layer;
    //public GameObject head;
    public int nbRound;
    public GameObject menuFinPartie;
    public Text title;
    public int life;
    public Text p1FinalScore;
    public Text p2FinalScore;
    public Text p3FinalScore;
    public Text p4FinalScore;

    public int finalLife;
    
    //gestion de bug
    bool scoreAddedTimer = false;



    Vector2[] direction = new Vector2[] { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
    public Vector2 getRandomDir()
    {
        return direction[UnityEngine.Random.Range(0, direction.Length)];
    }


    //se lance au lancement de la scene ou se trouve ce scripte
    void Start()
    {
       //Debug.Log( getRandomDir());


        if (gm.modeScore)
        {
            nbPlayers = PlayerPrefs.GetInt("nbPlayers");
        }
        if (gm.modeLife)
        {
            nbPlayers = PlayerPrefs.GetInt("nbLifePlayers");
          
            int playerAlive = nbPlayers;
        }
     
        initialPos = transform.position;
        dir = getRandomDir();
        //Debug.Log(dir);
       //Debug.Log(ActivateP.getRandomDir());
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed * gm.gameSpeed;
        wallPrefab = Resources.Load("Wall"+gameObject.tag) as GameObject;
        //head = Resources.Load("head" + playerName) as GameObject;

      

        //affichage en mode life
        if (gm.modeLife)
        {
            life = PlayerPrefs.GetInt("nbLife");
            if (life <= 100)
            {
                title.text = "Light Snake " + life + " Life";
            }
            else
            {
                title.text = "Light Snake infinity Life";
                GameObject.Find("Life" + playerName).GetComponent<Text>().text = "Immortal ";
            }
         

        }

        //affichage en mode score
        if (gm.modeScore)
        {

       
         nbRound = PlayerPrefs.GetInt("nbRound");
        if (nbRound <= 100)
        {
            title.text = "Light Snake " + gm.round + "/" + nbRound;
        }
        else
        {
            title.text = "Light Snake Ininity";
        }
        }

    }



    //se lance au lancement de la scene ou se trouve ce scripte (avant start)
    private void Awake()
    {
  
        GameObject.Find("Boost" + playerName).GetComponent<Text>().text = "Boost : " + boost;
        GameObject.Find("Shot" + playerName).GetComponent<Text>().text = "Shot : " + shotMissile;
        cam = Camera.main.GetComponent<Cam>();//fait le lien avec le script "Cam"
        gm = GameObject.Find("GameManager").GetComponent<GameMananger>();
    }

    // Un update qui se fait en décallé
    void LateUpdate()
    {
        missilePosX = (int)dir.x;
        missilePosY = (int)dir.y;
        PlayerPrefs.SetInt("MissilePosX" + playerName, (missilePosX));
        PlayerPrefs.SetInt("MissilePosY" + playerName, (missilePosY));
        if (invert == false)
        {
            HandheldKeys();
        }
        rb.velocity = dir * speed * gm.gameSpeed;
        SetLastWallSize(lastWallCol, lastPos, transform.position);
    }


    private void Update()
    {

        if (timerStart)
        {
            time++;
            
        }
            //activation du boost en fonction du temps
            if (time >= tempsAttente)
            {
           
            if (canActivateBoost)
            {
                verifBoost = true;
                StartCoroutine("ActivateBoost");
                GameObject.Find("Boost" + playerName).GetComponent<Text>().text = "Boost : " + boost;
                timerStart = false;
                time = 0;
               
            }
       
        }


    }


    //en fonction d'ou regarde le joueur le missile est instancié
    public void shootDirection()
    {

        if (dir.x == 1 && dir.y == 0)
        {
            Instantiate(Missile, new Vector2(transform.position.x + 1.5f, transform.position.y), Quaternion.identity); //tir droite
        }
        if (dir.x == 0 && dir.y == 1)
        {
            Instantiate(Missile, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);// tir haut
        }
        if (dir.x == -1 && dir.y == 0)
        {
            Instantiate(Missile, new Vector2(transform.position.x - 1.5f, transform.position.y), Quaternion.identity);//tir gauche -1,0
        }
        if (dir.x == 0 && dir.y == -1)
        {
            Instantiate(Missile, new Vector2(transform.position.x, transform.position.y - 1.5f), Quaternion.identity); // tir bas
                                                                                                                    //tir haut 0,-1
        }
     
    }

    //se declanche au moment du toucher du mur si on a activer la fonctionalité "on trigger"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != lastWallCol && collision.gameObject.tag != "PowerUpBoost" )
        {
            isAlive = false;
            gm.KillPlayer();
            cam.Shake(0.7f,0.4f,50f);
            cam.PlayBoumSfx();
            Instantiate(boomParticles, transform.position, Quaternion.identity);        
            gameObject.SetActive(false);
            speed = 5;
            canActivateBoost = true;
            if (gm.modeLife)
            {
                life--;
                GameObject.Find("Life"+playerName).GetComponent<Text>().text = "Life "+ life;
            }

        }
        
    }

    //c' est ici quon gerera les touche
   public void HandheldKeys()
    {
        if (isAbot == false && gm.gameSpeed >= 1)
        {

       
        if (Input.GetButtonDown(playerName+"UP"))
        {
            if (dir != Vector2.down)
            {
            dir = Vector2.up;
                CreateWall();
           
            }
        }
        if (Input.GetButtonDown(playerName+"DOWN"))
        {
            if (dir != Vector2.up)
            {
                dir = Vector2.down;
                CreateWall();
            }
        }
        if (Input.GetButtonDown(playerName+"LEFT"))
        {
            if (dir != Vector2.right)
            {
                dir = Vector2.left;
                CreateWall();
            }
        }
        if (Input.GetButtonDown(playerName+"RIGHT"))
        {
            if (dir != Vector2.left)
            {
                dir = Vector2.right;
                CreateWall();
            }
        }
        if (Input.GetButtonDown(playerName + "BOOST"))
        {
            //timer start a l appuie
            timerStart = true;


        }

            if (Input.GetButtonUp(playerName+"BOOST"))
        {

            if (time <= tempsAttente)
            {

                if (verifBoost == false)
                {
                    if (shotMissile >= 1)
                    {
                        shootDirection();
                        shotMissile--;
                        GameObject.Find("Shot" + playerName).GetComponent<Text>().text = "Shot : " + shotMissile;
                        
                    }

                    timerStart = false;
                    time = 0;
                }
                else
                {
                    verifBoost = false;
                }
              
            }
            timerStart = false;
            time = 0;


        }
      
        }
 
        rb.velocity = dir * speed * gm.gameSpeed;
    }



    public int giveRandom(int min, int max)
    {
        int aleaNb = UnityEngine.Random.Range(min, max + 1);

        return aleaNb;
    }

  



    //fonction pour savoir dans quel direction se situe la droite du joueur

    void CalculateRight()//calcule la direction droite en fonction du personnage
    {
        if (dir.x == 0 && dir.y == 1)
        {
            dirRight = Vector2.right;
           
        }
        else if (dir.x == 1 && dir.y == 0)
        {
            dirRight = Vector2.down;
     
        }
        else if (dir.x == -1 && dir.y == 0)
        {
            dirRight = Vector2.up;
         
        }
        else 
        {
            dirRight = Vector2.left;
          

        }
    
    }


    //c est ici quon gere le boost du personnage
    IEnumerator ActivateBoost()
    {
        if (boost >= 1 && gm.gameSpeed >= 1)
        {  
        canActivateBoost = false;
        speed += 5;
        boost--;
        GameObject.Find("Boost" + playerName).GetComponent<Text>().text = "Boost : " + boost;
        yield return new WaitForSeconds(3);
        speed -= 5;
        Invoke("ReloadBoost",1);
            if (inBoost == false)
            {
                speed = 10;
            }
        }
    }

    void ReloadBoost()
    {
        canActivateBoost = true;
    }

    //c est ici quon instancie le mur durant nos changement de direction
    public void CreateWall()
    {
        if (immortal == false)
        {
            lastPos = transform.position; // recupere la pos du joueureur au moment du changement
            GameObject go = Instantiate(wallPrefab, transform.position, Quaternion.identity);
            if (lastWallCol != null)
            {
                lastWallCol.gameObject.layer = 8;//player
            }
            lastWallCol = go.GetComponent<Collider2D>();
        }
    }

    //c est ici qu on calcul la taille du mur
    private void SetLastWallSize(Collider2D col, Vector2 posStart, Vector2 posEnd)
    {

        if (immortal == false)
        {

     
        if (col)
        {
        col.transform.position = posStart + (posEnd - posStart)/2;
        float size = Vector2.Distance(posEnd, posStart);
        if (posStart.x != posEnd.x)
        {
            col.transform.localScale = new Vector2(size + wallWidth, wallWidth);
        }
        else
        {
            col.transform.localScale = new Vector2(wallWidth,size + wallWidth);
        }
      } 
        }

    }

    //c est ici que l'ont reset l'affichage des icons d'objets
    public void resetIcon()
    {
        GameObject.Find("iconPowerUpBoost" + playerName).GetComponent<Image>().sprite = oldPoweUpBoost;
        GameObject.Find("iconStop" + playerName).GetComponent<Image>().sprite = oldIconStop;
        GameObject.Find("iconApocalypse" + playerName).GetComponent<Image>().sprite = oldIconApocalypse;
        GameObject.Find("iconInfinityShot" + playerName).GetComponent<Image>().sprite = oldIconInfinityShot;
        GameObject.Find("iconInvert" + playerName).GetComponent<Image>().sprite = oldIconInvert;
    }
 
    //c'est ici que l on reset la map apres chaque partie
    public void clearDisplay()
    {

        allShot = GameObject.FindGameObjectsWithTag("shot");
        allBoost = GameObject.FindGameObjectsWithTag("boost");

        for (int i = 0; i < allShot.Length; i++)
        {
            Destroy(allShot[i]);
        }
        for (int j = 0; j < allBoost.Length; j++)
        {
            Destroy(allBoost[j]);
        }
    }





    //c est ici que l on verifie quand la partie se finis



   


 
    //fonction qui empeche l'ajout en trop en cas de bug de point
    IEnumerator AddScore()
    {
        if (!scoreAddedTimer)
        {

            score++;
            scoreAddedTimer = true;
            yield return new WaitForSeconds(2);
            scoreAddedTimer = false;
        }
    }


    //verifie si le joueur meurt
    public bool isDead()
    {
        if (gm.modeLife && life <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    //remet a 0 les attribut du joueur
    public void ResetPlayer()
    {
    
        if (gm.modeScore)
        {
            StartCoroutine(AddScore());
          
        }
  
        resetIcon();
        invert = false;
        waitAction = false;
        boost = 1;
        shotMissile = 1;
        GameObject.Find("Boost" + playerName).GetComponent<Text>().text = "Boost : " + boost;
        GameObject.Find("Shot" + playerName).GetComponent<Text>().text = "Shot : " + shotMissile;
        transform.position = initialPos;
      

        //gestion mode vie


        if (isDead())
        {
            isAlive = false;

            //Debug.Log(playerName);
        
            //Debug.Log(nbPlayers);
        }
        else
        {
            isAlive = true;
        }
        dir = getRandomDir();
        //dir = ActivateP.getRandomDir();
        speed = 10;
        transform.localScale = new Vector2(1,1);
        immortal = false;
        clearDisplay();



        if (gm.modeLife)
        {
            foreach (var item in playerName)
            {
                //Debug.Log(item);
            }
            //if (playerAlive <= 1) { menuFinPartie.SetActive(true); };
        }

        //to do afficher la fin du round afficher le vainqueur et retour au lobby apres 4 sec

  


    }




}
