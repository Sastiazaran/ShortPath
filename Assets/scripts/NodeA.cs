using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NodeA : IComparable {
	public int idNode;
	public float FCost;           //Total cost 
	public float estCost;             //Estimated cost
	public float GCost;
	public float HCost;
	
	public bool boolObst;              //Does the node is an obstacle or not

	public NodeA parent;                //Parent of the node in the linked list
	public Vector3 position;           //Position of the node

	//public int numConections;
	
	
	public NodeA()
	{   this.FCost = 0.0f;
		this.GCost = 0.0f;
		this.HCost = 0.0f;
		this.estCost= 0.0f;
		this.boolObst = false;
		this.parent = null;
	}
	//******************************************************************
	
	public NodeA(Vector3 pos)
	{
		this.FCost = 0.0f;
		this.GCost = 0.0f;
		this.HCost = 0.0f;
		this.estCost= 0.0f;
		this.boolObst = false;
		this.parent = null;
		
		this.position = pos;
	}
	//*****************************************************************************
	public void MarkObs()
	{ boolObst = true;
	}

    public void SetPosition(Vector3 pos)
    {
        this.position = pos;

    }
	//***********************************************************
	public int CompareTo(object obj)
	{
		NodeA tmpnode = (NodeA)obj;
		if (this.FCost < tmpnode.FCost)
			return -1;
		if (this.FCost > tmpnode.FCost)
			return 1;
		
		return 0;
	}



}
