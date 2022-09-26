using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class parametreScoreGame : MonoBehaviour
{



    public GameObject detailMenu;
    bool isDetailed = false;
    private void Start()
    {

    }

    private void Awake()
    {
   
    }



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
