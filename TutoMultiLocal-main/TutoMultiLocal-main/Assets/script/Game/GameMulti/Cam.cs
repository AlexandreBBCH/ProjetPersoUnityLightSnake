using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Projet  : Light Snake
//Auteur  : Alexandre Babich
//Class   : Cam.cs
//Date    : 26.09.2022
//Version : Alpha
public class Cam : MonoBehaviour
{
    //gestion de la camera et de l'audio
    public AudioClip boomSfx;
    public AudioClip chhh;
    bool shakingCam;
  
    //Joue un son
    public void PlayBoumSfx()
    {
        GetComponent<AudioSource>().volume = (float)PlayerPrefs.GetInt("volumeBruitage")/100;
        GetComponent<AudioSource>().PlayOneShot(boomSfx);
    }

    public void PlayChhhh()
    {
            StartCoroutine("wait");
    }
    
   
    public void Shake(float duration, float amount,float intensity)
    {
        if (!shakingCam)
        {
            StartCoroutine(ShakeCam(duration, amount, intensity));
        }
    }


    //tuto unity pour faire cette effet  
    //gere le mouvement de la cam
    IEnumerator ShakeCam(float dur, float amount,float intensity)
    {
        float t = dur;
        Vector3 originalPos = Camera.main.transform.localPosition;
        Vector3 targetPos = Vector3.zero;
        shakingCam = true;

        while ( t > 0.0f)
        {
            if (targetPos == Vector3.zero)
            {
                targetPos = originalPos + (Random.insideUnitSphere * amount);
            }

            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPos, intensity * Time.deltaTime);

            if (Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
            {
                targetPos = Vector3.zero;
            }

            t -= Time.deltaTime;
            yield return null;


        }

        Camera.main.transform.localPosition = originalPos;
        shakingCam=false;




    }

}
