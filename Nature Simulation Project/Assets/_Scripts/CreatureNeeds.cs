using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureNeeds : MonoBehaviour      // Stats that creatures need to survive
{
    // Start is called before the first frame update
    void Start()
    {

        curFood = Random.Range(maxFood/2,maxFood);
     //   curWater = Random.Range(maxWater/2,maxWater);
        curRest = Random.Range(maxRest/2,maxRest);
        curHealth = Random.Range(maxHealth/2,maxHealth);


        StartCoroutine(PassiveConsumptionRate(5));

    }

    public int maxFood = 100;
 //   public int maxWater = 100;
    public int maxRest = 100;
    public int maxHealth = 100;

    public bool isDead = false;

    public int curFood;
 //   public int curWater;
    public int curRest;
    public int curHealth;

    

    IEnumerator PassiveConsumptionRate(int waitTime)
    {

        while (true)
        {

            curFood--;
         //   curWater--;
            curRest--;

            if (curFood < 0 /* || curWater < 0 */)
            {
                maxHealth--;
            }

            yield return new WaitForSeconds(waitTime);

        }


    }

}
