using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : PowerUp.cs
//Date    : 26.09.2022
//Version : Alpha
public class PowerUp : MonoBehaviour
{

    Player p;
    Cam cam;
    bool shakingCam;
    public GameObject boomParticles;
    bool onInfinityBoost = false;
    bool isImmortal = false;
    Vector2 positionCollide;
    GameMananger gm;


    public Sprite iconBoost;
    Sprite oldPoweUpBoost;

    public Sprite iconStop;
    Sprite oldIconStop;

    public Sprite iconInvert;
    Sprite oldIconInvert;

    public Sprite iconInfinityShot;
    Sprite oldIconInfinityShot;

    public Sprite iconApocalypse;
    Sprite oldIconApocalypse;



    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    public GameObject boost;
    public GameObject shot;

    string player1 = "PlayerBlue";
    string player2 = "PlayerRed";
    string player3 = "PlayerGreen";
    string player4 = "PlayerViolet";
    public GameObject[] mur;
    public GameObject[] allBoost;
    public GameObject[] allShot;
    int nbPlayers;
    int nbLifePlayers;
    Missile missile;

    public bool modeScore;
    public bool modeLife;

    //bruitage
    public AudioClip boomSfx;


    string[] nameWall = new string[] { "WallBlue(Clone)","WallRed(Clone)","WallGreen(Clone)","WallViolet(Clone)" };

    ////////////////////////////////////Instant///////////Instante//////Durer//////////Durer//////Durer/////////Durer/////////Durer/////
    string[] powerUp = new string[] { "AddBoost", "MoreMissile", "PowerUpBoost", "Stop", "InfinityShot", "InverseCommand", "Tremblement"/*"ReplaceStrat"*/};

   

    private void Start()
    {
        //nbPlayers = PlayerPrefs.GetInt("nbPlayers");
      
      

        p = GetComponent<Player>();
        cam = Camera.main.GetComponent<Cam>();
        oldPoweUpBoost = GameObject.Find("iconPowerUpBoost" + p.playerName).GetComponent<Image>().sprite;
        oldIconStop = GameObject.Find("iconStop" + p.playerName).GetComponent<Image>().sprite;
        oldIconInfinityShot = GameObject.Find("iconInfinityShot" + p.playerName).GetComponent<Image>().sprite;
        oldIconApocalypse = GameObject.Find("iconApocalypse" + p.playerName).GetComponent<Image>().sprite;
        oldIconInvert = GameObject.Find("iconInvert" + p.playerName).GetComponent<Image>().sprite;

    }



    private void Update()
    {
        if (modeScore)
        {
            nbPlayers = PlayerPrefs.GetInt("nbPlayers");
        }
        if(modeLife)
        {
    
            nbPlayers = PlayerPrefs.GetInt("nbLifePlayers");
        }
        
        mur = GameObject.FindGameObjectsWithTag("mur");
        inverserControl();

    }



