using UnityEngine;
using System.Collections;
using System;

public class Node : MonoBehaviour, IComparable
{ 
	public float totalCost;           //Total cost 
	public float estCost;             //Estimated cost 

	public bool bolObst;              //Does the node is an obstacle or not
	public Node parent;                //Parent of the node in the linked list
	public Vector3 position;           //Position of the node
	public int idNode;


	public Node()
	{   this.totalCost = 0.0f;
		this.estCost= 0.0f;
		this.bolObst = false;
		this.parent = null;
	}
	//******************************************************************

	public Node(Vector3 pos)
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
		Node node = (Node)obj;
		if (this.estCost < node.estCost)
			return -1;
		if (this.estCost > node.estCost)
			return 1;
		
		return 0;
	}
}
