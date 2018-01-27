using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour {

    public List<Mushroom> mushrooms;
    public GameObject MushroomPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addMushroom(Mushroom m)
    {
        mushrooms.Add(m);
    }

    public void destroyMushroom(Mushroom m)
    {
        mushrooms.Remove(m);
        Destroy(m.transform);
    }
}
