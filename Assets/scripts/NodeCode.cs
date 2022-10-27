using UnityEngine;
using System.Collections;
using System;

public class NodeCode : MonoBehaviour, IComparable
{ 
	public float totalCost;            
	public float estCost;             
	
	public bool bolObst;              
	public NodeCode parent;               
	public Vector3 position;           
	public int idNode;
	//public int numConections;
	
	
	public NodeCode()
	{   this.totalCost = 0.0f;
		this.estCost= 0.0f;
		this.bolObst = false;
		this.parent = null;
	}
	//******************************************************************
	
	public NodeCode(Vector3 pos)
	{
		this.estCost = 0.0f;
		this.totalCost = 0.0f;
		this.bolObst = false;
		this.parent = null;
		
		this.position = pos;
	}
	//*****************************************************************************
	
	public void IsObstacle()
	{
		this.bolObst = true;
	}
	
	//***********************************************************
	public int CompareTo(object obj)
	{
		NodeCode tmpnode = (NodeCode)obj;
		if (this.estCost < tmpnode.estCost)
			return -1;
		if (this.estCost > tmpnode.estCost)
			return 1;
		
		return 0;
	}
}

