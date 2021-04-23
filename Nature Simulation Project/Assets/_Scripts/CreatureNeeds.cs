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

        int rng = Random.Range(0,2);
        sex = sexArray[rng];

        StartCoroutine(PassiveConsumptionRate(5));
        StartCoroutine(TimeTillMaturity(1));

    }

    private void FixedUpdate()
    {

        if (isPregnant && sex == "Female")
        {
            birthTime -= 1 * Time.deltaTime;

            if (birthTime <= 0)
            {
                Reproduce();

            }
        }

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

    public string mySpecies = "Animal";
    public byte sizeClass = 0;
    
    public int damage = 20;

    [Header("Reproduction")]
    public string sex;
    private string[] sexArray = new string[]{"Male","Female"};
    public bool isPregnant;
    public bool isMature;
    public int matureTime = 60;
    public int minOffspring = 1;
    public int maxOffspring = 3;
    private int offspringTotal;
    private float birthTime = 60f;
    
    public GameObject myPrefab;

    void Reproduce()
    {
        offspringTotal = Random.Range(minOffspring,maxOffspring);

        for (int i = 0; i < offspringTotal; i++)
        {
          var newOffspring = Instantiate(myPrefab,transform.position,Quaternion.identity);
            var setOffspring = newOffspring.GetComponent<CreatureNeeds>();

            setOffspring.isMature = false;
            setOffspring.isPregnant = false;
            setOffspring.matureTime = 60;


        }

        birthTime = Random.Range(30,120);
        isPregnant = false;

        Debug.Log(gameObject + " Gave Birth");
    }


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

    IEnumerator TimeTillMaturity(int waitTime)
    {

        while (true)
        {

            matureTime--;

            if (matureTime <= 0 || isMature)
            {

                isMature = true;

                //  StopCoroutine("TimeTillMaturity");
                yield break;
            }
            Debug.Log(matureTime);
            yield return new WaitForSeconds(waitTime);
        }

    }

}
