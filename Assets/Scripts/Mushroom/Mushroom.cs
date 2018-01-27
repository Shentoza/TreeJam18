using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float range;
    int sporeCost;
    static int count;
    MushroomManager manager;
    List<Mushroom> mushroomNeighbors;
    List<ShroomTree> treeNeighbors;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void detectNeighbors()
    {
        foreach(Mushroom m in manager.mushrooms)
        {
            if (Vector3.Distance)
            mushroomNeighbors.Add(m);
        }
    }

    void deleteTree(ShroomTree s)
    {
        treeNeighbors.Remove(s);
    }

    void OnTriggerEnter(Collision c)
    {
        if (c.gameObject.GetComponent<ShroomTree>())
        {
            treeNeighbors.Add(c.gameObject.GetComponent<ShroomTree>());
        }
        else if (c.gameObject.GetComponent<Mushroom>())
        {
            mushroomNeighbors.Add(c.gameObject.GetComponent<Mushroom>());
        }
    }

    public int Cost
    {
        get
        {
            return sporeCost;
        }
    }
}
