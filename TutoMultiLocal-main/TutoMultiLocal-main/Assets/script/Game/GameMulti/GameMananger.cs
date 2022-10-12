using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    public bool modeSurvivor;


    public int nbLife;
    public int nbRound;
    public int survivorTimer;
    public int totalScore = 0;
    public bool lastOne = true;

    public GameObject menuFinPartie;
    public Text title;
    public Text winner;
    public Text scoreFinalFirst;
    public Text scoreFinalSecond;
    public Text scoreFinalThird;
    public Text scoreFinalFourth;

    public Text scoreLifeFinalFirst;
    public Text scoreLifeFinalSecond;
    public Text scoreLifeFinalThird;
    public Text scoreLifeFinalFourth;


    bool scoreAddedTimer = false;
    bool roundAddedTimer = false;

    public int nbPlayers;
    public int generalLifeCompteur;

    public int nbPartie = 1;

    public GameObject heart;
    //Les Joueurs
    public Player P1;
    public Player P2;
    public Player P3;
    public Player P4;

    Dictionary<string, int> scoreboard = new Dictionary<string, int>();
    Dictionary<string, int> scoreboardLife = new Dictionary<string, int>();

    public int aleaRound;
    GameObject[] allEvent;

    private void Start()
    {
        players = FindObjectsOfType<Player>();
        nbAlivePlayers = players.Length;
    }




    private void Awake()
    {

        initialisation();
        //Instantiate()

    }


    /// <summary>
    /// Initalise les parties avec le nécéssaire propre au mode 
    /// </summary>
    public void initialisation()
    {
        if (modeLife)
        {
            nbPlayers = PlayerPrefs.GetInt("nbLifePlayers");
            nbLife = PlayerPrefs.GetInt("nbLife");
            lifeStatetement();
        }
        if (modeScore)
        {
            nbPlayers = PlayerPrefs.GetInt("nbPlayers");
            nbRound = PlayerPrefs.GetInt("nbRound");


        }
        if (modeSurvivor)
        {
           nbPlayers = PlayerPrefs.GetInt("nbSurvivorPlayers");
           survivorTimer = PlayerPrefs.GetInt("survivorTimer");
        }

   

    }

    /// <summary>
    /// Initialise tout ce qui concerne le mode vie (affichage et gestion du mode vie en general)
    /// </summary>
    public void lifeStatetement()
    {

  
        scoreboardLife.Add("P1", 0);
        scoreboardLife.Add("P2", 0);
        if (nbPlayers == 3)
        {
            scoreboardLife.Add("P3", 0);

        }
        if (nbPlayers == 4)
        {
            scoreboardLife.Add("P3", 0);
            scoreboardLife.Add("P4", 0);
        }
        GameObject.Find("LifeP1").GetComponent<Text>().text = "Life : " + nbLife;
        GameObject.Find("LifeP2").GetComponent<Text>().text = "Life : " + nbLife;
        GameObject.Find("LifeP3").GetComponent<Text>().text = "Life : " + nbLife;
        GameObject.Find("LifeP4").GetComponent<Text>().text = "Life : " + nbLife;
   





        generalLifeCompteur = nbPlayers;
    }



    //A optimiser en une fonction
    /// <summary>
    /// Gere le nombre de joueur en vie et active tout les donnée concernant la fin de partie si il ny a plus de joueur restant
    /// </summary>
    public void KillPlayer()
    {
        nbAlivePlayers--;
        if (nbAlivePlayers <= 1)
        {
            gameSpeed = 0;
            GetWinner();
        }
    }

    /// <summary>
    /// Ajout de 1 score au joueur 1 en empechant tout bug de duplication 
    /// </summary>
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
    /// <summary>
    /// Ajout de 1 score au joueur 2 en empechant tout bug de duplication 
    /// </summary>
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
    /// <summary>
    /// Ajout de 1 score au joueur 3 en empechant tout bug de duplication 
    /// </summary>
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
    /// <summary>
    /// Ajout de 1 score au joueur 4 en empechant tout bug de duplication 
    /// </summary>
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

    /// <summary>
    /// regarde qui gagne le round et lui donne un point en concéquence
    /// </summary>
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


    /// <summary>
    /// convertie les nom base "PlayerColor" en "P1,P2"
    /// </summary>
    /// <param name="name">nom du joueur</param>
    /// <returns>string nom convertie</returns>
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

   /// <summary>
   /// Empeche la duplication de round
   /// </summary>
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


    /// <summary>
    /// Gere l'affichage du score board du mode score
    /// </summary>
    public void orderScoreBoardAndDisplay()
    {
      
       
        scoreboard.Add("P1", scores[0]);
        scoreboard.Add("P2", scores[1]);
        if (nbPlayers == 3)
        {
            scoreboard.Add("P3", scores[2]);
        }
        if (nbPlayers == 4)
        {
            scoreboard.Add("P3", scores[2]);
            scoreboard.Add("P4", scores[3]);
        }

        int compteur = 0;
        foreach (KeyValuePair<string,int> scoreInfos in scoreboard.OrderByDescending(key => key.Value))
        {
            compteur++;

            if (compteur == 1)
            {
 
                scoreFinalFirst.text = scoreInfos.Key + " : " + scoreInfos.Value;
                if (modeScore)
                {
                    winner.text = scoreInfos.Key;
                }
            
            }
            if (compteur == 2)
            {
                scoreFinalSecond.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
            if (compteur == 3)
            {
                scoreFinalThird.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
            if (compteur == 4)
            {
                scoreFinalFourth.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
    
            //scoreInfos.Key + " : " +scoreInfos.Value;
            //GameObject.Find(scoreInfos.Key + "ScoreFinal").GetComponent<Text>().text = Convert.ToString( scoreInfos.Value);

       
        }

    }
    /// <summary>
    /// Gere l'affichage du score board du mode vie
    /// </summary>
    public void orderScoreBoardLifeAndDisplay()
    {
   

      

        scoreboardLife["P1"] = P1.finalLife;
        scoreboardLife["P2"] = P2.finalLife;
        if (nbPlayers == 3)
        {
            scoreboardLife["P3"] = P3.finalLife;
        }
        if (nbPlayers == 4)
        {
            scoreboardLife["P3"] = P3.finalLife;
            scoreboardLife["P4"] = P4.finalLife;
        }


        int compteurLife = 0;
        foreach (KeyValuePair<string, int> scoreInfos in scoreboardLife.OrderBy(key => key.Value))
        {


   

            compteurLife++;

            if (compteurLife == 1)
            {

                scoreLifeFinalFirst.text = scoreInfos.Key + " : "  + "First";
                if (modeLife)
                {
                    winner.text = "Winner " + scoreInfos.Key;
                
                }
            
            }
            if (compteurLife == 2)
            {
        
                scoreLifeFinalSecond.text = scoreInfos.Key + " : " + "Second";
            }
            if (compteurLife == 3)
            {
                scoreLifeFinalThird.text = scoreInfos.Key + " : " + "Third";
            }
            if (compteurLife == 4)
            {
                scoreLifeFinalFourth.text = scoreInfos.Key + " : " + "Fourth";

            }



        }

    }

    /// <summary>
    /// verifie si la fin de round a lieu et acctionne la methode servant a afficher le scoreboard
    /// </summary>
    public void verifWin()
    {

        if (nbRound < 100)
        {
            if (round > nbRound)
           {
    
                menuFinPartie.SetActive(true);
            
                orderScoreBoardAndDisplay();
           
              
                title.text = "Light Snake " + nbRound + "/" + nbRound;
                GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 0;
                p.speed = 0;

            }


        }
        else
        {
            title.text = "Light Snake Ininity";
            nbRound = 9999;
        }
    }

    /// <summary>
    /// Reset la game en remettant le tout a 0 (affichage notamment)
    /// </summary>
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


        foreach (GameObject go in murs)
        {
            Destroy(go);
        }

     


        foreach (Player p in players)
        {
       
            if (p.isDead())
            {

                nbAlivePlayers--;

                if (nbAlivePlayers <= 1)
                {
                    p.menuFinPartie.SetActive(true);

              

                    GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 0;
                    p.speed = 0;
                    orderScoreBoardLifeAndDisplay();
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






        scoreBoardOrder();

        suppAllEvent();
           aleaRound = UnityEngine.Random.RandomRange(1, 4);
        if (modeLife)
        {
            if (aleaRound == 1)
            {
               
                spawnEventLife();
            } 
        }

        Invoke("HideGoTxt", 2);
        powerUp.gameObject.SetActive(true);



        nbPartie++;

    }

    /// <summary>
    /// fait instencier aléatoirement un coeur donnant 1pv de plus a celui qui arrive a l attraper et supprime tous ceux present
    /// </summary>
    /// 
    public void spawnEventLife()
    {
        allEvent = GameObject.FindGameObjectsWithTag("event");
        Debug.Log(allEvent.Count());
        int compteur = 0;
        foreach (GameObject eventName in allEvent)
        {
            Destroy(allEvent[compteur]);
            compteur++;
             
        }
        Instantiate(heart, new Vector2(0, 0), Quaternion.identity);
    }


    void suppAllEvent()
    {
        allEvent = GameObject.FindGameObjectsWithTag("event");
        int compteur = 0;
        foreach (GameObject eventName in allEvent)
        {
            Destroy(allEvent[compteur]);
            compteur++;

        }
    }

    /// <summary>
    /// Ordonne le scorboard
    /// </summary>
    void scoreBoardOrder()
    {
        int compteur = 0;
        foreach (KeyValuePair<string, int> scoreInfos in scoreboard.OrderBy(key => key.Value))
        {
            compteur++;

            if (compteur == 1)
            {
                scoreFinalFirst.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
            if (compteur == 2)
            {
                scoreFinalSecond.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
            if (compteur == 3)
            {
                scoreFinalThird.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
            if (compteur == 4)
            {
                scoreFinalFourth.text = scoreInfos.Key + " : " + scoreInfos.Value;
            }
        }
    }

    /// <summary>
    /// Cache le "Go" de debut de game
    /// </summary>
        void HideGoTxt()
        {
            goTxt.SetActive(false);
 
        }

    }


