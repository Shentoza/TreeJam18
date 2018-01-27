using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class ResourceManager : Singleton<ResourceManager> {

	//amount of spore the player has
	private float spore = 0;
	// the amount of spores which a generated per second
	private float spores_per_Second = 0;
	// the full amount of spores the player recieves in a minute
	private float full_spore_amount = 0;
	// amount of spore one tree produces
	private float spore_per_tree = 0;


	//array of trees, which are connected to the main Tree
	List<ShroomTree> connected_Trees;
	int tree_Amount;


	// Use this for initialization
	void Start () {

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
			tree_Amount++;
			//full_spore_amount += tree.GetSporesPerMin ();
			spores_per_Second = full_spore_amount / 60.0f;
			NodeManager.Instance.add_Nodes (tree);
		}
		
	}

	//increases the amount of spore every second
	public void increase_Spore()
	{
		float new_spore = spore + spores_per_Second;
		EventManager.Instance.SendSporeChange (spore, new_spore);
	}

	//deletes the given tree if its connected to the network
	public void delete_Tree(ShroomTree tree)
	{
		if (connected_Trees.Contains (tree)) 
		{
			connected_Trees.Remove (tree);
			//full_spore_amount -= tree.GetSporesPerMin();
			spores_per_Second = full_spore_amount / 60.0f;
		}
	}

	public bool reduce_spore(float amount)
	{
		if (spore - amount >= 0) 
		{
			spore -= amount;
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
}
