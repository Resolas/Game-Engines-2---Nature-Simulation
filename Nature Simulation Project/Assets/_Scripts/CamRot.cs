using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour
{

    public float speed;
    Transform myTransform;

    private void Start()
    {

        myTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        myTransform.Rotate(0,speed * Time.deltaTime,0);


    }




}
