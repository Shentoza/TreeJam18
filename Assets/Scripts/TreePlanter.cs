using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanter : MonoBehaviour {

    [SerializeField]
    int numOfTrees;
    GameObject plane;
    [SerializeField]
    GameObject tree1;
    [SerializeField]
    GameObject tree2;

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
            float x = Random.Range(0, 100);
            float z = Random.Range(0, 100);

            Ray ray = new Ray(new Vector3(x, 100, z), new Vector3(0, -1, 0));
            //Debug.DrawRay(new Vector3(x, 100, z), new Vector3(0, -1, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            //Debug.DrawLine(new Vector3(x, 100, z), hit.point, Color.cyan);
            if (hit.collider.GetComponentInParent<ShroomTree>() != null)
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.cyan);
                Debug.Log("Neue Position auswürfeln");
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
        int planted = 0;
        while (planted < numOfTrees)
        {
            Vector3 position;
            position = getTreePosition();
            float treeModel = Random.Range(0, 100);
            Instantiate((treeModel <= 50.0f) ? tree1 : tree2, position, tree1.transform.rotation);

            if (treeModel <= 50.0f)
                    Instantiate(tree1, position, tree1.transform.rotation);
                else
                    Instantiate(tree2, position, tree1.transform.rotation);
                planted++;
            }
        }
}
