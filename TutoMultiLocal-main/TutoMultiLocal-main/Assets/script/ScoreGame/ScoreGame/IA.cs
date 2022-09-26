using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.PostProcessing;

public class IA : MonoBehaviour
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
    [HideInInspector]
    public bool isAlive = true;
    public int boost = 1;
    //public SpriteRenderer wallColor;
    Vector2 initialPos;
    Vector2 dirRight;
    public LayerMask layerToGiveToWall;
    public bool invert = false;
    public bool inBoost = false;

    public Vector2 saveDirectionRight;
    int missilePosX;
    int missilePosY;



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

    public GameObject[] allBoost;
    public GameObject[] allShot;

    public LayerMask layer;
    RaycastHit2D hit;
    public float detectionWall = 0.1f;
    Player p;

    //Prevenir le danger
    RaycastHit2D checkUp  ;
    RaycastHit2D checkDown;
    RaycastHit2D checkRight;
    RaycastHit2D checkLeft;

    bool dangerUp = false;
    bool dangerDown = false;
    bool dangerRight = false;
    bool dangerLeft = false;

    float detectionDanger = 0.1f;


    bool waitUp = false;
    bool waitDown = false;
    bool waitRight = false;
    bool waitLeft = false;

    public int waitChangeDirection = 2;

    RaycastHit2D checkBufUp;
    RaycastHit2D checkBufDown;
    RaycastHit2D checkBufRight;
    RaycastHit2D checkBufLeft;

    public float detectionBuf = 20f;


    private void Start()
    {
     

        initialPos = transform.position;
        dir = Vector2.up;//Direction par defaut
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed * gm.gameSpeed;
        wallPrefab = Resources.Load("Wall" + gameObject.tag) as GameObject;
        InvokeRepeating("choixDecision", 1, 5);
       

    }

    private void Awake()
    {
     
        cam = Camera.main.GetComponent<Cam>();//fait le lien avec le script "Cam"
        gm = GameObject.Find("GameManager").GetComponent<GameMananger>();
    }

    private void LateUpdate()
    {
    
     
        rb.velocity = dir * speed * gm.gameSpeed;

        detectionNotToGo();
    }
  



    private void Update()
    {
   
        //SetLastWallSize(lastWallCol, lastPos, transform.position);
        TakeWallDecision();


    }
    private void OnTriggerEnter2D(Collider2D collision)//se declanche au moment du toucher du border si on a activer la fonctionalité "on trigger"
    {
        if (collision != lastWallCol && collision.gameObject.tag != "PowerUpBoost")
        {
            isAlive = false;
            gm.KillPlayer();
            cam.Shake(0.7f, 0.4f, 50f);
            cam.PlayBoumSfx();
            Instantiate(boomParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            speed = 5;
            canActivateBoost = true;
        }

    }


    public void choixDecision() {
        int alea = giveRandom(1, 4);

        switch (alea)
        {
            case 1:
                if (dir != Vector2.down)
                {
                    if (dangerUp != true)
                    {
                        if (waitUp == false)
                        {
                            dir = Vector2.up;
                            CreateWall();
                            StartCoroutine("waitDirUp");
                        }
                    }
          
                }
  
                      
                break;
            case 2:
                if (dir != Vector2.left)
                {
                    if (dangerRight != true)
                    {
                        if (waitRight == false)
                        {
                            dir = Vector2.right;
                            CreateWall();
                            StartCoroutine("waitDirRight");
                        }
                    }
            
                }
     
                break;
            case 3:
         
       
               if(dir != Vector2.up)
                {
                    if (dangerDown != true)
                    {
                        if (waitDown == false)
                        {
                            dir = Vector2.down;
                            CreateWall();
                            StartCoroutine("waitDirDown");
                        }
                    }
            
                }
        
                break;
            case 4:
                if (dir != Vector2.right)
                {
                    if (dangerLeft != true)
                    {
                        if (waitLeft == false)
                        {
                            dir = Vector2.left;
                            CreateWall();
                            StartCoroutine("waitDirLeft");
                        }
                    }
           
                }
        
                break;
        }

    }


        public void detectionNotToGo()
    {
        checkUp = Physics2D.Raycast(transform.position, transform.up, detectionDanger, ~layer);
        checkDown = Physics2D.Raycast(transform.position, -transform.up, detectionDanger, ~layer);
        checkRight = Physics2D.Raycast(transform.position, transform.right, detectionDanger, ~layer);
        checkLeft = Physics2D.Raycast(transform.position, -transform.right, detectionDanger, ~layer);

        checkBufUp = Physics2D.Raycast(transform.position, transform.up, detectionBuf, ~layer);
        checkBufDown = Physics2D.Raycast(transform.position, -transform.up, detectionBuf, ~layer);
        checkBufRight = Physics2D.Raycast(transform.position, transform.right, detectionBuf, ~layer);
        checkBufLeft = Physics2D.Raycast(transform.position, -transform.right, detectionBuf, ~layer);

        Debug.DrawRay(transform.position ,transform.up * detectionBuf, Color.red);
        Debug.DrawRay(transform.position, -transform.up * detectionBuf, Color.blue);

        if (checkUp.collider.tag == "border" || checkUp.collider.tag == "mur")
        {
            dangerUp = true;
        }
        else
        {
            dangerUp = false;
            if (checkBufUp.collider.tag == "PowerUpBoost")
            {
                dir = Vector2.up;
            }
        }

        if (checkDown.collider.tag == "border" || checkDown.collider.tag == "mur")
        {
            dangerDown = true;
        }
        else
        {
            dangerDown = false;
            if (checkBufDown.collider.tag == "PowerUpBoost")
            {
                dir = Vector2.down;
            }
        }

        if (checkRight.collider.tag == "border" || checkRight.collider.tag == "mur")
        {
            dangerRight = true;
        }
        else
        {
            dangerRight= false;
            if (checkBufRight.collider.tag == "PowerUpBoost")
            {
                dir = Vector2.right;
            }
        }
    
        if (checkLeft.collider.tag == "border" || checkLeft.collider.tag == "mur")
        {
            dangerLeft = true;
        }
        else
        {
            dangerLeft = false;
            if (checkBufLeft.collider.tag == "PowerUpBoost")
            {
                dir = Vector2.left;
            }
        }
    }



        public void TakeWallDecision()
        {



        if (dir == Vector2.up)//si il vas vers le haut
        {


            hit = Physics2D.Raycast(transform.position, transform.up, detectionWall, ~layer);
            if (hit.collider.tag == "border" || hit.collider.tag == "mur")
            {
                int alea = giveRandom(1, 4);
                if (alea == 1)
                {
                    if (dir != Vector2.down)
                    {
                        if (waitUp == false)
                        {
                        dir = Vector2.up;
                        hit = Physics2D.Raycast(transform.position, transform.up, detectionWall, ~layer);
                        CreateWall();
                        StartCoroutine("waitDirUp");
                        }
                    }
                
                  
                }
                if (alea == 2)
                {
                    if (dir != Vector2.left)
                    {
                        if (waitRight == false)
                        {
                            dir = Vector2.right;
                            hit = Physics2D.Raycast(transform.position, transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirRight");
                        }
                    }
               
                }
                if (alea == 3)
                {
                    if (dir != Vector2.right)
                    {
                        if (waitLeft == false)
                        {
                            dir = Vector2.left;
                            hit = Physics2D.Raycast(transform.position, -transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirLeft");
                        }
                    }
           
                    
               
                    
                }

                if (alea == 4)
                {
                    if (dir != Vector2.up)
                    {
                        if (waitDown == false)
                        {
                            dir = Vector2.down;
                            hit = Physics2D.Raycast(transform.position, -transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirDown");
                        }
                    }
          



                }
            }
    
        }

       if (dir == Vector2.right)//si il va vers la droite
        {
           
            hit = Physics2D.Raycast(transform.position, transform.right, detectionWall, ~layer);
            if (hit.collider.tag == "border" || hit.collider.tag == "mur")
            {
                int alea = giveRandom(1, 4);
                if (alea == 1)
                {
                    if (dir != Vector2.down)
                    {
                        if (waitUp == false)
                        {
                            dir = Vector2.up;
                            hit = Physics2D.Raycast(transform.position, transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirUp");
                        }
                    }
           
                }
                if (alea == 2)
                {
                    if (dir != Vector2.up)
                    {
                        if (waitDown == false)
                        {
                            dir = Vector2.down;
                            hit = Physics2D.Raycast(transform.position, -transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirDown");
                        }
                    }
             
                }
                if (alea == 3)
                {
                    if (dir != Vector2.left)
                    {
                        if (waitRight == false)
                        {
                            dir = Vector2.right;
                            hit = Physics2D.Raycast(transform.position, transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirRight");
                        }
                    }
                  

                }

                if (alea == 4)
                {
                    if (dir != Vector2.right)
                    {
                        if (waitLeft == false)
                        {
                            dir = Vector2.left;
                            hit = Physics2D.Raycast(transform.position, -transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirLeft");
                        }
                    }


                }

            }
        }
        if (dir == Vector2.left)//si il va a gauche
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, detectionWall, ~layer);
       
            if (hit.collider.tag == "border" || hit.collider.tag == "mur")
            {
                int alea = giveRandom(1, 4);
                if (alea == 1)
                {
                    if (dir != Vector2.down)
                    {
                        if (waitUp == false)
                        {
                            dir = Vector2.up;
                            hit = Physics2D.Raycast(transform.position, transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirUp");
                        }
                    }
              

                }
                if (alea == 2)
                {
                    if (dir != Vector2.right)
                    {
                        if (waitLeft == false)
                        {
                            dir = Vector2.left;
                            hit = Physics2D.Raycast(transform.position, -transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirLeft");
                        }
                    }
                  

                }
                if (alea == 3)
                {
                    if (dir != Vector2.up)
                    {
                        if (waitDown == false)
                        {
                            dir = Vector2.down;
                            hit = Physics2D.Raycast(transform.position, -transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirDown");
                        }
                    }
        

                }
                if (alea == 4)
                {
                    if (dir != Vector2.left)
                    {
                        if (waitRight == false)
                        {
                            dir = Vector2.right;
                            hit = Physics2D.Raycast(transform.position, transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirRight");
                        }
                    }


                }
            }
            }
        if (dir == Vector2.down)//si il va en bas
        {
            hit = Physics2D.Raycast(transform.position, -transform.up, detectionWall, ~layer);
            if (hit.collider.tag == "border" || hit.collider.tag == "mur")
            {
                int alea = giveRandom(1, 4);
                if (alea == 1)
                {
                    if (dir != Vector2.up)
                    {
                        if (waitDown == false)
                        {
                            dir = Vector2.down;
                            hit = Physics2D.Raycast(transform.position, -transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirDown");
                        }
                    }
         

                }
                if (alea == 2)
                {
                    if (dir != Vector2.left)
                    {
                        if (waitRight == false)
                        {
                            dir = Vector2.right;
                            hit = Physics2D.Raycast(transform.position, transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirRight");
                        }
                    }
           

                }
                if (alea == 3)
                {
                    if (dir != Vector2.right)
                    {
                        if (waitLeft == false)
                        {
                            dir = Vector2.left;
                            hit = Physics2D.Raycast(transform.position, -transform.right, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirLeft");
                        }
                    }
                 

                }
                if (alea == 4)
                {
                    if (dir != Vector2.down)
                    {
                        if (waitUp == false)
                        {
                            dir = Vector2.up;
                            hit = Physics2D.Raycast(transform.position, transform.up, detectionWall, ~layer);
                            CreateWall();
                            StartCoroutine("waitDirUp");
                        }
                    }


                }

            }
        }

        



    }


    public int giveRandom(int min,int max)
    {
        int aleaNb = UnityEngine.Random.Range(min, max+1);

        return aleaNb;
    }
     
  
    public void CreateWall()
    {
       
            //lastPos = transform.position; // recupere la pos du joueureur au moment du changement
            //GameObject go = Instantiate(wallPrefab, transform.position, Quaternion.identity);
            //if (lastWallCol != null)
            //{
            //    lastWallCol.gameObject.layer = 8;//player
            //}
            //lastWallCol = go.GetComponent<Collider2D>();
        
    }
    private void SetLastWallSize(Collider2D col, Vector2 posStart, Vector2 posEnd)
    {



            if (col)
            {
                col.transform.position = posStart + (posEnd - posStart) / 2;
                float size = Vector2.Distance(posEnd, posStart);
                if (posStart.x != posEnd.x)
                {
                    col.transform.localScale = new Vector2(size + wallWidth, wallWidth);
                }
                else
                {
                    col.transform.localScale = new Vector2(wallWidth, size + wallWidth);
                }
            }
        

    }

    
    public void ResetPlayer()
    {

        p.resetIcon();
        p.invert = false;
        boost = 1;
        p.shotMissile = 1;
        GameObject.Find("Boost" + playerName).GetComponent<Text>().text = "Boost : " + p.boost;
        GameObject.Find("Shot" + playerName).GetComponent<Text>().text = "Shot : " + p.shotMissile;
        transform.position = initialPos;
        isAlive = true;
        dir = Vector2.up;
        speed = 10;
        transform.localScale = new Vector2(1, 1);
       
        p.clearDisplay();



    }


    IEnumerator waitDirUp()
    {
        waitUp = true;
        yield return new WaitForSeconds(waitChangeDirection);
        waitUp = false;
    }

    IEnumerator waitDirDown()
    {
        waitDown = true;
        yield return new WaitForSeconds(waitChangeDirection);
        waitRight = false;
    }

    IEnumerator waitDirRight()
    {
        waitRight = true;
        yield return new WaitForSeconds(waitChangeDirection);
        waitRight = false;
    }

    IEnumerator waitDirLeft()
    {
        waitLeft = true;
        yield return new WaitForSeconds(waitChangeDirection);
        waitLeft = false;
    }


}
