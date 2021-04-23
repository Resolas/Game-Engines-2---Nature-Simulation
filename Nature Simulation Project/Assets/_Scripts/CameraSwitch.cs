using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunCam(setTime));    
    }

    public int setTime = 5;

    public GameObject[] myCams;
    private int curCam;


    void SwitchCurCam()
    {

        if (curCam < myCams.Length - 1)
        {
            curCam++;
        }
        else
        {
            curCam = 0;
        }

        for (int i = 0; i < myCams.Length; i++)
        {

            if (i == curCam)
            {

                myCams[i].SetActive(true);

            }
            else
            {
                myCams[i].SetActive(false);
            }

        }

    }

    IEnumerator RunCam(int time)
    {

        while (true)
        {
            yield return new WaitForSeconds(time);


            SwitchCurCam();


        }



    }

}
