using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Node
{
    public float range;
    int sporeCost;

    public int Cost{
        get{
            return sporeCost;
        }
    }
    static int count;
    public MushroomManager mushMan;
    public ResourceManager resMan;
    List<Mushroom> mushroomNeighbors;
    
    //Baeme in Naehrstoffreichweite
    public List<ShroomTree> treeNeighbors;

	// Use this for initialization
	void Start () {
        resMan = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        if (!resMan)
            Debug.Log("[Mushroom] Resource Manager not Found!");
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void deleteTree(ShroomTree s)
    {
        treeNeighbors.Remove(s);
        if (treeNeighbors.Count == 0)
            die();
    }

    public void die()
    {
        //foreach()
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<ShroomTree>())
        {
            treeNeighbors.Add(c.gameObject.GetComponent<ShroomTree>());
            if (resMan.hasTree(c.gameObject.GetComponent<ShroomTree>()))
            {
                resMan.add_Tree(c.gameObject.GetComponent<ShroomTree>());
            }
        }
        else if (c.gameObject.GetComponent<Mushroom>())
        {
            mushroomNeighbors.Add(c.gameObject.GetComponent<Mushroom>());
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<ShroomTree>())
        {
            treeNeighbors.Remove(c.gameObject.GetComponent<ShroomTree>());
        }
        else if (c.gameObject.GetComponent<Mushroom>())
        {
            mushroomNeighbors.Remove(c.gameObject.GetComponent<Mushroom>());
        }
    }
}
