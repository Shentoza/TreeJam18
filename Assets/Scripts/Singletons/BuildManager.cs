 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class BuildManager : Singleton<BuildManager> {

	[SerializeField]
	GameObject[] mushroomPrefabs;

	private SphereCollider previewCollider;

    private int layer_mask;  

	private bool inBuildMode = true;

	private bool canBuildHere;

	private int colliderNum;

	private int selectedShroom = 0;

	// Use this for initialization
	void Start () {
		layer_mask = LayerMask.GetMask("Floor");
		previewCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(inBuildMode)
		{
			Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);      // von wo der Raycast starten soll
			RaycastHit hit;
			if (Physics.Raycast(myRay, out hit, Mathf.Infinity, layer_mask))                            // wenn der ray was trifft werden die infos des treffers in hit gespeichert
			{
				previewCollider.transform.position = hit.point;
				//Debug.Log(hit.point);

				if (colliderNum > 0 && Input.GetMouseButtonDown(0))                            // what to do if i press the left mouse button
				{
					Build(selectedShroom ,hit.point);
						//always spawn on height of the floor + half height of the object
					//Debug.Log(hit.point);                                   // debugs the vector3 of the position where I clicked
				}
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		++colliderNum;
		Debug.Log(colliderNum);
	}

	void OnTriggerExit(Collider c)
	{
		--colliderNum;
		Debug.Log(colliderNum);
	}

	void Build (int index, Vector3 pos)
    {
        GameObject m = mushroomPrefabs[index];
		int cost = m.GetComponent<Mushroom>().Cost;
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
