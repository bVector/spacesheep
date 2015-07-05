using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Edge : MonoBehaviour {
	List<Block> blocks = new List<Block>();
	public Block[]Blocks{ get { return blocks.ToArray(); } }
	
	public bool AddBlock(Block e)
	{
		if(blocks.Contains (e))return false;
		blocks.Add (e);
		e.AddEdge (this);
		return true;
	}
	
	public bool RemoveBlock(Block e)
	{
		if (!blocks.Contains (e))
			return false;
		blocks.Remove (e);
		e.RemoveEdge (this);
		return true;
	}
	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
