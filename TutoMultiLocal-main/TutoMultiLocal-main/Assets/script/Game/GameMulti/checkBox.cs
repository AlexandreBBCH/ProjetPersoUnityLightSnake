using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : checkBox.cs
//Date    : 26.09.2022
//Version : Alpha
public class checkBox : MonoBehaviour
{
  
    //creation d'une checkbox textuel de a à z


    public Text chckItem;
    bool itemCheck = true;


    private void Awake()
    {
        if (chckItem.text != null)
        {
            chckItem.text = PlayerPrefs.GetString("itemActif");
        }
        else
        {
            chckItem.text = "Yes";
        }
     
    }


    /// <summary>
    /// Stock vrai si le text vaut vrai et inversemment
    /// </summary>
    public void itemActive()
    {
        if (chckItem.text == "Yes")
        {
            chckItem.text = "No";
            PlayerPrefs.SetString("itemActif", "No");
        }
        else {
            chckItem.text = "Yes";
            PlayerPrefs.SetString("itemActif", "Yes");
        }

    }
}
