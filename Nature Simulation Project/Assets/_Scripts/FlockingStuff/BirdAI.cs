using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BirdAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
        myNeeds = GetComponent<CreatureNeeds>();

    }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } } // Saves from doing getComponent everytime
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


    public void FlockMove(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

}
