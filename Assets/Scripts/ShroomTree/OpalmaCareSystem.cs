using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/OpalmaCareSystem", true)]
public class OpalmaCareSystem : Singleton<OpalmaCareSystem> {

    [SerializeField]
    GameObject InfectionMarkerPrefab;

    //List with all infected trees
	[SerializeField]
	List<ShroomTree> infectedTrees = new List<ShroomTree>();

    //List with all infected trees which get dmg over time
	[SerializeField]
	List<ShroomTree> dyingTrees= new List<ShroomTree>();

    //Pick value
    [SerializeField]
    private float dmg;

	// Use this for initialization


	void Start () {
        EventManager.OnSecondPassed += handleDmg;
        EventManager.OnSecondPassed += checkInfections;
	}

    public void OnDestroy()
    {
        EventManager.OnSecondPassed -= handleDmg;
        EventManager.OnSecondPassed -= checkInfections;
    }


    public void addInfectedTree(ShroomTree tree)
    {
        tree.setInfection(true);
        infectedTrees.Add(tree);
        Instantiate(InfectionMarkerPrefab, tree.transform.position + new Vector3(0,15,0), Quaternion.identity);
    }

    //Fires "tree is dying" event
    public void addDyingTree(ShroomTree tree)
    {
        dyingTrees.Add(tree);
		tree.setSporesPerMin (0.0f);
        EventManager.Instance.SendTreeInfectionComplete(tree);
    }

    public void handleDmg()
    {
	 	foreach (ShroomTree tree in dyingTrees)
	    {   
			if (tree.alive) {
				if (tree.getHP () - dmg > .0f) {
					tree.dealDamage (dmg);
				} else {
					tree.alive = false;
					tree.killTree ();
				}
			}
	   	}
    }


    //Check every second if integrity of infected tree is max
    //if so tree reference is written into dyingTree list
    public void checkInfections()
    {
        foreach(ShroomTree tree in infectedTrees)
        {
            tree.incrIntegrity();
			if(tree.getIntegrity() >= tree.getMaxIntegrity() && !tree.dies)
            {
				tree.dies = true;
				addDyingTree(tree);
				tree.gameObject.AddComponent<Infection> ();
            }
        }
    }









}
