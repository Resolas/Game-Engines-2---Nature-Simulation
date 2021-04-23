using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BirdFlockBehaviour : ScriptableObject
{

    public abstract Vector3 CalculateMove(BirdAI aiAgent, List<Transform> context, Flock flock);

}
