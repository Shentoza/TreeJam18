using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTree : MonoBehaviour {

    [SerializeField]
    private float sporesPerMin;

    [SerializeField]
    private float healthPoints;

    //Starts at zero with full spore production 
    //More integrity = less spore production
    private float integrity;

    //Flag to check if tree is alive
    private bool alive;

    //List of all mushrooms in reaching area of tree
    private List<Mushroom> shroomsIntersected;
 

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
	

    public bool isAlive()
    {
        return alive;
    }

    public void addShroom(Mushroom shroom)
    {
        shroomsIntersected.Add(shroom);
    }

    public bool hasShroom(Mushroom shroom)
    {
        bool result = false;
        if (shroomsIntersected.Find(shroom))
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

    public float getSporesPerMin()
    {
        return sporesPerMin;
    }

    public void setSporesPerMin(float value)
    {
        sporesPerMin = value;
    }
}
