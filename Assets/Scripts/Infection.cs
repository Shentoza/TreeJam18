using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

	private SphereCollider collider;

	[SerializeField]
	float startRadius;
	[SerializeField]
	float maxRadius;
	float time = 0;

	[SerializeField]
	private float timeToSpread = 1;
	ShroomTree tree;

	// Use this for initialization
	void Start () {
		collider = GetComponent<SphereCollider>();
		collider.radius = 0;
		time = 0;
		tree = gameObject.GetComponent<ShroomTree> ();
	}
		
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > timeToSpread) {
			spread();
			time = 0;
		}
	}

	void spread(){
		if (tree.alive) {
			collider.radius += 2f;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.layer != LayerMask.NameToLayer ("Floor")) {			
			ShroomTree tree = c.gameObject.GetComponent<ShroomTree> ();
			if (tree != null) {
				if (tree.isInfected ())
					return;
				Ray ray = new Ray (transform.position, tree.transform.position - transform.position);
				RaycastHit hit;
				Physics.Raycast (ray, out hit);
				Debug.DrawLine (ray.origin, hit.point);
				Debug.DrawRay (ray.origin, tree.transform.position - transform.position);

				GameObject other = hit.collider.gameObject;

				if (other.GetComponent<ShroomTree> () != null) {
					OpalmaCareSystem.Instance.addInfectedTree (tree);
					//NEIN NEIN nicht mehr hier! das ist zu schnell!
					//tree.gameObject.AddComponent<Infection> ();
				}
			}
		}
	}
}
