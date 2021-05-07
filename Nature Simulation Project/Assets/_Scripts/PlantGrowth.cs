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
        curGrowth = Random.Range(0,0.7f);
        growthRateRange = Random.Range(1,2);
        foodValue = Random.Range(minFood,maxFood);
    }

    public float growthRate = 0.01f;
    public float growthRateRange;
    public float curGrowth;

    public int tillMaturity;

    public bool isMature;
    public bool hasSpread;
    public int foodValue;
    public int finalfoodValue;
    public int spreadTime;

    [Range(0,100)]
    public int minFood = 5;
    [Range(0,100)]
    public int maxFood = 10;

    public GameObject myPrefab;
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

        if (isMature && hasSpread == false)
        {
            StartCoroutine(SpreadPlants(30));
            hasSpread = true;
            Debug.Log("PLANTSPREAD STARTED");
        }



    }

    IEnumerator SpreadPlants(int waitTime)      // Do this only once
    {

        while (true)
        {

            yield return new WaitForSeconds(waitTime);

            int plantCount = Random.Range(1,3);

            for (int i = 0; i < plantCount; i++)
            {
                float rngX = Random.Range(-5f,5f);
                float rngZ = Random.Range(-5f,5f);

                RaycastHit hit;

                if (Physics.Raycast(transform.position + new Vector3(rngX, 10, rngZ), Vector3.down, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("GroundFertile"))
                    {
                        GameObject newPlant = Instantiate(myPrefab, hit.point, Quaternion.Euler(0, Random.Range(0, 360), 0));
                        PlantGrowth setPlant = newPlant.GetComponent<PlantGrowth>();

                        setPlant.curGrowth = Random.Range(0f, 0.05f);
                        setPlant.foodValue = Random.Range(5, 50);
                        setPlant.curGrowth = Random.Range(0, 0.2f);
                        setPlant.growthRateRange = Random.Range(1, 2);
                        setPlant.isMature = false;
                        setPlant.hasSpread = false;

                    }

                    

                }

                

            }
            Debug.Log("PLANTSPREAD");
            yield break;

        }

    }
}
