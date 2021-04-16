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

    }

    private CreatureNeeds getNeeds;
    private NavMeshAgent myAgent;

    // Update is called once per frame
    void Update()
    {
        
    }


    void RunAITree()
    {


        if (getNeeds.isDead != true)
        {

            // Wander

            if (getNeeds.curFood < getNeeds.maxFood / 2)        // if less than half find food
            {



            }
            else   // Wander
            {
               
                

            }



        }



    }

}
