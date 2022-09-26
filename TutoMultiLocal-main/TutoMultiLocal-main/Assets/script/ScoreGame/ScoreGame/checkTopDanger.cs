using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine.UI;
using System;


public class checkTopDanger : MonoBehaviour
{
    Player p;
    public RaycastHit2D top;
    int detectionDanger = 5;
    void Start()
    {
     
    }
    private void Awake()
    {
 
    }
    // Update is called once per frame
    private void Update()
    {

        string test = checkDangerTop();

   Debug.Log(top.collider.tag);
    }
    private void LateUpdate()
    {
        top = Physics2D.Raycast(transform.position, transform.up, detectionDanger, ~p.layer);
    }
    public string checkDangerTop()
    {

        //verifie si il y a un danger en haut
        if (top.collider.tag == "border" || top.collider.tag == "mur")
        {
            return "ya";
        }
        else
        {
            return "no";
        }
    }
}
