using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erupt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<Collider>();
        myMeshR = GetComponent<MeshRenderer>();
        

        StartCoroutine(Eruption(time));
    }

    public float time = 120f;

    Collider myCol;
    MeshRenderer myMeshR;

    public GameObject[] ObjHolder;
    public EnvironmentSwitch getEnv;

    public AudioClip myClip1;
    public AudioSource myAudioSrc1;

    public AudioClip myClip2;
    public AudioSource myAudioSrc2;

    public AudioSource myAudioSrc3;

    IEnumerator Eruption(float waitTime)
    {

        while (true)
        {

            yield return new WaitForSeconds(waitTime);

            myCol.enabled = false;
            myMeshR.enabled = false;
            
            for (int i = 0; i < ObjHolder.Length; i++)
            {

                ObjHolder[i].SetActive(true);

            }
            getEnv.environmentEffectActive = true;

            myAudioSrc1.clip = myClip1;
            myAudioSrc1.Play();

            myAudioSrc2.clip = myClip2;
            myAudioSrc2.Play();

            myAudioSrc3.Stop();

            yield break;

        }

    }



}
