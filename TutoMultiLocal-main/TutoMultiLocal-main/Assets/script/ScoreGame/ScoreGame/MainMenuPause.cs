using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
