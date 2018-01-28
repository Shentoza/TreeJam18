using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTree : Node {

    [SerializeField]
    private float currentHP;

    private static float maxHP = 100.0f;

    //Starts at zero with full spore production 
    [SerializeField]
    private float currentIntegrity = .0f;

    private static float maxIntegrity = 100.0f;

    //Flag to check if tree is alive
    public bool alive;

	[SerializeField]
	private bool infected = false;

	public bool dies = false;

    //List of all mushrooms in reaching area of tree
	[SerializeField]
    private List<Mushroom> shroomsIntersected;

    [SerializeField]
    GameObject Blattwerk;

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
		currentHP = maxHP;
		initalizeNeighbour ();
	}

    public void killTree()
    {
        alive = false;
        foreach (Mushroom shroom in shroomsIntersected)
        {
            shroom.deleteTree(this);
        }
		foreach (Node n in Neighbours) {
			n.remove_Node (this);
		}
		NodeManager.Instance.check_connections ();
        shroomsIntersected.Clear();
		gameObject.GetComponent<SphereCollider> ().radius = 0f;
    }


    //Trigger for adding shrooms connected to this tree
	 void OnTriggerEnter(Collider c)
    {
		if (!this.isInfected ()) {
			if (c.gameObject.GetComponent<Mushroom> ()) {
				if (!c.isTrigger) {
					shroomsIntersected.Add (c.gameObject.GetComponent<Mushroom> ());
					add_Node (c.gameObject.GetComponent<Mushroom> ());
				}
			}
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

    public int getIntersectedShroomCount()
    {
        return shroomsIntersected.Count;
    }

	public void removeIntersectedShroom(Mushroom m)
	{
		shroomsIntersected.Remove (m);
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

    public void incrIntegrity()
    {
        currentIntegrity++;
        if(currentIntegrity == maxIntegrity)
        {
            if(Blattwerk != null)
            {
                Destroy(Blattwerk);
            }
        }
    }

    public float getMaxIntegrity()
    {
        return maxIntegrity;
    }
}
