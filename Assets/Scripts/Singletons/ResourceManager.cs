﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Resource Manager", true)]
public class ResourceManager : Singleton<ResourceManager> {

	public static ResourceManager instance = null;

	//amount of spore the player has
	float spore = 0;
	// the amount of spores which a generated per second
	float spores_per_Second = 0;
	// the full amount of spores the player recieves in a minute
	float full_spore_amount = 0;
	// amount of spore one tree produces
	float spore_per_tree = 0;


	//array of trees, which are connected to the main Tree
	List<GameObject> connected_Trees;
	int tree_Amount;


	// Use this for initialization
	void Start () {

		connected_Trees = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//adds a new tree to the connected Tree array
	//doesnt do anything if the tree is already connected
	public void add_Tree(GameObject tree)
	{
		if (!connected_Trees.Contains (tree)) 
		{
			connected_Trees.Add (tree);
			tree_Amount++;
			//full_spore_amount += tree.GetComponent<ShroomTree> ().GetSporesPerMin ();
			spores_per_Second = full_spore_amount / 60.0f;
		} else 
		{
			Debug.Log("Tree already connected");
		}
		
	}

	//increases the amount of spore every second
	public void increase_Spore()
	{
		float new_spore = spore + spores_per_Second;
		EventManager.Instance.SendSporeChange (spore, new_spore);
	}

	//deletes the given tree if its connected to the network
	public void delete_Tree(GameObject tree)
	{
		if (connected_Trees.Contains (tree)) 
		{
			connected_Trees.Remove (tree);
			//full_spore_amount -= tree.GetComponent<ShroomTree>().GetSporesPerMin();
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
}
