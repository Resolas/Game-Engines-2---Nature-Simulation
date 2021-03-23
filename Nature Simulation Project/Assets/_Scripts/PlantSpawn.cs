using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GeneratePlants();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int loopX = 50;
    int loopZ = 50;

    float spacing = 10;
    int rngRange = 10;

    public GameObject[] myPlants;
	public string targetTag;

    void GeneratePlants()
    {



        for (int i = 0; i < loopX; i++)
        {

            for (int j = 0; j < loopZ; j++)
            {

                int rngX = Random.Range(-rngRange,rngRange);
                int rngZ = Random.Range(-rngRange,rngRange);

             //   float xPos = transform.position.x + (spacing * )
             //       float zPos

                RaycastHit hit;

                if (Physics.Raycast(transform.position + new Vector3(spacing * i + rngX, transform.position.y,spacing * j + rngZ),Vector3.down, out hit,Mathf.Infinity))
                {
                

                    


                    if (hit.collider.CompareTag(targetTag))
                    {
                        int rngPlant = Random.Range(0, myPlants.Length);

                        var newPlant = Instantiate(myPlants[rngPlant], hit.point, Quaternion.identity);

                        newPlant.transform.SetParent(hit.transform);
                    }

                    

                }


            }



        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3((spacing * loopX)/2,0,(spacing * loopZ)/2),new Vector3(spacing * loopX,1, spacing * loopZ));

    }

}
