using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systemics : MonoBehaviour      //  Fire mechanics and stuff e.g. fireballs
{
    // Start is called before the first frame update
    void Start()
    {

        if (fireSource)
        {
            StartCoroutine(FireSpread(3));
        }

        if (disableLifeSettings == false)
        {
            StartCoroutine(CheckHealth(1));
        }

    }

    [Header("Fire Settings")]

    public bool fireSource;
    public int range = 25;
    public int time = 5;    // multiplied by waitTime
    public int fireDuration = 5;
    public GameObject fireEffect;

    
    IEnumerator FireSpread(int waitTime)
    {

        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Collider[] getLife = Physics.OverlapSphere(transform.position,range, 1);

            foreach (Collider curLife in getLife)
            {
                if (curLife.CompareTag("Flora") || curLife.CompareTag("Fauna") || curLife.CompareTag("Tree"))
                {
                    var getInfo = curLife.GetComponent<Systemics>();

                    if (getInfo.disableLifeSettings == false && getInfo.isFlammable && getInfo.onFire != true)
                    {
                        var newFire = Instantiate(fireEffect, getInfo.transform.position, Quaternion.identity);
                        newFire.transform.parent = getInfo.transform;

                        getInfo.fireTime = fireDuration;
                        getInfo.onFire = true;


                    }

                }

                

            }

            if (time < 0)
            {
                Destroy(gameObject);
            }

        }

    }


    [Header("Fauna & Flora Settings")]
    public bool disableLifeSettings = false;
    public int health = 10;
    public bool onFire = false;     // can become a new source
    public bool isFlammable = true;
    public int fireTime = 0;

    IEnumerator CheckHealth(int waitTime)
    {

        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (fireTime > 0)
            {
                fireTime--;

            }
            else
            {
                onFire = false;
            }

            if (onFire && isFlammable)
            {
                health--;

                Collider[] getLife = Physics.OverlapSphere(transform.position, range, 1);

                foreach (Collider curLife in getLife)
                {
                    if (curLife.CompareTag("Flora") || curLife.CompareTag("Fauna") || curLife.CompareTag("Tree"))
                    {
                        var getInfo = curLife.GetComponent<Systemics>();

                        if (getInfo.disableLifeSettings == false && getInfo.isFlammable && getInfo.onFire != true)
                        {
                            var newFire = Instantiate(fireEffect, getInfo.transform.position, Quaternion.identity);
                            newFire.transform.parent = getInfo.transform;

                            getInfo.fireTime = fireDuration;
                            getInfo.onFire = true;


                        }

                    }



                }


            }

            if (health < 0)
            {

                Destroy(gameObject);

            }





        }

    }

    private void OnDrawGizmos()
    {
        if (fireSource)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        

            
    }

}
