using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//**********************************************************************************
public class Dijkstra : MonoBehaviour {


	List<NodeCode> visited = new List<NodeCode>();
	List<NodeCode> unvisited= new List<NodeCode>();

	public List<NodeCode> pathfound= new List<NodeCode>();

	Vector3 oldTargetPos;
	Vector3 targetPos;

	public NodeCode startNode;
	public NodeCode targetNode;
	NodeCode  currentNode;
	graph myGraph = new graph();

	int cont=0;
	bool calculando = false;


	//**********************************************************
	// Use this for initialization
	void Start () {

		GameObject nodesScenario;

		nodesScenario = GameObject.Find ("nodeSet");

		NodeCode[] nodes = nodesScenario.GetComponentsInChildren <NodeCode>();
		NodeCode tmpnode;

		
		for (int i = 0; i < nodes.Length; i++)
		{
			nodes[i].idNode = i;
		}

			for (int i=0; i<nodes.Length;i++) {
				tmpnode=nodes[i];
			if (tmpnode.idNode==startNode.idNode)
				{tmpnode.estCost=0.0f;
				currentNode=tmpnode;}
			else 
				tmpnode.estCost = float.MaxValue;
			tmpnode.position=tmpnode.gameObject.transform.position;  // asign position
			myGraph.addNode(tmpnode);  // add to the graph // order list
			
		}

		oldTargetPos = targetPos;
		targetPos = targetNode.gameObject.transform.position;
		

		//printEdges ();

	}
	
	// Update is called once per frame
	void Update () {

		targetPos = targetNode.gameObject.transform.position;

		if (oldTargetPos != targetPos || cont==0) {
			if (!calculando)
            {
			pathfound.Clear();
			calculando = true;
			pathfound = findPathD(startNode, targetNode);
				calculando = false;
				cont++;
			oldTargetPos=targetPos;
            }
	
		}
	}
	//***********************************************************************
	
	public List<NodeCode> findPathD(NodeCode startNode, NodeCode targetNode)
	{
		List<NodeCode> mypath = new List<NodeCode>();
		int idcurrent;
		NodeCode newNode;
		currentNode = startNode;
		currentNode.estCost = 0;
		visited.Add(currentNode);

		while (currentNode.idNode != targetNode.idNode) {
			idcurrent = currentNode.idNode;
            List<int> lista = myGraph.neighNode(idcurrent);
            List<int> neigCurrent = lista;
			foreach (int idn in neigCurrent) 
				if (!isUnvisited (idn) && !isVisited (idn)) {
					newNode = myGraph.finNode (idn);
					unvisited.Add (newNode);
					unvisited.Sort ();
				}
			calculateCost (currentNode, neigCurrent);
		    currentNode = unvisited [0];
			visited.Add (currentNode);
			unvisited.RemoveAt (0);

		}

		newNode = currentNode;
		while (newNode.idNode!=startNode.idNode)
		{
			mypath.Add(newNode);
			newNode=newNode.parent;

		}
		mypath.Add (newNode);
		mypath.Reverse ();
		return mypath;
	}
	//*********************************************************

	public void calculateCost(NodeCode current,List<int> neig )
	{float costEst = 0.0f;
	float costOld = 0.0f;
		foreach (int id in neig)
			foreach (NodeCode item in unvisited) 
			{
			if (item.idNode==id)   
			{int n0=current.idNode;
				int n1=id;			
				costEst=current.estCost + myGraph.weigthMat[ (n0), (id)];
				costOld=item.estCost;
				if (costOld> costEst)
				{item.estCost=costEst;
					item.parent=current;
				}
			   }
			}
		unvisited.Sort ();
	return;
	}

	//******************************************************
	public bool isUnvisited(int id)
	{ foreach (NodeCode item in unvisited)
			if (item.idNode == id)
				return true;
		return false;
	}

	public bool isVisited(int id)
	{ foreach (NodeCode item in visited)
		if (item.idNode == id)
			return true;
		return false;
	}
//	}

	//**************************************************************+ 
	void OnDrawGizmos(){
     if (myGraph.adjMat!=null)
		for (int j=0; j<myGraph.adjMat.GetLength(1); j++)
			for (int i=0; i<=j; i++) {
					if (myGraph.adjMat[j,i]==1.0f)
				{
					Debug.DrawLine (myGraph.posNode(j),myGraph.posNode(i));
				
				}
			
			}

	}

	public void walk(List<NodeCode> pathfound)
	{
		foreach (NodeCode item in pathfound)
			Debug.Log ("Node = "+item.idNode);

	}
}
