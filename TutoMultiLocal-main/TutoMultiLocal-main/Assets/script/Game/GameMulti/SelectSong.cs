using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class SelectSong : MonoBehaviour
{
    public Dropdown musique;
    int musiqueIndex;
    public GameObject objetMusique;
    public AudioClip ZaZa;
    public AudioClip ReTron;
    public AudioClip Processing;
    public AudioClip LightSnakeTheme;


    public AudioClip[] allMusic;

    private void Start()
    {
        allMusic = new AudioClip[] {ReTron,ZaZa,Processing, LightSnakeTheme};
    }


    /// <summary>
    /// sert a selectionnez la musique souhaité
    /// </summary>
    public void selectionMusique()
    {
        if (objetMusique == null)
        {
            objetMusique = GameObject.Find("Musique");
        }
        musiqueIndex = musique.value;
        objetMusique.GetComponent<AudioSource>().Stop();
        objetMusique.GetComponent<AudioSource>().clip = allMusic[musiqueIndex];
        objetMusique.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("choixMusique", musiqueIndex);
    }


}
