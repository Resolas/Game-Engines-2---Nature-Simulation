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
    
    public bool isHunter = false;
    
    [SerializeField]
    private bool canEat = false;
    private bool canMate = false;

    IEnumerator RunAI(int waitTime)
    {

        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (isHunter)
            {
                RunAITreeCarnivore();
            }
            else
            {
                RunAITreeHerbivore();
            }

            

          //  Debug.Log("TEST");

        }

    }


    void RunAITreeHerbivore()
    {


        if (getNeeds.isDead != true)
        {

            

            if (getNeeds.curFood < getNeeds.maxFood / 2)        // if less than half find food
            {

                Collider[] getPlants = Physics.OverlapSphere(transform.position, range, 1);
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
                        canMate = false;
                    }
                    else    //      Keep Wandering
                    {
                        Wander();
                    }

                }


            }
            else if (getNeeds.curFood > 70 && getNeeds.sex == "Male") // Find Mate
            {

                Collider[] getMates = Physics.OverlapSphere(transform.position, range, 1);
                float closest = 1000;
                float currentTarget;
                float getDist;
                Transform finalTarget = null;

                foreach (Collider myMate in getMates)
                {

                    if (myMate.CompareTag("Fauna") != true) continue;   // Skip if its not a plant (Trees Excluded)
                    if (myMate.GetComponent<CreatureNeeds>().mySpecies != getNeeds.mySpecies) continue; // skip if not the same species
                    if (myMate.GetComponent<CreatureNeeds>().isPregnant) continue;          // skip if its already pregnant
                    if (myMate.GetComponent<CreatureNeeds>().sex != "Female") continue;

                    getDist = Vector3.Distance(transform.position, myMate.transform.position);

                    currentTarget = getDist;

                    if (closest > currentTarget)    // gets the closest out of all
                    {
                        closest = currentTarget;
                        finalTarget = myMate.transform;
                    }

                    if (finalTarget != null)        // sets destination to target if it exists
                    {

                        myAgent.destination = finalTarget.position;
                        canMate = true;
                    }
                    else    //      stands still
                    {
                        Wander();
                    }

                }

                if (finalTarget == null)
                {
                    Wander();
                }

            }
            else   // Wander
            {
                Wander();


            }

        }

    }

    void RunAITreeCarnivore()
    {


        if (getNeeds.isDead != true)
        {

            // Wander

            if (getNeeds.curFood < getNeeds.maxFood / 2)        // if less than half find food
            {

                Collider[] getAnimals = Physics.OverlapSphere(transform.position, range, 1);
                float closest = 1000;
                float currentTarget;
                float getDist;
                Transform finalTarget = null;

                foreach (Collider myAnimal in getAnimals)           // Hunt
                {

                    if (myAnimal.CompareTag("Fauna") != true) continue;   // Skip if its not a plant (Trees Excluded)
                    if (myAnimal.GetComponent<CreatureNeeds>().mySpecies == getNeeds.mySpecies) continue;   // If same species skip
                    if (myAnimal.GetComponent<CreatureNeeds>().sizeClass > getNeeds.sizeClass) continue;    // if larger skip

                    getDist = Vector3.Distance(transform.position, myAnimal.transform.position);

                    currentTarget = getDist;

                    if (closest > currentTarget)    // gets the closest out of all
                    {
                        closest = currentTarget;
                        finalTarget = myAnimal.transform;
                    }

                    if (finalTarget != null)        // sets destination to target if it exists
                    {

                        myAgent.destination = finalTarget.position;
                        canEat = true;
                    }
                    else    //      stands still
                    {
                        //   myAgent.destination = transform.position;
                        Wander();
                    }

                //    Debug.Log(getAnimals.Length);
                //    Debug.Log(finalTarget);

                }


            }
            else if (getNeeds.curFood > 70 && getNeeds.sex == "Male") // Find Mate
            {

                Collider[] getMates = Physics.OverlapSphere(transform.position, range, 1);
                float closest = 1000;
                float currentTarget;
                float getDist;
                Transform finalTarget = null;

                foreach (Collider myMate in getMates)
                {

                    if (myMate.CompareTag("Fauna") != true) continue;   // Skip if its not a plant (Trees Excluded)
                    if (myMate.GetComponent<CreatureNeeds>().mySpecies != getNeeds.mySpecies) continue; // skip if not the same species
                    if (myMate.GetComponent<CreatureNeeds>().isPregnant) continue;          // skip if its already pregnant
                    if (myMate.GetComponent<CreatureNeeds>().sex != "Female") continue;
                    if (myMate.GetComponent<CreatureNeeds>().isMature != true) continue;

                    getDist = Vector3.Distance(transform.position, myMate.transform.position);

                    currentTarget = getDist;

                    if (closest > currentTarget)    // gets the closest out of all
                    {
                        closest = currentTarget;
                        finalTarget = myMate.transform;
                    }

                    if (finalTarget != null)        // sets destination to target if it exists
                    {

                        myAgent.destination = finalTarget.position;
                        canMate = true;
                        canEat = false;
                    }
                    else    //      Keep Wandering :L
                    {
                        Wander();
                    }

                }

                if (finalTarget == null)
                {
                    Wander();
                }

            }
            else   // Wander
            {

                Wander();

            }

        }

    }

    void Wander()
    {
        canEat = false;
        canMate = false;
        Vector3 wanderPos = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));

        myAgent.destination = wanderPos + transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        //  Debug.Log("TESTCOL");

        if (collision.collider.CompareTag("Flora") && canEat == true && isHunter != true)
        {

            var getFood = collision.collider.GetComponent<PlantGrowth>();
            getNeeds.curFood += getFood.finalfoodValue;
            canEat = false;
            Destroy(collision.gameObject);

        }
        else if (collision.collider.CompareTag("Fauna") && canEat == true && isHunter == true)
        {

            var getFood = collision.collider.GetComponent<CreatureNeeds>();
            getNeeds.curFood += (getFood.curFood / 2) + 10;
            canEat = false;
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("Fauna") && canMate == true &&
            collision.collider.GetComponent<CreatureNeeds>().mySpecies == getNeeds.mySpecies &&
            collision.collider.GetComponent<CreatureNeeds>().isPregnant == false)
        {
            collision.collider.GetComponent<CreatureNeeds>().isPregnant = true;
            getNeeds.curFood -= 30;
            canMate = false;

            Debug.Log(gameObject + " HAD MATED!");
        }

    }

    



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }

}
