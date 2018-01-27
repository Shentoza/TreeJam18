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

    void addMushroom(Vector3 position)
    {
        mushrooms.Add(Instantiate(MushroomPrefab, position, 
            Quaternion.identity).GetComponent<Mushroom>());
    }

    void destroyMushroom(Mushroom m)
    {
        mushrooms.Remove(m);
        Destroy(m.transform);
    }
}
