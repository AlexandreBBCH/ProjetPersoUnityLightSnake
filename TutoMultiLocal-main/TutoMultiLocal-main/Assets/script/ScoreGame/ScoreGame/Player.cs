using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public int speed; 
    public Vector2 dir;
    GameObject wallPrefab;// mur a instancier
    Vector2 lastPos;
    Collider2D lastWallCol;
    PowerUp effect;
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

    public bool isAbot = false;
 

    public GameObject Missile;
    public int shotMissile = 1;

    //gestion maintient touche
    float time;
    bool timerStart = false;
    bool verifBoost = false;
    int tempsAttente = 120;  
    
    public bool immortal = false;

  
    public Sprite oldPoweUpBoost;
    public Sprite oldIconStop;
    public Sprite oldIconInvert;
    public Sprite oldIconInfinityShot;
    public Sprite oldIconApocalypse;
    int nbPlayers;
    int playerAlive ;
    public GameObject[] allBoost;
    public GameObject[] allShot;

    //public GameObject head;

    public bool waitAction = false;




    IA bot;
    public LayerMask layer;
    RaycastHit2D hit;



    RaycastHit2D checkBufUp;
    RaycastHit2D checkBufDown;
    RaycastHit2D checkBufRight;
    RaycastHit2D checkBufLeft;

    public float detectionBuf = 20f;

    public int nbRound;
    public GameObject menuFinPartie;
    public Text title;

    public int life;

    void Start()
    {
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
        dir = Vector2.up;//Direction par defaut
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed * gm.gameSpeed;
        wallPrefab = Resources.Load("Wall"+gameObject.tag) as GameObject;
        //head = Resources.Load("head" + playerName) as GameObject;
        if (isAbot == true)
        {
            //if (waitAction == false)
            //{
            //    InvokeRepeating("choixDecision", 1, 1/10f);
            //}
        }



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


        if (gm.modeScore)
        {

       
         nbRound = PlayerPrefs.GetInt("nbRound");
        if (nbRound <= 100)
        {
            title.text = "Light Snake " + score + "/" + nbRound;
        }
        else
        {
            title.text = "Light Snake Ininity";
        }
        }

    }
    public RaycastHit2D top;
    public RaycastHit2D bottom;
    public RaycastHit2D left;
    public RaycastHit2D right;


    public float detectionWall = 1;
    public float detectionDanger = 2;

    public bool topDanger = false;
    public bool bottomDanger = false;
    public bool rightDanger = false;
    public bool leftDanger = false;


    private void Awake()
    {
        //GameObject.Find("PlayerBlue").GetComponent<Player>().enabled = true;
        //GameObject.Find("PlayerBlue").GetComponent<Player>().effect.inverserControl();

        //GameObject.Find("PlayerBleu").GetComponent<Player>().invert = true;
      

        GameObject.Find("Boost" + playerName).GetComponent<Text>().text = "Boost : " + boost;
        GameObject.Find("Shot" + playerName).GetComponent<Text>().text = "Shot : " + shotMissile;
        //recupéré tout les objet et appliquer l inversion a tous sauf le joueur qui le prend; 


        top = Physics2D.Raycast(transform.position, transform.up, detectionDanger, ~layer);
        cam = Camera.main.GetComponent<Cam>();//fait le lien avec le script "Cam"
        gm = GameObject.Find("GameManager").GetComponent<GameMananger>();

 
  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.DrawRay(new Vector2(transform.position.x,transform.position.y + 1), Vector2.up * detectionDanger, Color.white);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y -1 ), Vector2.down * detectionDanger, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x +1, transform.position.y ), Vector2.right * detectionDanger, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x -1, transform.position.y ), Vector2.left * detectionDanger, Color.green);
        //head.transform.position = lastPos;

        //Debug.Log(time);
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

        if (isAbot)
        {
                checkDangerRight();     
                checkDangerTop();       
                checkDangerBot();
                checkDangerLeft();       
        }


        //oublie de recheck
    }


    private void Update()
    {
        Debug.Log(PlayerPrefs.GetString("itemActif"));

        if (isAbot)
        {     
            TakeWallDecision();
            right = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, detectionDanger, ~layer);
            top = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, detectionDanger, ~layer);
            bottom = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, detectionDanger, ~layer);
            left = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, detectionDanger, ~layer);
            //si on va a droite pas besoin de verifier le danger venant de gauche pareil pour tout les autres
        }


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

    //public void haveChangeDirection()
    //{
    //    if (dir.x == 1 && dir.y == 0)
    //    {
    //        topDanger = false;
    //    }
    //    if (dir.x == 1 && dir.y == 0)
    //    {

    //    }
    //}

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
    
    private void OnTriggerEnter2D(Collider2D collision)//se declanche au moment du toucher du mur si on a activer la fonctionalité "on trigger"
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
           
            //Debug.Log(playerName);
            //gestion des coeur


        }
        
    }


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

    /// <summary>
    /// mettre ici le tracking des buff
    /// </summary>
    /// 






    //check danger
    //1,0 droite
    //-1,0 gauche


    public void checkDangerTop()
    {
        //verifie si il y a un danger en haut
 
        if (top.collider.tag == "border" || top.collider.tag == "mur")
        {
            topDanger = true;
            StartCoroutine("topSafe");
           
        }
    }

    IEnumerator topSafe()
    {
        yield return new WaitForSeconds(3);
        topDanger = false;

    }
    IEnumerator botSafe()
    {
        yield return new WaitForSeconds(3);
        bottomDanger = false;

    }
    IEnumerator rightSafe()
    {
        yield return new WaitForSeconds(3);
        rightDanger = false;

    }
    IEnumerator leftSafe()
    {
        yield return new WaitForSeconds(3);
        leftDanger = false;

    }

    public void checkDangerBot()
    {
        //verifie si il y a un danger en bas
        if (bottom.collider.tag == "border" || bottom.collider.tag == "mur")
        {
            bottomDanger = true;
            StartCoroutine("bottomSafe");
        }
    }
    public void checkDangerRight()
    {
        if (right.collider.tag == "border" || right.collider.tag == "mur")
        {
            rightDanger = true;
            StartCoroutine("rightSafe");

        }

    }
    public void checkDangerLeft()
    {
        //verifie si il y a un danger a gauche
        if (left.collider.tag == "border" || left.collider.tag == "mur")
        {
            leftDanger = true;
            StartCoroutine("leftSafe");

        }

    }
 

   




   public bool waitUp = false;
   public bool waitDown = false;
   public bool waitRight = false;
   public bool waitLeft = false;
   public bool waitGlobal = false;
    int wait = 1;
    public RaycastHit2D hitWall;

    public void TakeWallDecision()
    {

        if (dir.x == 1 && dir.y == 0)//si il va vers la droite
        {
     
            hitWall = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), transform.right, detectionWall, ~layer);

            if (hitWall.collider.tag == "border" || hitWall.collider.tag == "mur")
            {
          
                int alea = giveRandom(1, 2);
 
                if ((topDanger == false && bottomDanger == false && waitGlobal == false) || (topDanger == true && bottomDanger == true && waitGlobal == false))
                {
                    if (alea == 1 && waitUp == false && waitGlobal == false)
                    {
                
                        dir = Vector2.up;
                        CreateWall();
                        StartCoroutine("waitDirDown");
                        StartCoroutine("waitDirGlobal");
                        leftDanger = false;
                    }
                    if (alea == 2 && waitDown == false && waitGlobal == false)
                    {
                        dir = Vector2.down;
                        CreateWall();
                        StartCoroutine("waitDirUp");
                        StartCoroutine("waitDirGlobal");
                        leftDanger = false;
                    }

                }
                else if (topDanger == true && waitDown == false && waitGlobal == false)
                {

                    dir = Vector2.down;
                    CreateWall();
                    StartCoroutine("waitDirUp");
                    StartCoroutine("waitDirGlobal");
                    leftDanger = false;

                }
                else if (bottomDanger == true && waitUp == false && waitGlobal == false)
                {
                    dir = Vector2.up;
                    CreateWall();
                    StartCoroutine("waitDirDown");
                    StartCoroutine("waitDirGlobal");
                    leftDanger = false;
                }

            }
 

        }

        if (dir.x == -1 && dir.y == 0)//si il va a gauche
        {
    
      hitWall = Physics2D.Raycast(new Vector2(transform.position.x -1, transform.position.y), -transform.right, detectionWall, ~layer);
   
            if (hitWall.collider.tag == "border" || hitWall.collider.tag == "mur")
            {
          
                int alea2 = giveRandom(1, 2);
                if ((topDanger == false && bottomDanger == false && waitGlobal == false) || (topDanger == true && bottomDanger == true && waitGlobal == false))
                {
                    if (alea2 == 1 && waitUp == false && waitGlobal == false)
                    {
                        dir = Vector2.up;
                        CreateWall();
                        StartCoroutine("waitDirDown");
                        StartCoroutine("waitDirGlobal");
                        rightDanger = false;
                    }
                    if (alea2 == 2 && waitDown == false && waitGlobal == false)
                    {
                        dir = Vector2.down;
                        CreateWall();
                        StartCoroutine("waitDirUp");
                        StartCoroutine("waitDirGlobal");
                        rightDanger = false;
                    }

                }
                else if (topDanger == true && waitDown == false && waitGlobal == false)
                {

                    dir = Vector2.down;

                    CreateWall();
                    StartCoroutine("waitDirUp");
                    StartCoroutine("waitDirGlobal");
                    rightDanger = false;
                }
                else if (bottomDanger == true && waitUp == false && waitGlobal == false)
                {
                    dir = Vector2.up;
                    CreateWall();
                    StartCoroutine("waitDirDown");
                    StartCoroutine("waitDirGlobal");
                    rightDanger = false;
                }

            }


        }

   
        if (dir.x == 0 && dir.y == -1)//si il va en bas
        {  
        
            hitWall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), -transform.up, detectionWall, ~layer);

            if (hitWall.collider.tag == "border" || hitWall.collider.tag == "mur")
            {
                
                int alea3 = giveRandom(1, 2);
                if ((rightDanger == false && leftDanger == false && waitGlobal == false) || (rightDanger == true && leftDanger == true && waitGlobal == false))
                {
                    if (alea3 == 1 && waitRight == false && waitGlobal == false)
                    {
                        dir = Vector2.right;
                        CreateWall();
                        StartCoroutine("waitDirLeft");
                        StartCoroutine("waitDirGlobal");
                        topDanger = false;
                    }
                    if (alea3 == 2 && waitLeft == false && waitGlobal == false)
                    {

                        dir = Vector2.left;
                        CreateWall();
                        StartCoroutine("waitDirRight");
                        StartCoroutine("waitDirGlobal");

                        topDanger = false;
                    }

                }
                else if (rightDanger == true && waitLeft == false && waitGlobal == false)
                {
                    dir = Vector2.left;
                    CreateWall();
                    StartCoroutine("waitDirRight");
                    StartCoroutine("waitDirGlobal");

                    topDanger = false;
                }
                else if (leftDanger == true && waitRight == false && waitGlobal == false)
                {
                    dir = Vector2.right;
                    CreateWall();
                    StartCoroutine("waitDirLeft");
                    StartCoroutine("waitDirGlobal");
                    topDanger = false;
                }

            }
        

        }


        if (dir.x == 0 && dir.y == 1)//si il vas vers le haut
        {
      
             hitWall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), transform.up, detectionWall, ~layer);
            if (hitWall.collider.tag == "border" || hitWall.collider.tag == "mur")
            {
        
                int alea4 = giveRandom(1, 2);
                if ((rightDanger == false && leftDanger == false && waitGlobal == false) || (rightDanger == true && leftDanger == true && waitGlobal == false))
                {
                    if (alea4 == 1 && waitRight == false && waitGlobal == false)
                    {
                    
                        dir = Vector2.right;
                        bottomDanger = false;
                        Debug.Log("test");
                        CreateWall();
                        StartCoroutine("waitDirLeft");
                        StartCoroutine("waitDirGlobal");
             
                    }
                    if (alea4 == 2 && waitLeft == false && waitGlobal == false)
                    {
                        dir = Vector2.left;
                        bottomDanger = false;
                        Debug.Log("test");
                        CreateWall();
                        StartCoroutine("waitDirRight");
                        StartCoroutine("waitDirGlobal");
         
                    }

                }
                else if (rightDanger == true && waitLeft == false && waitGlobal == false)
                {
                    dir = Vector2.left;
                    bottomDanger = false;
                    Debug.Log("test");
                    CreateWall();
                    StartCoroutine("waitDirRight");
                    StartCoroutine("waitDirGlobal");
  
                }
                else if (leftDanger == true && waitRight == false && waitGlobal == false) 
                {
                    dir = Vector2.right;
                    bottomDanger = false;
                    Debug.Log("test");
                    CreateWall();
                    StartCoroutine("waitDirLeft");
                    StartCoroutine("waitDirGlobal");

        
                }
         
            }
 


        }
     
    }
 
    IEnumerator waitDirUp()
    {
        waitUp = true;
        yield return new WaitForSeconds(wait);
        waitUp = false;
    }

    IEnumerator waitDirDown()
    {
        waitDown = true;
        yield return new WaitForSeconds(wait);
        waitRight = false;
    }

    IEnumerator waitDirRight()
    {
        waitRight = true;
        yield return new WaitForSeconds(wait);
        waitRight = false;
    }

    IEnumerator waitDirLeft()
    {
        waitLeft = true;
        yield return new WaitForSeconds(wait);
        waitLeft = false;
    }

    IEnumerator waitDirGlobal()
    {
        waitGlobal = true;
        yield return new WaitForSeconds(wait);
        waitGlobal = false;
    }
    public int giveRandom(int min, int max)
    {
        int aleaNb = UnityEngine.Random.Range(min, max + 1);

        return aleaNb;
    }

    public void choixDecision()
    {

        Debug.Log("choix");
        int alea = giveRandom(1, 4);

        switch (alea)
        {
            case 1:
                if (dir != Vector2.down  && dir != Vector2.up && waitGlobal == false && waitUp == false && !topDanger)
                {                
                    dir = Vector2.up;
                    CreateWall();
                    StartCoroutine("waitDirDown");
                    StartCoroutine("waitDirGlobal");
                }
                else
                {
                    alea = 2;
                }


                break;
            case 2:
                if (dir != Vector2.left && dir != Vector2.right && waitGlobal == false && waitRight == false && !rightDanger)
                {                 
                    dir = Vector2.right;
                    CreateWall();
                    StartCoroutine("waitDirLeft");
                    StartCoroutine("waitDirGlobal");
                }
                else
                {
                    alea = 3;
                }
                break;
            case 3:


                if (dir != Vector2.up && dir != Vector2.up && waitGlobal == false && waitDown == false && !bottomDanger)
                {
                    dir = Vector2.down;
                    CreateWall();
                    StartCoroutine("waitDirLeft");
                    StartCoroutine("waitDirGlobal");
                }
                else
                {
                    alea = 4;
                }

                break;
            case 4:
                if (dir != Vector2.right && dir != Vector2.left && waitGlobal == false && waitLeft == false && !leftDanger )
                {
                    dir = Vector2.left;
                    CreateWall();
                    StartCoroutine("waitDirRight");
                    StartCoroutine("waitDirGlobal");
                }
                else
                {
                    alea = 1;
                }

                break;
        }

    }


    public void checkBuff()
    {
        checkBufUp = Physics2D.Raycast(transform.position, transform.up, detectionBuf, ~layer);
        checkBufDown = Physics2D.Raycast(transform.position, -transform.up, detectionBuf, ~layer);
        checkBufRight = Physics2D.Raycast(transform.position, transform.right, detectionBuf, ~layer);
        checkBufLeft = Physics2D.Raycast(transform.position, -transform.right, detectionBuf, ~layer);


        if (checkBufUp.collider.name == "PowerBoost(Clone)")
        {
         
            dir = Vector2.up;
        }
        if (checkBufDown.collider.name == "PowerBoost(Clone)")
        {
     
            dir = Vector2.down;
        }
        if (checkBufRight.collider.name == "PowerBoost(Clone)")
        {

            dir = Vector2.right;
        }
        if (checkBufLeft.collider.name == "PowerBoost(Clone)")
        {

            dir = Vector2.left;
        }


    }



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








    public Vector2 GetRightDir()
    {
        CalculateRight();
        return dirRight;
    }

    public void TurnRight()
    {

        dir = GetRightDir();
        CreateWall();
    }
    //bot
    public void TurnLeft()
    {

        dir = -GetRightDir();
        CreateWall();
    }






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

    public void resetIcon()
    {

        GameObject.Find("iconPowerUpBoost" + playerName).GetComponent<Image>().sprite = oldPoweUpBoost;
        GameObject.Find("iconStop" + playerName).GetComponent<Image>().sprite = oldIconStop;
        GameObject.Find("iconApocalypse" + playerName).GetComponent<Image>().sprite = oldIconApocalypse;
        GameObject.Find("iconInfinityShot" + playerName).GetComponent<Image>().sprite = oldIconInfinityShot;
        GameObject.Find("iconInvert" + playerName).GetComponent<Image>().sprite = oldIconInvert;
    }
 
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

    public int score = 0;


    public Text p1FinalScore;
    public Text p2FinalScore;
    public Text p3FinalScore;
    public Text p4FinalScore;

   public void verifWin()
    {
        if (nbRound <= 100)
        {
            if (score >= nbRound)
            {
                menuFinPartie.SetActive(true);
                GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 0;
                speed = 0;
             
            }

           
        }
        else
        {
            title.text = "Light Snake Ininity";
        }
    }


   


    bool scoreAddedTimer = false;
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

    public void resetWait()
    {
        waitUp = false;
        waitDown = false;
        waitRight = false;
        waitLeft = false;
        waitGlobal = false;
        topDanger = false;
        bottomDanger = false;
        rightDanger = false;
        leftDanger = false;
    }

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
    
    public void ResetPlayer()
    {
        if (gm.modeScore)
        {
            StartCoroutine(AddScore());
            title.text = "Light Snake " + score + "/" + nbRound;
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
        dir = Vector2.up;
        speed = 10;
        transform.localScale = new Vector2(1,1);
        immortal = false;
        clearDisplay();
        resetWait();
        if (gm.modeScore)
        {
            verifWin();
        }

        if (gm.modeLife)
        {
            foreach (var item in playerName)
            {
                //Debug.Log(item);
            }
            //if (playerAlive <= 1) { menuFinPartie.SetActive(true); };
        }
       
        // afficher la fin du round afficher le vainqueur et retour au lobby apres 4 sec
     
    


    }




}
