using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : ActivatePlayers.cs
//Date    : 26.09.2022
//Version : Alpha
public class ActivatePlayers : MonoBehaviour
{
    public GameObject P3;
    public GameObject P4;
    public bool modeScore;
    public bool modeLife;



    void Awake()
    {

        displayPlayerInfoMultiGame();


    }

    /// <summary>
    /// Methode servant a gerer l'affichage en fonction du nombre de joueurs en mode multi
    /// </summary>
    private void displayPlayerInfoMultiGame()
    {

        if (modeScore)
        {
            int nbPlayers = PlayerPrefs.GetInt("nbPlayers");


            if (nbPlayers == 2)
            {

                GameObject.FindGameObjectWithTag("StatGreen").SetActive(false);
                GameObject.FindGameObjectWithTag("StatViolet").SetActive(false);
            }
            if (nbPlayers == 3)
            {
                P3.SetActive(true);
                GameObject.FindGameObjectWithTag("StatViolet").SetActive(false);
            }
            else if (nbPlayers == 4)
            {
                P3.SetActive(true);
                P4.SetActive(true);
            }
        }

        if (modeLife)
        {
            int nbPlayers = PlayerPrefs.GetInt("nbLifePlayers");


            if (nbPlayers == 2)
            {

                GameObject.FindGameObjectWithTag("StatGreen").SetActive(false);
                GameObject.FindGameObjectWithTag("StatViolet").SetActive(false);
            }
            if (nbPlayers == 3)
            {
                P3.SetActive(true);
                GameObject.FindGameObjectWithTag("StatViolet").SetActive(false);
            }
            else if (nbPlayers == 4)
            {
                P3.SetActive(true);
                P4.SetActive(true);
            }
        }
    }
}


