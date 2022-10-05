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


    public int nbLife;
    public int nbRound;
    public int totalScore = 0;
    public bool lastOne = true;

    public GameObject menuFinPartie;
    public Text title;
    public Text scoreFinalFirst;
    public Text scoreFinalSecond;
    public Text scoreFinalThird;
    public Text scoreFinalFourth;


    bool scoreAddedTimer = false;
    bool roundAddedTimer = false;

    public int nbPlayers;
    public int generalLifeCompteur;

    Dictionary<string, int> scoreboard = new Dictionary<string, int>();
    Dictionary<string, int> scoreboardLife = new Dictionary<string, int>();

    private void Start()
    {
        players = FindObjectsOfType<Player>();
        nbAlivePlayers = players.Length;
    }




    private void Awake()
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

        generalLifeCompteur = nbPlayers;


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
            //Debug.Log(scoreInfos.Value + scoreInfos.Key);
            //scoreInfos.Key + " : " +scoreInfos.Value;
            //GameObject.Find(scoreInfos.Key + "ScoreFinal").GetComponent<Text>().text = Convert.ToString( scoreInfos.Value);

       
        }

    }

    public void verifWin()
    {
        if (nbRound <= 100)
        {
            if (round > nbRound)
            {
                //int bestScore =  (new[] { scores[0], scores[1], scores[2], scores[3] }.Max()) ;
                //Debug.Log(scoreBoard["P1"]);      
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


        foreach (GameObject go in murs)
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
                    generalLifeCompteur--;

                    p.gameObject.SetActive(false);
                    p.ResetPlayer();
                    p.finalLife = generalLifeCompteur;
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


        foreach (Player p in players)
        {



            //Debug.Log(p.playerName + " " + p.finalLife.ToString());


            //Mode vie scoreboard

            scoreboardLife.Add(p.playerName, p.finalLife);

            foreach (var item in scoreboardLife)
            {
                Debug.Log(scoreboardLife.Values);
            }
            Debug.Log(scoreboardLife.Keys);



            //scoreboardLife.Add("P1", p.finalLife);
            //scoreboardLife.Add("P2", p.finalLife);
            //if (nbPlayers == 3)
            //{
            //    scoreboardLife.Add("P3", p.finalLife);
            //}
            //if (nbPlayers == 4)
            //{
            //    scoreboardLife.Add("P3", p.finalLife);
            //    scoreboardLife.Add("P4", p.finalLife);
            //}

        }
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
            Invoke("HideGoTxt", 2);
            powerUp.gameObject.SetActive(true);

        }

        void HideGoTxt()
        {
            goTxt.SetActive(false);
        }

    }


