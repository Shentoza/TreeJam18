using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class ResourceManager : Singleton<ResourceManager> {

    //amount of spore the player has
    [SerializeField]
	private float spore = 50.0f;
	// the amount of spores which a generated per second
	private float spores_per_Second = 0;
	// the full amount of spores the player recieves in a minute
	private float spore_per_minute = 0;
    // amount of spore one tree produces
    private float spore_per_tree = 30.0f,
        spore_gain_default = 50.0f;


	//array of trees, which are connected to the main Tree
	List<ShroomTree> connected_Trees;
	int tree_Amount;


	// Use this for initialization
	void Start () {
        EventManager.OnSecondPassed += increase_Spore;
        EventManager.OnTreeInfectionComplete += delete_Tree;
		connected_Trees = new List<ShroomTree> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//adds a new tree to the connected Tree array
	//doesnt do anything if the tree is already connected
	public void add_Tree(ShroomTree tree)
	{
		if (!connected_Trees.Contains (tree)) 
		{
			connected_Trees.Add (tree);
            tree.transform.GetChild(0).gameObject.SetActive(true);
            EventManager.Instance.SendTreeCountChange(++tree_Amount);
            calculateSporeGain();
			NodeManager.Instance.add_Nodes (tree);
		}
		
	}

	//increases the amount of spore every second
	public void increase_Spore()
	{
		float new_spore = spore + spores_per_Second;
		EventManager.Instance.SendSporeChange (spore, new_spore);
        spore = new_spore;
	}

	//deletes the given tree if its connected to the network
	public void delete_Tree(ShroomTree tree)
	{
		if (connected_Trees.Contains (tree)) 
		{
			connected_Trees.Remove (tree);
            tree.transform.GetChild(0).gameObject.SetActive(false);
            EventManager.Instance.SendTreeCountChange(--tree_Amount);
            calculateSporeGain();
            NodeManager.Instance.remove_Node(tree);
		}
	}

	public bool reduce_spore(float amount)
	{
		if (spore - amount >= 0) 
		{
            float oldSpore = spore;
			spore -= amount;
            EventManager.Instance.SendSporeChange(oldSpore, spore);
			return true;
		} else 
		{
			Debug.Log("Not enough spores!");
			return false;
		}
	}

    public bool hasTree(ShroomTree tree)
    {
        return connected_Trees.Contains(tree);
    }

    private void calculateSporeGain()
    {
        spore_per_minute = tree_Amount * spore_per_tree + spore_gain_default;
        spores_per_Second = spore_per_minute / 60.0f;
    }
}
