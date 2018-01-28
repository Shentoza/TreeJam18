using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanter : MonoBehaviour {

    [SerializeField]
    int numOfTrees;
    GameObject plane;

    [SerializeField]
    ShroomTree[] treePrefabs;

    // Use this for initialization
    void Start() {
        plantTrees();
    }

    public Vector3 getTreePosition()
    {
        Vector3 result = new Vector3(0, 0, 0);
        bool freeSpace = false;
        while (!freeSpace)
        {
            //Find x,y on plane
            float x = Random.Range(transform.localScale.x * (-5.0f), transform.localScale.x * 5.0f);
            float z = Random.Range(transform.localScale.z * (-5.0f), transform.localScale.z * 5.0f);

            Ray ray = new Ray(new Vector3(x, 100, z), new Vector3(0, -1, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.collider.GetComponentInParent<ShroomTree>() != null)
            {
                continue;
            }              
            else
            {
                result = new Vector3(x, 0, z);
                freeSpace = true;
            }

        }
        return result;
    }

    public void plantTrees()
    {
        List<ShroomTree> listOfTrees = new List<ShroomTree>();
        int planted = 0;

        while (planted < numOfTrees)
        {
            Vector3 position;
            position = getTreePosition();
            ShroomTree model = treePrefabs[Random.Range(0, treePrefabs.Length)];
            Quaternion randomRotation = Random.rotation;
            listOfTrees.Add(Instantiate<ShroomTree>(model, position, Quaternion.Euler(0,0,Random.Range(0,360f))));
            planted++;
        }

        foreach(ShroomTree tree in listOfTrees)
        {
            Destroy(tree.transform.GetChild(2).gameObject);
        }
    }
   
}
