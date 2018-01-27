using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float range;
    int sporeCost;
    static int count;
    public MushroomManager manager;
    List<Mushroom> mushroomNeighbors;
    public List<ShroomTree> treeNeighbors;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void deleteTree(ShroomTree s)
    {
        treeNeighbors.Remove(s);
    }

    void OnTriggerEnter(Collider c)
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

<<<<<<< HEAD
    public int Cost
    {
        get
        {
            return sporeCost;
=======
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<ShroomTree>())
        {
            treeNeighbors.Remove(c.gameObject.GetComponent<ShroomTree>());
        }
        else if (c.gameObject.GetComponent<Mushroom>())
        {
            mushroomNeighbors.Remove(c.gameObject.GetComponent<Mushroom>());
>>>>>>> dca79814ca36d03b38cbc322290eb0873b7e5c87
        }
    }
}
