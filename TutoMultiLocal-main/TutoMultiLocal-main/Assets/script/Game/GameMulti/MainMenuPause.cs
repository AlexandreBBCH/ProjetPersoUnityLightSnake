using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : MainMenuPause.cs
//Date    : 26.09.2022
//Version : Alpha
public class MainMenuPause : MonoBehaviour
{

    bool isPaused = false;
    GameMananger gm;
    public GameObject pauseMenu;

    public Slider musiqueSlider;
    public Slider bruitageSlider;



    private void Start()
    {

        gm = GameObject.Find("GameManager").GetComponent<GameMananger>();
    }

    /// <summary>
    /// renvoie au menu principal
    /// </summary>
    public void BackToMenu()
    {
        Application.LoadLevel(0);
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {


            if (!isPaused)
            {


                isPaused = true;
                //coordonnance visuel des slideBar
                musiqueSlider.value = PlayerPrefs.GetInt("volumeMusique");
                bruitageSlider.value = PlayerPrefs.GetInt("volumeBruitage");
            


                pauseMenu.SetActive(isPaused);
            }
            else
            {
                isPaused = false;

                pauseMenu.SetActive(isPaused);
            }

        }
    }

    /// <summary>
    /// gere l ouverture du mode pause
    /// </summary>
    public void openMenu()
    {
        if (!isPaused)
        {


            isPaused = true;
            //coordonnance visuel des slideBar
            musiqueSlider.value = PlayerPrefs.GetInt("volumeMusique");
            bruitageSlider.value = PlayerPrefs.GetInt("volumeBruitage");



            pauseMenu.SetActive(isPaused);
        }
        else
        {
            isPaused = false;

            pauseMenu.SetActive(isPaused);
        }
    }




}
