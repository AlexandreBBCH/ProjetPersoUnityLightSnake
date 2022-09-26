using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class GestionOption : MonoBehaviour
{

    public Slider musiqueSlider;
    public Text volumeMusiqueText;
    public float volumeMusique;

    public Slider bruitageSlider;
    public Text bruitageText;
    public float volumeBruitage;



    private void Start()
    {

    

    }



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



    public void GestionSound()
    {
        volumeBruitage = bruitageSlider.value;
        PlayerPrefs.SetInt("volumeBruitage", (int)(bruitageSlider.value));
        volumeBruitage = PlayerPrefs.GetInt("volumeBruitage");
        bruitageText.text = volumeBruitage.ToString();


    }


}
