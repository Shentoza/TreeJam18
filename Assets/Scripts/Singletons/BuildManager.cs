 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager> {

	GameObject[] mushroomPrefabs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Build (int index, Vector3 pos)
    {
        Mushroom m = mushroomPrefabs[index].GetComponent<Mushroom>();
		int cost = m.Cost;
        if (ResourceManager.Instance.reduce_spore(cost)){
			Instantiate(m, pos, Quaternion.identity);		//besser hier glaube ich
			//MushroomManager.addMushroom(pos);
			return;
		}
		else{
			//Fehlersound
		}
		
    }
}
