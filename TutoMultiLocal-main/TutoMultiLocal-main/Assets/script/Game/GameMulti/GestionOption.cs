using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : GestionOption.cs
//Date    : 26.09.2022
//Version : Alpha
public class GestionOption : MonoBehaviour
{

    public Slider musiqueSlider;
    public Text volumeMusiqueText;
    public float volumeMusique;
    public Slider bruitageSlider;
    public Text bruitageText;
    public float volumeBruitage;

    public Toggle tglRules;

  

    /// <summary>
    ///  c est ici que l ont recupere les parametre audio au prealable choisis ainsi que la gestion de la musique
    /// </summary>
    public void GestionAudio()
    {
        volumeMusique = musiqueSlider.value;
        volumeMusique /= 100;
        PlayerPrefs.SetInt("volumeMusique", (int)(musiqueSlider.value));
        volumeMusique = PlayerPrefs.GetInt("volumeMusique");
        volumeMusiqueText.text = volumeMusique.ToString();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("song");
        foreach (GameObject obj in objs)
        {
            if (obj.GetComponent<AudioSource>() != null)
            {
                AudioSource Musique = obj.GetComponent<AudioSource>();
                Musique.volume = volumeMusique;
                Musique.GetComponent<AudioSource>().volume = volumeMusique / 100;
               
            }
        }
    }

    /// <summary>
    /// c est ici que l ont recupere les parametre audio au prealable choisis ainsi que la gestion des �bruitage"
    /// </summary>
    public void GestionSound()
    {
        volumeBruitage = bruitageSlider.value;
        PlayerPrefs.SetInt("volumeBruitage", (int)(bruitageSlider.value));
        volumeBruitage = PlayerPrefs.GetInt("volumeBruitage");
        bruitageText.text = volumeBruitage.ToString();


    }


    public void activationRule()
    {
        //if (tglRules.isOn)
        //{
        //    PlayerPrefs.SetInt("ruleActivated", 1);
       
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("ruleActivated", 0);
       
        //}

      

    }

}
