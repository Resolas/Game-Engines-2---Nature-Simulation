using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public BirdAI agentPrefab;
    List<BirdAI> agents = new List<BirdAI>();
    public BirdFlockBehaviour behaviour;

    [Range(10, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.05f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 10f; // how fast
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f; // how close can they be
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }



    // Start is called before the first frame update
    void Start()
    {

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i ++)
        {

            BirdAI newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
                transform
                );

            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
                

        }


    }

    // Update is called once per frame
    void Update()
    {

        foreach (BirdAI agent in agents)
        {

            List<Transform> context = GetNearbyObjects(agent);
    
            Vector3 move = behaviour.CalculateMove(agent,context,this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.FlockMove(move);
            


        }
        
    }

    List<Transform> GetNearbyObjects(BirdAI agent)
    {

        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);

        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {

                context.Add(c.transform);

            }

        }
        return context;

    }


}
