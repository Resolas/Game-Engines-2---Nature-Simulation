using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        myNeeds = GetComponent<CreatureNeeds>();

    }


    CreatureNeeds myNeeds;


    private bool isPerched;

    // Update is called once per frame
    void FixedUpdate()
    {


        BirdDecisionTree();


    }


    void BirdDecisionTree()     // Decision behaviour of birds (flying AI)
    {

        if (myNeeds.isDead != true)
        {











        }


    }

}
