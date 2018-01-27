using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/OpalmaCareSystem", true)]
public class OpalmaCareSystem : Singleton<OpalmaCareSystem> {


    //List with all infected trees
    List<ShroomTree> infectedTrees;

    //List with all infected trees which get dmg over time
    List<ShroomTree> dyingTrees;

    //Pick value
    [SerializeField]
    private float dmg;

	// Use this for initialization
	void Start () {
        infectedTrees = new List<ShroomTree>();
        dyingTrees = new List<ShroomTree>();
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
    }

    //Fires "tree is dying" event
    public void addDyingTree(ShroomTree tree)
    {
            dyingTrees.Add(tree);
            EventManager.Instance.SendTreeInfectionComplete(tree);
    }

    public void handleDmg()
    {
        foreach (ShroomTree tree in dyingTrees)
        {   if (tree.getHP()-dmg > .0f)
                tree.dealDamage(dmg);
            else
                tree.killTree();
        }
    }


    //Check every second if integrity of infected tree is max
    //if so tree reference is written into dyingTree list
    public void checkInfections()
    {
        foreach(ShroomTree tree in infectedTrees)
        {
            tree.incrIntegrity();
            if(tree.getIntegrity() >= tree.getMaxIntegrity())
            {
                addDyingTree(tree);
            }
        }
    }









}
