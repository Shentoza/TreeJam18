using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTree : Node {

    [SerializeField]
    private float sporesPerMin;

    private float currentHP;

    [SerializeField]
    private static float maxHP;

    //Starts at zero with full spore production 
    private float currentIntegrity = .0f;

    [SerializeField]
    private static float maxIntegrity = 100.0f;

    //Flag to check if tree is alive
    private bool alive;

    private bool infected;

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
        infected = false;
        shroomsIntersected = new List<Mushroom>();
        currentHP = maxHP;
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


    //Trigger for adding shrooms connected to this tree
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
        if (shroomsIntersected.Contains(shroom))
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

    public void dealDamage(float dmg)
    {
        currentHP -= dmg;
    }

    public float getHP()
    {
        return currentHP;
    }

    public void setInfection(bool value)
    {
        infected = value;
    }

    public bool isInfected()
    {
        return infected;
    }

    public float getIntegrity()
    {
        return currentIntegrity;
    }

    public float getMaxIntegrity()
    {
        return maxIntegrity;
    }
}
