using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    private void Start()
    {
        myAudioSrc = GetComponent<AudioSource>();

        timer = Random.Range(tMin,tMax);

        StartCoroutine(PlaySound(timer));
    }

    public AudioClip myClip;
    private AudioSource myAudioSrc;

    public float timer;
    public float tMin = 3;
    public float tMax = 10;

    IEnumerator PlaySound(float waitTime)
    {

        while (true)
        {


            yield return new WaitForSeconds(waitTime);

            myAudioSrc.clip = myClip;
            myAudioSrc.pitch = Random.Range(0.8f,1.2f);

            myAudioSrc.Play();




            timer = Random.Range(tMin,tMax);
            waitTime = timer;


        }

    }


}
