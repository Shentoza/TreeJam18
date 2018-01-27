using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTree : MonoBehaviour {

    [SerializeField]
    private float sporesPerMin;

    [SerializeField]
    private float maxHP;

    private float currentHP;

    [SerializeField]
    private float maxIntegrity;

    //Starts at zero with full spore production 
    //if integrity == maxIntegrity, spore production stops
    private float currentIntegrity;

    //Flag to check if tree is alive
    private bool alive;

    private bool infected;

    //List of all mushrooms in reaching area of tree
    private List<Mushroom> shroomsIntersected;

    private ResourceManager resMan;
	void Start () {
        alive = true;
        infected = false;
        shroomsIntersected = new List<Mushroom>();
        resMan = GameObject.Find("EventManager").GetComponent<ResourceManager>();
        
	}

    public void killTree()
    {
        alive = false;
        foreach (Mushroom shroom in shroomsIntersected)
        {
            shroom.deleteTree(this);
        }
        shroomsIntersected.Clear();
        resMan.delete_Tree(this);
    }

    //Prototyped dmg calculation. Suggestions?
    public void harmTree(float dmg)
    {
        currentHP -= dmg;
    }

    public void setIntegrity(float value)
    {
        currentIntegrity = value;
        if(currentIntegrity == maxIntegrity)
        {
            setSporesPerMin(0);
        }
    }

    public float getIntegrity()
    {
        return currentIntegrity;
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

    public float getSporesPerMin()
    {
        return sporesPerMin;
    }

    public void setSporesPerMin(float value)
    {
        sporesPerMin = value;
    }
}
