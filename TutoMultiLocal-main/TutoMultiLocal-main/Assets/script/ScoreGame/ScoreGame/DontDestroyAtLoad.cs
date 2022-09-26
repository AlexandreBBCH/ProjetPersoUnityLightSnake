using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DontDestroyAtLoad : MonoBehaviour
{
    public GestionOption option;
    public Cam audioBruitage;
    //public AudioClip boomSfx;

    //gestion changement Musique //choixMusique.cs
    public Dropdown musique;
    public AudioClip ZaZa;
    public AudioClip ReTron;
    public AudioClip Processing;
    ChoixMusique musiqueScript;

   

    int musiqueIndex;



    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        musiqueIndex = PlayerPrefs.GetInt("choixMusique");
       
        switch (musiqueIndex)
        {
            case 1:

                GetComponent<AudioSource>().Stop();
                musique.value = 1;
                GetComponent<AudioSource>().clip = ZaZa;
                GetComponent<AudioSource>().Play();
                break;
            case 2:

                GetComponent<AudioSource>().Stop();
                musique.value = 2;
                GetComponent<AudioSource>().clip = Processing;
                GetComponent<AudioSource>().Play();
                break;
            default:
                GetComponent<AudioSource>().clip = ReTron;
                break;
        }



        GameObject[] objs = GameObject.FindGameObjectsWithTag("song");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        option.musiqueSlider.value = PlayerPrefs.GetInt("volumeMusique"); ;



        option.volumeBruitage = PlayerPrefs.GetInt("volumeBruitage");
        option.bruitageSlider.value = option.volumeBruitage;
        option.bruitageText.text = option.volumeBruitage.ToString();
        GameObject[] objs2 = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject obj2 in objs2)
        {
            if (obj2.GetComponent<AudioSource>() != null)
            {
                AudioSource bruitage = obj2.GetComponent<AudioSource>();
                bruitage.volume = option.volumeBruitage;
                bruitage.GetComponent<AudioSource>().volume = option.volumeBruitage / 100;
            }
        }
   
      
     
    }
  

    public void choixMusique()
    {
    
    }


}
