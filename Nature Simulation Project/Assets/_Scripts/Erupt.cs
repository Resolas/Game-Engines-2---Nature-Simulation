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


            yield break;

        }

    }



}
