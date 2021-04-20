using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CreatureNeeds))]
public class LandAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        getNeeds = GetComponent<CreatureNeeds>();
        myAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(RunAI(time));

    }

    private CreatureNeeds getNeeds;
    private NavMeshAgent myAgent;
    [Header("AI Settings")]
    public float range = 100;
    public int time = 3;
    [SerializeField]
    private bool canEat = false;

    IEnumerator RunAI(int waitTime)
    {

        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            RunAITree();

          //  Debug.Log("TEST");

        }

    }


    void RunAITree()
    {


        if (getNeeds.isDead != true)
        {

            // Wander

            if (getNeeds.curFood < getNeeds.maxFood / 2)        // if less than half find food
            {

                Collider[] getPlants = Physics.OverlapSphere(transform.position,range,1);
                float closest = 1000;
                float currentTarget;
                float getDist;
                Transform finalTarget = null;

                foreach (Collider myPlant in getPlants)
                {

                    if (myPlant.CompareTag("Flora") != true) continue;   // Skip if its not a plant (Trees Excluded)

                    getDist = Vector3.Distance(transform.position, myPlant.transform.position);

                    currentTarget = getDist;

                    if (closest > currentTarget)    // gets the closest out of all
                    {
                        closest = currentTarget;
                        finalTarget = myPlant.transform;
                    }

                    if (finalTarget != null)        // sets destination to target if it exists
                    {

                        myAgent.destination = finalTarget.position;
                        canEat = true;
                    }
                    else    //      stands still
                    {
                        myAgent.destination = transform.position;
                        canEat = false;
                    }

                    Debug.Log(getPlants.Length);
                    Debug.Log(finalTarget);

                }
                

            }
            else   // Wander
            {
                canEat = false;

                Vector3 wanderPos = new Vector3(Random.Range(-20,20),0,Random.Range(-20,20));

                myAgent.destination = wanderPos + transform.position;


            }



        }



    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("TESTCOL");

        if (collision.collider.CompareTag("Flora") && canEat == true)
        {

            var getFood = collision.collider.GetComponent<PlantGrowth>();
            getNeeds.curFood += getFood.finalfoodValue;
            canEat = false;
            Destroy(collision.gameObject);

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }

}
