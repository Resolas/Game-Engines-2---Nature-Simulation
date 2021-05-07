using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{

    public bool explodeOnImpact;

    public float timeToExplode = 20;
    public float range = 20;

    public GameObject bounceEff;
    public GameObject ExplodeEff;
    public GameObject fireEff;

    private void FixedUpdate()
    {
        if (explodeOnImpact != true)
        {
            timeToExplode -= 1 * Time.deltaTime;
        }
        if (timeToExplode <= 0)
        {
            SetFire();
            Instantiate(ExplodeEff,transform.position,Quaternion.identity);

            Destroy(gameObject);
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if (explodeOnImpact != true)
        {
            foreach (ContactPoint contact in collision.contacts)
            {

                Instantiate(bounceEff,contact.point,Quaternion.identity);
                timeToExplode -= 1f;

            }


        }
        else
        {


            SetFire();
            Instantiate(ExplodeEff,transform.position,Quaternion.identity);

            Destroy(gameObject);
        }


    }

    void SetFire()
    {

        Collider[] getFlammables = Physics.OverlapSphere(transform.position,range);

        foreach (Collider flammable in getFlammables)
        {

        //    if (flammable.CompareTag("Flora") != true || flammable.CompareTag("Fauna") != true) continue;       // skip if object is neither of those

            Systemics getSys = flammable.GetComponent<Systemics>();

            if (getSys != null)
            {
                if (getSys.isFlammable == false) continue; // skip if not flammable

                if (flammable.CompareTag("Flora") || flammable.CompareTag("Tree"))
                {
                    var parentFire = Instantiate(fireEff, flammable.transform.position, Quaternion.identity);
                    parentFire.transform.parent = flammable.transform;
                }
                else if (flammable.CompareTag("Fauna"))
                {
                    var parentFire = Instantiate(fireEff,flammable.transform.position,Quaternion.identity);
                    parentFire.transform.parent = flammable.transform;
                }
                

                getSys.fireTime = 5;
                getSys.onFire = true;

                Debug.Log(flammable.name + " Is On Fire!");
            }
            



        }



    }

}
