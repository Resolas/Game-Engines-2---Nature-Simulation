using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnvironmentSwitch : MonoBehaviour
{
    private void Start()
    {
        myColor = mySkyColorDefault;
        mySky.SetColor("_EmissionColor", myColor);
        DynamicGI.UpdateEnvironment();
    }

    public float globalPostPr;

    // Update is called once per frame
    void FixedUpdate()
    {

        if (environmentEffectActive == true)
        {
            if (t < b) t += Time.deltaTime * speed;

            globalPostPr = Mathf.Lerp(a, b, t);


            myColor = Color.Lerp(mySkyColorDefault,mySkyColorFiery,globalPostPr);

            mySky.SetColor("_EmissionColor", myColor);

            DynamicGI.UpdateEnvironment();
        }

        for (int i = 0; i < myProfiles.Length; i ++)
        {

            myProfiles[i].weight = globalPostPr;

        }

        

    }
    public bool environmentEffectActive = false;

    public float speed = 0.2f;
    float a = 0;
    float b = 1;
    float t = 0;

    public PostProcessVolume[] myProfiles;

    public Material mySky;
    public Color myColor;
    
    public Color mySkyColorDefault;
    public Color mySkyColorFiery;

}
