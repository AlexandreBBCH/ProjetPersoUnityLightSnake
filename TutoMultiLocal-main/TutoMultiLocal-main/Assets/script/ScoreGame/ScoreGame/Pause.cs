using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
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

    public void BackToMenuMulti()
    {
        Application.LoadLevel(2);
    }
    public void OptionScoreMenu()
    {

        Application.LoadLevel(3);

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
                GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 0;


                pauseMenu.SetActive(isPaused);
            }
            else
            {
                isPaused = false;
                GameObject.Find("GameManager").GetComponent<GameMananger>().gameSpeed = 1;
                pauseMenu.SetActive(isPaused);
            }
           
        }
    }




}
