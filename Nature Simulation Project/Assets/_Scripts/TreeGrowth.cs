using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour     // growth script for trees
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateBranches();
    }

    public Transform[] myBranchPoints;

    public SpawnTable getSpawnTable;

    public bool ignoreRange = false; // ignore and use set value instead
    public int spawnPerPointRange = 3;
    public int spawnNum = 1;

    void GenerateBranches()
    {

        for (int i = 0; i < myBranchPoints.Length; i++)
        {

            

                var numOfBranch = Random.Range(0, spawnPerPointRange);

            if (ignoreRange == true) numOfBranch = spawnNum;


                for (int j = 0; j < numOfBranch; j++)
            {
                int branchRng = Random.Range(0,getSpawnTable.mySpawnList.Length);

                var newBranch = Instantiate(getSpawnTable.mySpawnList[branchRng],
                    myBranchPoints[i].transform.position + new Vector3(0,Random.Range(-2,2),0),
                    Quaternion.Euler(Random.Range(0,-45),Random.Range(0,360),0));

                newBranch.transform.SetParent(gameObject.transform);

            }


        }




    }

    
}
