using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	private bool isConnected;

	private List<Node> neighbours;

	bool isInOpen = false, isInClosed = false;

    public bool IsInOpen{
        get{return isInOpen;}
        set{isInOpen = value;}
    }

    public bool IsInClosed{
        get{return isInClosed;}
        set{isInClosed = value;}
    }

    public List<Node> Neighbours{
        get{return neighbours;}
        set{ neighbours = value;}
    }

    public bool IsConnected{
        get{return isConnected;}
		set{isConnected = value;}
    }

	public void add_Node(Node n)
	{
		neighbours.Add (n);
	}

	public void remove_Node(Node n)
	{
		neighbours.Remove (n);
	}

	public void initalizeNeighbour()
	{
		neighbours = new List<Node> ();
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
