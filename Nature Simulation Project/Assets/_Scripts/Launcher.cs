using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Intervals(timer));

    }

    public GameObject myPrefab;

    public float power = 10f;
    public float accuracy = 5f;

    public float timer = 1f;

   

    IEnumerator Intervals(float waitTime)
    {

        while (true)
        {

            yield return new WaitForSeconds(waitTime);

            FireObject();
                


        }

    }

    void FireObject()
    {

        var newObject = Instantiate(myPrefab,transform.position,Quaternion.identity);

        var setRB = newObject.GetComponent<Rigidbody>();

        float accX = Random.Range(-accuracy,accuracy);
        float accY = Random.Range(-accuracy,accuracy);

        setRB.velocity = (transform.forward * power) + new Vector3(accX,accY,0);
    }
}
