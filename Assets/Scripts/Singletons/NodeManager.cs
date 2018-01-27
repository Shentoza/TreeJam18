using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Prefab("Prefabs/Singletons/Manager", true)]
public class NodeManager : Singleton<NodeManager>{

	[SerializeField]
	List<Node> all_Nodes;

	List<Node> open_Nodes;

	List<Node> closed_Nodes;

	public Node root;

	// Use this for initialization
	void Start () {
		EventManager.OnTreeInfectionComplete += check_connections;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void check_connections(ShroomTree tree)
	{
		foreach(Node n in all_Nodes)
		{
			n.IsConnected = false;
		}
		
		open_Nodes.Clear();
		//closed_Nodes.Clear();

		root.IsInOpen = true;
		open_Nodes.Add(root);

		while(open_Nodes.Count != 0)
		{
			Node current = open_Nodes[0];
			foreach(Node n in current.Neighbours)
			{
				if (!n.IsInOpen && !n.IsInClosed)
				{
					open_Nodes.Add(n);
					n.IsInOpen = true;
				}
			}
			current.IsInClosed = true;
			current.IsConnected = true;
		}

		foreach(Node n in all_Nodes)
		{
			if (!n.IsConnected)
				if (n.GetComponent<Mushroom>() != null)
				{
					n.GetComponent<Mushroom>().die();
				}
				else
				{
					ResourceManager.Instance.delete_Tree(n.GetComponent<ShroomTree>());
				}
		}
	}

	public void add_Nodes(Node new_Node)
	{
		all_Nodes.Add (new_Node);
	}

	void OnDestroy()
	{
		EventManager.OnTreeInfectionComplete -= check_connections;
	}
}
