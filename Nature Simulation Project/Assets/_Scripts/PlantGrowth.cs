using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour        // for bushes, crops etcs and NOT trees
{
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();

        // Start Growth
        curGrowth = Random.Range(0,0.2f);
        growthRateRange = Random.Range(1,2);
        foodValue = Random.Range(5,50);
    }

    public float growthRate = 0.01f;
    public float growthRateRange;
    public float curGrowth;

    public int tillMaturity;

    public bool isMature;
    public int foodValue;
    public int finalfoodValue;

    Transform myTransform;

    // Update is called once per frame
    void FixedUpdate()
    {

        

        if (isMature != true)       //  1 = max size
        {
            curGrowth += growthRate * growthRateRange * Time.deltaTime;
            myTransform.localScale = new Vector3(1 * curGrowth,1 * curGrowth,1 * curGrowth);

            if (curGrowth > 1)
            {
                isMature = true;
                finalfoodValue = foodValue;
            }
            else
            {
                finalfoodValue = foodValue / 2;
            }

        }

        



    }
}
