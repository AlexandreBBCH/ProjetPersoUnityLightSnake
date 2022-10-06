using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : parametreScoreGame.cs
//Date    : 26.09.2022
//Version : Alpha
public class parametreScoreGame : MonoBehaviour
{



    public GameObject detailMenu;
    bool isDetailed = false;
 /// <summary>
 /// ouvre la page detail
 /// </summary>
    public void openDetail()
    {

        if (!isDetailed)
        {
            isDetailed = true;
            detailMenu.SetActive(isDetailed);
        }
        else
        {
            isDetailed = false;
            detailMenu.SetActive(isDetailed);
        }
    }


}
