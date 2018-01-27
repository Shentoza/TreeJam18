using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/MushroomManager", true)]
public class MushroomManager : Singleton<MushroomManager> { 

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
