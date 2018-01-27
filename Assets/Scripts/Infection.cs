using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

	private SphereCollider collider;

	[SerializeField]
	float startRadius;
	[SerializeField]
	float maxRadius;

	float time;


	// Use this for initialization
	void Start () {
		collider = new SphereCollider();
		collider.radius = 0;
		time = 0;
		EventManager.OnSecondPassed += spread;
	}

	void Destroy()
	{
		EventManager.OnSecondPassed -= spread;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spread(){
		collider.radius = Mathf.Log(1 + time);
	}

	void OnTriggerEnter(Collider c)
	{
		ShroomTree tree = c.gameObject.GetComponent<ShroomTree>();
		if (tree.isInfected())
			return;
		if (tree != null)
		{
			Ray ray = new Ray(transform.position, tree.transform.position - transform.position);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);

			GameObject other = hit.collider.gameObject;
			if (other == tree.gameObject)
			{
				OpalmaCareSystem.Instance.addInfectedTree(tree);
				tree.gameObject.AddComponent<Infection>();
			}
		}
	}
}
