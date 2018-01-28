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
		open_Nodes = new List<Node> ();
		closed_Nodes = new List<Node> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void check_connections()
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
			open_Nodes.Remove (current);
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

    public void remove_Node(Node n)
    {
        all_Nodes.Remove(n);
    }
}
