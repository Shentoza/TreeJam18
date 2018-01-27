using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Node
{
    public float range;
    int sporeCost = 0,
        blockedMask;

    public int Cost
    {
        get
        {
            return sporeCost;
        }
    }
    static int count;
    public List<Mushroom> mushroomNeighbors;

    //Baeume in Naehrstoffreichweite
    public List<ShroomTree> treeNeighbors;

    public GameObject[] smallMushrooms;

    // Use this for initialization
    void Start()
    {
        blockedMask = LayerMask.GetMask("Blocked");
        mushroomNeighbors = new List<Mushroom>();
        initalizeNeighbour();
        NodeManager.Instance.add_Nodes(this);
        EventManager.OnMinutePassed += spawnSmallMushroom;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void deleteTree(ShroomTree s)
    {
        treeNeighbors.Remove(s);
        if (treeNeighbors.Count == 0)
            die();
    }

    public void die()
    {
        foreach (ShroomTree s in treeNeighbors)
        {
            s.remove_Node(this);
            s.removeIntersectedShroom(this);
        }
        remove_Node(this);
        Destroy(this.gameObject);
    }

    public void spawnSmallMushroom()
    {
        Vector3 randomPos;
        GameObject newShroom;
        bool placed = false;
        int i = 0;
        Ray ray;
        while (!placed && i++ < 10)
        {
            randomPos = transform.position + new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 0.0f,
                UnityEngine.Random.Range(-2.0f, 2.0f)); // This places in a rectangle around the shroom, shold be circle
            ray = new Ray(randomPos + new Vector3(0f, 100f, 0f), Vector3.down);
            if (!Physics.Raycast(ray, Mathf.Infinity, blockedMask))
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    newShroom = Instantiate(smallMushrooms[0], transform);
                }
                else
                {
                    newShroom = Instantiate(smallMushrooms[1], transform);
                }

                newShroom.transform.SetPositionAndRotation(randomPos,
                    Quaternion.Euler(0, UnityEngine.Random.Range(0.0f, 360.0f), 0));
                placed = true;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (!c.isTrigger)
        {
            if (c.gameObject.GetComponent<ShroomTree>())
            {
                treeNeighbors.Add(c.gameObject.GetComponent<ShroomTree>());
                ResourceManager.Instance.add_Tree(c.gameObject.GetComponent<ShroomTree>());
                add_Node(c.gameObject.GetComponent<ShroomTree>());

            }
            else if (c.gameObject.GetComponent<Mushroom>())
            {
                mushroomNeighbors.Add(c.gameObject.GetComponent<Mushroom>());
                add_Node(c.gameObject.GetComponent<Mushroom>());
            }
        }
    }

}
