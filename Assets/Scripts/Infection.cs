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
		collider = GetComponent<SphereCollider>();
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
		++time;
		//collider.radius = Mathf.Log(1 + time);
		collider.radius += 1f;
	}

	void OnTriggerEnter(Collider c)
	{
		if (!(c.gameObject.layer == LayerMask.NameToLayer ("Floor"))) {			
			ShroomTree tree = c.gameObject.GetComponent<ShroomTree> ();
		
			Debug.Log (c.gameObject.name);
			if (tree != null) {
				Debug.Log ("1");
				if (tree.isInfected ())
					return;
				Debug.Log ("2");
				Ray ray = new Ray (transform.position, tree.transform.position - transform.position);
				RaycastHit hit;
				Physics.Raycast (ray, out hit);
				Debug.DrawLine (ray.origin, hit.point);

				GameObject other = hit.collider.gameObject;

				Debug.Log (other.name);
	
				if (other.GetComponent<ShroomTree> () != null) {
					Debug.Log ("SPREADING");
					OpalmaCareSystem.Instance.addInfectedTree (tree);
					//NEIN NEIN nicht mehr hier! das ist zu schnell!
					//tree.gameObject.AddComponent<Infection> ();
				}
			}
		}
	}
}
