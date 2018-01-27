using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTree : Node {

    [SerializeField]
    private float sporesPerMin;

    [SerializeField]
    private float healthPoints;

    //Starts at zero with full spore production 
    private float integrity;

    //Flag to check if tree is alive
    private bool alive;

    //List of all mushrooms in reaching area of tree
	[SerializeField]
    private List<Mushroom> shroomsIntersected;

    public bool IsAlive
    {
        get
        {
            return alive;
        }

        set
        {
            alive = value;
        }
    }

    void Start () {
        alive = true;
        shroomsIntersected = new List<Mushroom>();      
	}

    public void killTree()
    {
        alive = false;
        foreach (Mushroom shroom in shroomsIntersected)
        {
            shroom.deleteTree(this);
        }
        shroomsIntersected.Clear();
    }
	

	/*
    public void addShroom(Mushroom shroom)
    {
        shroomsIntersected.Add(shroom);
    }
	*/
	//instead of addShroom method
	//Adds a shroom thats within the collider of the tree, even if its not set by the tree itself

	 void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<Mushroom>())
        {
			if(!c.isTrigger)
            	shroomsIntersected.Add(c.gameObject.GetComponent<Mushroom>());
        }
    }

    public bool hasShroom(Mushroom shroom)
    {
        bool result = false;
        if (true)//shroomsIntersected.Find(shroom))
        {
            result = true;
        }

        return result;
    }

    public void getAllIntersectedShrooms()
    {
        foreach(Mushroom shroom in shroomsIntersected)
        {
            Debug.Log(shroom);
        }
    }

	public List<Mushroom> getIntersectedShroomList()
	{
		return shroomsIntersected;
	}

    public float getSporesPerMin()
    {
        return sporesPerMin;
    }

    public void setSporesPerMin(float value)
    {
        sporesPerMin = value;
    }
}
