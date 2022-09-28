using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : GameMananger.cs
//Date    : 26.09.2022
//Version : Alpha
public class GameMananger : MonoBehaviour
{


    public int nbAlivePlayers;
    [HideInInspector]
    Player p;
    public float gameSpeed = 1;
    public int[] scores;
    public int[] life;
    public int round = 1;
    Player[] players;
    public GameObject goTxt;
    PowerUp powerUp;
    public bool modeScore;
    public bool modeLife;
  

    public int nbLife;
    public int nbRound;
    public int totalScore = 0;
    public bool lastOne = true;

    public GameObject menuFinPartie;
    public Text title;

    bool scoreAddedTimer = false;
    bool roundAddedTimer = false;

    int nbPlayers;
    private void Start()
    {
        players = FindObjectsOfType<Player>();
        nbAlivePlayers = players.Length;
    }

    private void Awake()
    {
        nbPlayers = PlayerPrefs.GetInt("nbPlayers");
        if (modeLife)
        {
            nbLife = PlayerPrefs.GetInt("nbLife");
            lifeStatetement();
        }
        if (modeScore)
        {
            nbRound = PlayerPrefs.GetInt("nbRound");
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
            scores[3]++;

            scoreAddedTimer = true;
            yield return new WaitForSeconds(2);
            scoreAddedTimer = false;
        }
    }
    void GetWinner()
    {
     
        foreach (Player p in players)
        {
       
            if (p.isAlive)
            {
                if (modeScore)
                {

              
                switch (p.playerName)
                {
                    case "P1":
                        StartCoroutine("AddScore0");
               
                            break;
                    case "P2":
          
                        StartCoroutine("AddScore1");
                
                      


                        break;
                    case "P3":
        
                        StartCoroutine("AddScore2");
                  
                     

                        break;
                    case "P4":
                      
               
                        StartCoroutine("AddScore3");

                  
                 
                            break;

             
                    }
              

                    GameObject.Find("ScoreP1").GetComponent<Text>().text = "Score " + scores[0];
                    GameObject.Find("ScoreP2").GetComponent<Text>().text = "Score " + scores[1];
                    if (nbPlayers == 3)
                    {
                        GameObject.Find("ScoreP3").GetComponent<Text>().text = "Score " + scores[2];
                    }
                    if (nbPlayers == 4)
                    {
                        GameObject.Find("ScoreP3").GetComponent<Text>().text = "Score " + scores[2];
                        GameObject.Find("ScoreP4").GetComponent<Text>().text = "Score " + scores[3];
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


    IEnumerator waitAddScore()
    {
        if (!roundAddedTimer)
        {
            round++;
            roundAddedTimer = true;
            yield return new WaitForSeconds(1);
            roundAddedTimer = false;
        }
    }

    public void verifWin()
    {
        if (nbRound <= 100)
        {
            if (round > nbRound)
            {
                //int bestScore =  (new[] { scores[0], scores[1], scores[2], scores[3] }.Max()) ;

                Debug.Log("P1 " + scores[0]);
                Debug.Log("P2 " + scores[1]);
                Debug.Log("P3 " + scores[2]);
                Debug.Log("P4 " + scores[3]);
                menuFinPartie.SetActive(true);
          
                title.text = "Light Snake " + nbRound + "/" + nbRound;
                GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 0;
                p.speed = 0;

            }


        }
        else
        {
            title.text = "Light Snake Ininity";
        }
    }




    IEnumerator ResetGame() //Ienumerator permet de mettre un yield pour faire attendre
    {
      
        if (modeScore)
        {



            StartCoroutine(waitAddScore());
            title.text = "Light Snake " + round + "/" + nbRound;
            verifWin();
        }
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
