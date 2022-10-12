using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : Menu.cs
//Date    : 26.09.2022
//Version : Alpha
public class Menu : MonoBehaviour
{
    bool optionOpen = false;
    public GameObject optionMenu;
    public Text nbPlayers;
    public Slider nbPlayersSlider;

    public Text nbLifePlayers;
    public Slider nbPlayersLifeSlider;

    public Text nbSurvivorPlayers;
    public Slider nbPlayersSurvivorSlider;

    GameMananger gm;
  
    public Text nbRound;
    public Slider sliderRound;
 

    public Text nbLife;
    public Slider sldLife;


    public Text txtSurvivorTimer;
    public Slider sldSurvivorTimer;

    public bool modeScore;
    public bool modeLife;
    public bool modeSurvivor;

    private void Start()
    {

        initialisation();
    }

    void initialisation()
    {
        if (modeScore)
        {
            int nb = PlayerPrefs.GetInt("nbPlayers");
            nbPlayersSlider.value = nb;
        }

        if (modeLife)
        {

            int nbLifePlayer = PlayerPrefs.GetInt("nbLifePlayers");
            nbPlayersLifeSlider.value = nbLifePlayer;
        }

        if (modeSurvivor)
        {
            int nbSurvivorPlayer = PlayerPrefs.GetInt("nbSurvivorPlayers");
            nbPlayersSurvivorSlider.value = nbSurvivorPlayer;
        }
    }

    void initialisationMode()
    {
        if (modeScore)
        {
            int nbRound = PlayerPrefs.GetInt("nbRound");
            sliderRound.value = nbRound;
        }

        if (modeLife)
        {
            int nbLife = PlayerPrefs.GetInt("nbLife");
            sldLife.value = nbLife;
        }
        if (modeSurvivor)
        {
            int survivorTimer  = PlayerPrefs.GetInt("survivorTimer");
            sldSurvivorTimer.value = survivorTimer;
        }
    }
    private void Awake()
    {
        initialisationMode();
    }



    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void MainMenu()
    {

        Application.LoadLevel(0);

    }

    /// <summary>
    /// enmene a la page score
    /// </summary>
    public void Play()
    {
        
        Application.LoadLevel(1);
    
    }

    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void PlayLife()
    {

        Application.LoadLevel(5);

    }

    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void PlaySurvivor()
    {

        Application.LoadLevel(7);

    }
    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void MultiMenu()
    {

        Application.LoadLevel(2);

    }

    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void OptionScoreMenu()
    {

        Application.LoadLevel(3);

    }
    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void OptionLifeMenu()
    {

        Application.LoadLevel(4);

    }

    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    public void OptionSurvivorMenu()
    {

        Application.LoadLevel(6);

    }
 

  
    /// <summary>
    /// enmene a la page indiqu�
    /// </summary>
    /// 

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// verif si option active ou pas
    /// </summary>
    public void Option()
    {
        if (!optionOpen)
        {
            optionOpen = true;
         
        }
        else
        {
            optionOpen = false;
           
        }
        optionMenu.SetActive(optionOpen);
        
    }

    /// <summary>
    /// Set le nombre de joueur des option du mode score
    /// </summary>
    public void SetPlayers()
    {
        nbPlayers.text = nbPlayersSlider.value.ToString();
        PlayerPrefs.SetInt("nbPlayers", (int)(nbPlayersSlider.value));
    }

    /// <summary>
    /// Set le nombre de joueur des option du mode life
    /// </summary>
    public void SetLifePlayers()
    {
        nbLifePlayers.text = nbPlayersLifeSlider.value.ToString();
        PlayerPrefs.SetInt("nbLifePlayers", (int)(nbPlayersLifeSlider.value));
    }


    /// <summary>
    /// Set le nombre de joueur des option du mode survivor
    /// </summary>
    public void SetSurvivorPlayers()
    {
        nbSurvivorPlayers.text = nbPlayersSurvivorSlider.value.ToString();
        PlayerPrefs.SetInt("nbSurvivorPlayers", (int)(nbPlayersSurvivorSlider.value));
    }

    /// <summary>
    /// Set le nombre de round du mode score
    /// </summary>
    public void SetRound()
    {
        nbRound.text = "Round " + sliderRound.value.ToString();
        if (sliderRound.value >= 100)
        {
            nbRound.text = "Infinity";

        }
        PlayerPrefs.SetInt("nbRound", (int)(sliderRound.value));
    }

    /// <summary>
    /// Set le nombre de vie du mode life
    /// </summary>

    public void SetLife()
    {

        nbLife.text = sldLife.value +  " Life "  ;
        if (sldLife.value >= 100)
        {
            nbLife.text = "Immortal";


        }
        PlayerPrefs.SetInt("nbLife", (int)(sldLife.value));
    }


    /// <summary>
    /// Set le timer
    /// </summary>
    public void SetSurvivorTimer()
    {

        txtSurvivorTimer.text =  " Time " + sldSurvivorTimer.value;
        PlayerPrefs.SetInt("survivorTimer", (int)(sldSurvivorTimer.value));
    }



}
