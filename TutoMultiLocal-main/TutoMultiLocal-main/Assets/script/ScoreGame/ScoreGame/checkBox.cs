using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkBox : MonoBehaviour
{
    // Start is called before the first frame update

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



    public void itemActive()
    {
        Debug.Log(PlayerPrefs.GetString("itemActif"));
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
