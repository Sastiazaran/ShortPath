using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class graph  {
	public int [,] adjMat;
	public int [,] weigthMat;

	public List<NodeCode> graphDijkstra = new List<NodeCode>();
	
	public graph () {
		adjMat = new int[8, 8]{ 
			{0,1,1,1,0,0,0,0},
			{1,0,0,1,0,1,0,0},
			{1,0,0,1,1,0,0,0},
			{1,1,1,0,1,1,1,0},
			{0,0,1,1,0,0,1,1},
			{0,1,0,1,0,0,1,0},
			{0,0,0,1,1,1,0,1},
			{0,0,0,0,1,0,1,0} };
		weigthMat = new int[8, 8]{ 
			{0,8,4,7,0,0,0,0},
			{8,0,0,2,0,5,0,0},
			{4,0,0,4,7,0,0,0},
			{7,2,4,0,3,3,7,0},
			{0,0,7,3,0,0,3,1},
			{0,5,0,3,0,0,5,0},
			{0,0,0,7,3,5,0,1},
			{0,0,0,0,1,0,1,0} };
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	public void addNode(NodeCode nodev){
		this.graphDijkstra.Add (nodev);
	}
	public void removeNode(NodeCode nodev){
		this.graphDijkstra.Add (nodev);
	}

	public Vector3 posNode(int id)
	{Vector3 pos= Vector3.zero;
		foreach (NodeCode item in graphDijkstra) 
		{
			if (item.idNode == id)
				pos = item.position;
		}
		return pos;
	}

	public NodeCode finNode(int id)
	{NodeCode tmp;
		foreach (NodeCode item in graphDijkstra) 
		{
			if (item.idNode == id)
			{tmp = item;
				return tmp;}
		}
		return null;
	}

	public List<int> neighNode(int id)
	{ 
		int j;
		List <int> neigbor = new List<int>();
		if (adjMat != null)
		{
			j = id;   // - 1;
			for (int i=0; i<adjMat.GetLength(0); i++)
			 {
				if (adjMat[j,i]==1.0f)
				{neigbor.Add(i);
				}
				
			 }
		 }
		return neigbor;
	}
}
