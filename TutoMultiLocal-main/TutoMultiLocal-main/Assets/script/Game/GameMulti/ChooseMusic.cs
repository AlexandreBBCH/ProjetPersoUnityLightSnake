using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : ChoixMusique.cs
//Date    : 26.09.2022
//Version : Alpha
public class ChoixMusique : MonoBehaviour
{


    public Dropdown musique;
    int musiqueIndex;
    public GameObject objetMusique;
    public AudioClip ZaZa;
    public AudioClip ReTron;
    public AudioClip Processing;

  /// <summary>
  /// Methode servant � changer de musique
  /// </summary>
    public void selectionMusique()
    {
        if(objetMusique == null)
        {
            objetMusique = GameObject.Find("Musique");
        }
     
        musiqueIndex = musique.value;
        objetMusique.GetComponent<AudioSource>().Stop();
        switch (musiqueIndex)
        {
            case 0:
                objetMusique.GetComponent<AudioSource>().clip = ReTron;
                break;
            case 1:
                objetMusique.GetComponent<AudioSource>().clip = ZaZa;
                break;
            case 2:
                objetMusique.GetComponent<AudioSource>().clip = Processing;
                break;
        }
        objetMusique.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("choixMusique",musiqueIndex);
    }


  

}
