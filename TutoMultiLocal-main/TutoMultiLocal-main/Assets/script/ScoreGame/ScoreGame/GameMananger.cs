using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMananger : MonoBehaviour
{


    public int nbAlivePlayers;
    [HideInInspector]
    Player p;
    public float gameSpeed = 1;
    public int[] scores;
    public int[] life;
    Player[] players;
    public GameObject goTxt;
    PowerUp powerUp;
    public bool modeScore;
    public bool modeLife;

    public int nbLife;
    public int totalScore = 0;
    public bool lastOne = true;


    private void Start()
    {
        players = FindObjectsOfType<Player>();
        nbAlivePlayers = players.Length;
   
      

    }

    private void Awake()
    {
        if (modeLife)
        {
            nbLife = PlayerPrefs.GetInt("nbLife");
            lifeStatetement();
        }
 
    }


    public void lifeStatetement()
    {
  
        GameObject.Find("LifeP1").GetComponent<Text>().text = "Life " + nbLife;
        GameObject.Find("LifeP2").GetComponent<Text>().text = "Life " + nbLife;
        GameObject.Find("LifeP3").GetComponent<Text>().text = "Life " + nbLife;
        GameObject.Find("LifeP4").GetComponent<Text>().text = "Life " + nbLife;
    }





    public void KillPlayer()
    {
        nbAlivePlayers--;
        if (nbAlivePlayers <= 1)
        {
            gameSpeed = 0;
            GetWinner();
        }
    }

    bool scoreAddedTimer = false;
    IEnumerator AddScore0()
    {
        if (!scoreAddedTimer)
        {
            scores[0]++;
            scoreAddedTimer = true;
            yield return new WaitForSeconds(2);
            scoreAddedTimer = false;
        }
    }
    IEnumerator AddScore1()
    {
        if (!scoreAddedTimer)
        {
            scores[1]++;
            scoreAddedTimer = true;
            yield return new WaitForSeconds(2);
            scoreAddedTimer = false;
        }
    }
    IEnumerator AddScore2()
    {
        if (!scoreAddedTimer)
        {
            scores[2]++;
            scoreAddedTimer = true;
            yield return new WaitForSeconds(2);
            scoreAddedTimer = false;
        }
    }
    IEnumerator AddScore3()
    {
        if (!scoreAddedTimer)
        {
            scores[0]++;
            scoreAddedTimer = true;
            yield return new WaitForSeconds(2);
            scoreAddedTimer = false;
        }
    }
    void GetWinner()
    {
        foreach(Player p in players)
        {
       
            if (p.isAlive)
            {
                if (modeScore)
                {

              
                switch (p.playerName)
                {
                    case "P1":



                    
                        StartCoroutine("AddScore0");
                        GameObject.Find("ScoreP1").GetComponent<Text>().text = "Score " + scores[0];
                   
                        break;
                    case "P2":
          
                        StartCoroutine("AddScore1");
                        GameObject.Find("ScoreP2").GetComponent<Text>().text = "Score " + scores[1];
                      


                        break;
                    case "P3":
        
                        StartCoroutine("AddScore2");
                        GameObject.Find("ScoreP3").GetComponent<Text>().text = "Score " + scores[2];
                     

                        break;
                    case "P4":
                      
               
                        StartCoroutine("AddScore3");
                        GameObject.Find("ScoreP4").GetComponent<Text>().text = "Score " + scores[3];
                        
                        break;
                }
            }
          }
        }
        //Reset Game
        StartCoroutine("ResetGame");
    }

    public string convertName(string name)
    {
        if (name == "PlayerBlue")
        {
            return "P1";
        }
        if (name == "PlayerRouge")
        {
            return "P2";
        }
        if (name == "PlayerVert")
        {
            return "P3";
        }
        if (name == "PlayerViolet")
        {
            return "P4";
        }
        return name;
    }





    IEnumerator ResetGame() //Ienumerator permet de mettre un yield pour faire attendre
    {
        yield return new WaitForSeconds(2);  
        goTxt.SetActive(true);
        nbAlivePlayers = players.Length;
        gameSpeed = 1;
        GameObject[] murs = GameObject.FindGameObjectsWithTag("mur");
      
        
        foreach(GameObject go in murs)
        {
            Destroy(go);
        }

        foreach (Player p in players)
        {
            if (!p.isDead())
            {
                //bug
                if (nbAlivePlayers <= 1)
                {
                    GameObject.Find("theWinner").GetComponent<Text>().text = convertName(p.name);
                }
            }
            if (p.isDead())
            {
            
                nbAlivePlayers--;
                if (nbAlivePlayers <= 1)
                {
                   
                    p.menuFinPartie.SetActive(true);
                    GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 0;
                   
                  
                    p.speed = 0;
                
                  
                }
            }
            if (modeLife)
            {
                if (p.isDead())
                {
                    p.gameObject.SetActive(false);
                    p.ResetPlayer();
                }
                else
                {
                p.gameObject.SetActive(true);
                p.ResetPlayer();
                }
            
            }

            if (modeScore)
            {
                p.gameObject.SetActive(true);
                p.ResetPlayer();
            }
 
          
           
        }
        Invoke("HideGoTxt", 2);
        powerUp.gameObject.SetActive(true);
    }

    void HideGoTxt()
    {
        goTxt.SetActive(false);
    }



}