    //pour detecter les col avec un triger

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "PowerUpBoost")
        {
            string AleaPowerUp = powerUp[Random.Range(0,powerUp.Length)];

            if (AleaPowerUp == "PowerUpBoost")
            {
                Destroy(collision.gameObject);
                StartCoroutine("AddSpeed");
            }
            if (AleaPowerUp == "AddBoost")
            {

                Destroy(collision.gameObject);
                AddBoost();
                
            }

            if (AleaPowerUp == "InverseCommand")
            {
                Destroy(collision.gameObject);
                inverserCommand();
         
            }
            if (AleaPowerUp == "Stop")
            {

                Destroy(collision.gameObject);
                StartCoroutine("Stop");
            }
            if (AleaPowerUp == "Tremblement")
            {
                Destroy(collision.gameObject);
                StartCoroutine("Tremblement");

            }
            if (AleaPowerUp == "MoreMissile")
            {

                Destroy(collision.gameObject);
                AddShot();
                GameObject.Find("Shot" + p.playerName).GetComponent<Text>().text = "Shot : " + p.shotMissile;
            }
            if (AleaPowerUp == "InfinityShot")
            {

                Destroy(collision.gameObject);
                StartCoroutine("InfinityShot");


            }
            if (AleaPowerUp == "ReplaceStrat")
            {

                Destroy(collision.gameObject);
                //StartCoroutine("ReplaceStrat");
                replaceStrategique();

            }
        }

        if (collision.gameObject.tag == "event")
        {
            
            Destroy(collision.gameObject);
            p.life+=2;
            GameObject.Find("Life" + p.playerName).GetComponent<Text>().text = "Life : " + p.life;

        }

    }


    /// <summary>
    /// active l'inversion des commands
    /// </summary>
    public void inverserCommand()
    {
       
        if (GameObject.Find(player1).GetComponent<Player>().playerName == p.playerName)
        {
            GameObject.Find("iconInvertP2").GetComponent<Image>().sprite = iconInvert;
            GameObject.Find(player2).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            if (nbPlayers == 3)
            {
                GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find(player3).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            }
            if (nbPlayers == 4)
            {
                GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find("iconInvertP4").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find(player3).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");      
                GameObject.Find(player4).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            }

        }

        if (GameObject.Find(player2).GetComponent<Player>().playerName == p.playerName)
        {
            GameObject.Find("iconInvertP1").GetComponent<Image>().sprite = iconInvert;
            GameObject.Find(player1).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            if (nbPlayers == 3)
            {
                GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find(player3).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            }
            if (nbPlayers == 4)
            {
                GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find("iconInvertP4").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find(player3).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
                GameObject.Find(player4).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            }

        }
     

       
        if (GameObject.Find(player3).GetComponent<Player>().playerName == p.playerName)
        {
           GameObject.Find("iconInvertP1").GetComponent<Image>().sprite = iconInvert;
           GameObject.Find("iconInvertP2").GetComponent<Image>().sprite = iconInvert;
           GameObject.Find(player1).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
           GameObject.Find(player2).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            if (nbPlayers == 4)
            {
                GameObject.Find("iconInvertP4").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find(player4).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
            }
        }
        
    
            if (GameObject.Find(player4).GetComponent<Player>().playerName == p.playerName)
            {
                GameObject.Find("iconInvertP1").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find("iconInvertP2").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = iconInvert;
                GameObject.Find(player1).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
                GameObject.Find(player2).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");
                GameObject.Find(player3).GetComponent<PowerUp>().StartCoroutine("SlowSpeed");

            }
        
    }

    /// <summary>
    ///  augmente la vitesse du joueur de 15
    /// </summary>
    IEnumerator AddSpeed()
    {   
        GameObject.Find("iconPowerUpBoost" + p.playerName).GetComponent<Image>().sprite = iconBoost;
        p.inBoost = true;
        p.speed = 25;
        yield return new WaitForSeconds(5);
        p.speed = 10;
        p.inBoost = false;
        GameObject.Find("iconPowerUpBoost" + p.playerName).GetComponent<Image>().sprite = oldPoweUpBoost;
    }

    /// <summary>
    /// donne un boost suplementaire au joueur
    /// </summary>
    void AddBoost()
    {


        StartCoroutine("apparitionBoostIcon");
        p.boost++;
        GameObject.Find("Boost" + p.playerName).GetComponent<Text>().text = "Boost : " + p.boost;

    }


    /// <summary>
    /// intancie une image annonçant le ramassage du boost supplementaire
    /// </summary>

    IEnumerator apparitionBoostIcon()
    {
        Instantiate(boost, p.transform.position, Quaternion.identity);
        allBoost = GameObject.FindGameObjectsWithTag("boost");
        yield return new WaitForSeconds(1);    
        Destroy(allBoost[0]);
    }


    /// <summary>
    ///     donne un tire suplementaire au joueur
    /// </summary>

    void AddShot()
    {
        StartCoroutine("apparitionShotIcon");
        GameObject.Find("Shot" + p.playerName).GetComponent<Text>().text = "Shot : " + p.shotMissile;
        p.shotMissile++;

    }
    /// <summary>
    /// intancie une image annonçant le ramassage du shot supplementaire
    /// </summary>
    IEnumerator apparitionShotIcon()
    {
        Instantiate(shot, p.transform.position, Quaternion.identity);
        allShot = GameObject.FindGameObjectsWithTag("shot");
        yield return new WaitForSeconds(1);
        Destroy(allShot[0]);
    }


 

    /// <summary>
    /// donne 999 missile pendant 4 seconde
    /// </summary>
    IEnumerator InfinityShot()
    {
        GameObject.Find("iconInfinityShot" + p.playerName).GetComponent<Image>().sprite = iconInfinityShot;
        p.inBoost = true;
        p.speed = 7;
        p.shotMissile = 999;
        GameObject.Find("Shot" + p.playerName).GetComponent<Text>().text = "Shot : " + p.shotMissile;
        yield return new WaitForSeconds(6);
        p.shotMissile = 0;
        GameObject.Find("Shot" + p.playerName).GetComponent<Text>().text = "Shot : " + p.shotMissile;
        p.speed = 10;
        p.inBoost = false;
        GameObject.Find("iconInfinityShot" + p.playerName).GetComponent<Image>().sprite = oldIconInfinityShot;
    }



    /// <summary>
    /// ralentis le joueur
    /// </summary>
    IEnumerator SlowSpeed()
    {
        p.inBoost = true;
        p.invert = true;
        p.speed = 4;
        yield return new WaitForSeconds(5);
        p.speed = 10;
        p.inBoost = false;
        p.invert = false;
        GameObject.Find("iconInvertP1").GetComponent<Image>().sprite = oldIconInvert;
        GameObject.Find("iconInvertP2").GetComponent<Image>().sprite = oldIconInvert;
        if (nbPlayers == 3)
        {
            GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = oldIconInvert;
        }
        if (nbPlayers == 4)
        {
            GameObject.Find("iconInvertP3").GetComponent<Image>().sprite = oldIconInvert;
            GameObject.Find("iconInvertP4").GetComponent<Image>().sprite = oldIconInvert;
        }
   
    }

    

    /// <summary>
    /// arrete le joueur pendant 2 seconde
    /// </summary>
  
    IEnumerator Stop()
    {
 
        GameObject.Find("iconStop" + p.playerName).GetComponent<Image>().sprite = iconStop;
        p.inBoost=true;
        p.speed = 0;
        yield return new WaitForSeconds(2);
        p.speed = 10;
        p.inBoost = false;
        GameObject.Find("iconStop" + p.playerName).GetComponent<Image>().sprite = oldIconStop;
    }



    /// <summary>
    /// fait trembler tout la map pendant quelque seconde
    /// </summary>

    IEnumerator Tremblement()
    {

        GameObject.Find("iconApocalypseP1").GetComponent<Image>().sprite = iconApocalypse;
        GameObject.Find("iconApocalypseP2").GetComponent<Image>().sprite = iconApocalypse;
        if (nbPlayers == 3 )
        {
            GameObject.Find("iconApocalypseP3").GetComponent<Image>().sprite = iconApocalypse;
        }
        if (nbPlayers == 4)
        {
            GameObject.Find("iconApocalypseP3").GetComponent<Image>().sprite = iconApocalypse;
            GameObject.Find("iconApocalypseP4").GetComponent<Image>().sprite = iconApocalypse;
        }

        for (int i = 0; i < 5; i++)
        {
            cam.PlayBoumSfx(cam.boomSfx);
            cam.Shake(5f, 2f, 30);
            if (i % 2 == 0)
            {
            destructionMur();      
            }
      
            yield return new WaitForSeconds(1);
        }

        GameObject.Find("iconApocalypseP1").GetComponent<Image>().sprite = oldIconApocalypse;
        GameObject.Find("iconApocalypseP2").GetComponent<Image>().sprite = oldIconApocalypse;
        if (nbPlayers == 3)
        {
            GameObject.Find("iconApocalypseP3").GetComponent<Image>().sprite = oldIconApocalypse;
        }
        if (nbPlayers == 4)
        {
            GameObject.Find("iconApocalypseP3").GetComponent<Image>().sprite = oldIconApocalypse;
            GameObject.Find("iconApocalypseP4").GetComponent<Image>().sprite = oldIconApocalypse;
        }

    }


    /// <summary>
    /// detruit des mur de façon àleatoire
    /// </summary>
    void destructionMur()
    {
        
        //faire un random compris entre la taille du tableau et 0
        foreach (GameObject murADetruire in mur)
        {
            if (murADetruire.name == nameWall[Random.Range(0,nameWall.Length)])
            {
                Destroy(mur[Random.Range(0, mur.Length)]);
                Instantiate(boomParticles, new Vector2(murADetruire.transform.position.x, murADetruire.transform.position.y), Quaternion.identity);
            
            }

        }
         
    }


    /// <summary>
    /// rend immortel le joueur (plus fonctionelle uniquement utilisé dans les fase de test)
    /// </summary>
    IEnumerator immortal()
    {
        
        p.immortal = true;
        yield return new WaitForSeconds(5);
        p.immortal = false;

    }


    /// <summary>
    /// idée mais pas utilisé
    /// </summary>
    void replaceStrategique()
    {
        //faire un random compris entre la taille du tableau et 0
        StartCoroutine("immortal");
    }


    /// <summary>
    /// Inverse les touche de tout les autres joueurs
    /// </summary>
    public void inverserControl()
    {
        if (p.invert == true)
        {


            if (Input.GetButtonDown(p.playerName + "UP"))
            {
                if (p.dir != Vector2.up)
                {
                    p.dir = Vector2.down;
                    p.CreateWall();

                }
            }
            if (Input.GetButtonDown(p.playerName + "DOWN"))
            {
                if (p.dir != Vector2.down)
                {
                    p.dir = Vector2.up;
                    p.CreateWall();
                }
            }
            if (Input.GetButtonDown(p.playerName + "LEFT"))
            {
                if (p.dir != Vector2.left)
                {
                    p.dir = Vector2.right;
                    p.CreateWall();
                }
            }
            if (Input.GetButtonDown(p.playerName + "RIGHT"))
            {
                if (p.dir != Vector2.right)
                {
                    p.dir = Vector2.left;
                    p.CreateWall();
                }
            }
            if (Input.GetButtonUp(p.playerName + "BOOST"))
            {



                if (p.shotMissile >= 1)
                {
                    p.shootDirection();
                    p.shotMissile--;
                    GameObject.Find("Shot" + p.playerName).GetComponent<Text>().text = "Shot : " + p.shotMissile;

                }





            }

        }

 
    }
}




