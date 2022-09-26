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
    //ChoixMusique musiqueScript;
    public Text nbRound;
    public Slider sliderRound;
    GameMananger gm;

    public Text nbLife;
    public Slider sldLife;

    public bool modeScore;
    public bool modeLife;

    private void Start()
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
    }



    private void Awake()
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



    }

    private void Update()
    {
  
       
    }


    public void MainMenu()
    {

        Application.LoadLevel(0);

    }
    public void Play()
    {
        
        Application.LoadLevel(1);
    
    }


    public void MultiMenu()
    {

        Application.LoadLevel(2);

    }


    public void OptionScoreMenu()
    {

        Application.LoadLevel(3);

    }

    public void OptionLifeMenu()
    {

        Application.LoadLevel(4);

    }

    public void PlayLife()
    {

        Application.LoadLevel(5);

    }

    public void OptionSurvivorMenu()
    {

        Application.LoadLevel(6);

    }

    public void Quit()
    {
        Application.Quit();
    }
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


    public void SetPlayers()
    {
        nbPlayers.text = nbPlayersSlider.value.ToString();
        PlayerPrefs.SetInt("nbPlayers", (int)(nbPlayersSlider.value));
    }

    public void SetLifePlayers()
    {
        nbLifePlayers.text = nbPlayersLifeSlider.value.ToString();
        PlayerPrefs.SetInt("nbLifePlayers", (int)(nbPlayersLifeSlider.value));
    }


    public void SetRound()
    {
        nbRound.text = "Round " + sliderRound.value.ToString();
        if (sliderRound.value >= 101)
        {
            nbRound.text = "Round infini";

        }
        PlayerPrefs.SetInt("nbRound", (int)(sliderRound.value));
    }

    public void SetLife()
    {

        nbLife.text = sldLife.value +  " Vie(s) "  ;
        if (sldLife.value >= 100)
        {
            nbLife.text = "Vie infinis";

        }
        PlayerPrefs.SetInt("nbLife", (int)(sldLife.value));
    }



}
