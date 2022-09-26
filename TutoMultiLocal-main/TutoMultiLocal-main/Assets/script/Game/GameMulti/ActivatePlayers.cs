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

    //gere l'affichage en fonction du nombre de joueurs
    void Awake()
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
