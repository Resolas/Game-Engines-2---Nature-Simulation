using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LookAtAnimal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LookNearest();
    }

    [Header("Settings")]
    public float range = 100f;
    public float speed = 10f;

    Rigidbody myRB;
  //  [SerializeField]
  //  private Transform myTarget;

    void LookNearest()
    {

        Collider[] getAnimals = Physics.OverlapSphere(transform.position,range);
        float closest = Mathf.Infinity;
        float curTarget;
        float getDist;
        Transform finalTarget = null;

        foreach (Collider animal in getAnimals)
        {

            if (animal.CompareTag("Fauna") != true) continue;

            getDist = Vector3.Distance(transform.position,animal.transform.position);

            curTarget = getDist;

            if (closest > curTarget)
            {
                closest = curTarget;
                finalTarget = animal.transform;
            }

            

        }

        if (finalTarget != null)
        {
            Debug.Log("TARGET "+finalTarget.name);
            var targetRot = Quaternion.LookRotation(finalTarget.position - transform.position);

            myRB.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, speed * Time.deltaTime));

        }

    }


}
