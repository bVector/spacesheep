﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {
	public int health = 100;
	List<Edge> edges = new List<Edge>();
	public Edge[]Edges{ get { return edges.ToArray(); } }
    public float Mass = 1;
    protected Rigidbody2D body;

	public bool AddEdge(Edge e)
	{
		if(edges.Contains (e))return false;
		edges.Add (e);
		return true;
	}
	
	public bool RemoveEdge(Edge e)
	{
		if(!edges.Contains (e))return false;
		edges.Remove (e);
		return true;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Awake()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }
}
