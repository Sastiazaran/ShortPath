using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//**********************************************************************************
public class astar : MonoBehaviour {
	
	
	List<NodeA> visited = new List<NodeA>();
	List<NodeA> unvisited= new List<NodeA>();
	
	public List<NodeA> pathfound= new List<NodeA>();
	
	Vector3 oldTargetPos;
	Vector3 targetPos;

	public GameObject startPoint;
	public GameObject targetPoint;

	NodeA startNode;
	NodeA targetNode;

	NodeA  currentNode;
	GraphGrid myGraph;
	
	int cont=0;
	
	
	//**********************************************************
	// Use this for initialization
	void Start () {

		myGraph = this.gameObject.GetComponent<GraphGrid>();
		int indexStar = myGraph.CellIndex (startPoint.transform.position);
		int row = myGraph.GetRow (indexStar);
		int col = myGraph.GetCol (indexStar);
		currentNode=myGraph.nodesA[row,col];
		startNode = myGraph.nodesA [row, col];

		int indexTar = myGraph.CellIndex (targetPoint.transform.position);
		row = myGraph.GetRow (indexTar);
		col = myGraph.GetCol (indexTar);
		targetNode=myGraph.nodesA[row,col];

		targetPos = targetPoint.transform.position;
		oldTargetPos = targetPos;

		}

	//*********************************************************************
	// Update is called once per frame
	void Update () {


		targetPos = targetPoint.transform.position;
		
		if (oldTargetPos != targetPos || cont==0) {

            if (pathfound.Count>0)
            {  Vector3 distance = targetPoint.transform.position - startPoint.transform.position;
                if (distance.magnitude < 0.5f)
                { pathfound.Clear();
                cont = 0;
                }
            }
            else {  
                int indexTar = myGraph.CellIndex(targetPoint.transform.position);
                int row = myGraph.GetRow(indexTar);
                int col = myGraph.GetCol(indexTar);
                targetNode = myGraph.nodesA[row, col];

				
                int indexStar = myGraph.CellIndex(startPoint.transform.position);
                row = myGraph.GetRow(indexStar);
                col = myGraph.GetCol(indexStar);
                currentNode = myGraph.nodesA[row, col];
                startNode = myGraph.nodesA[row, col];

				
                pathfound = findPathAstar(startNode, targetNode);
                cont++;
            }
			oldTargetPos=targetPos;
		}

		}
	//***********************************************************************

	public List<NodeA> findPathAstar(NodeA startNode, NodeA targetNode)
	{
		List<NodeA> mypath = new List<NodeA>();
		int idcurrent;
		NodeA newNode;
		
		while (currentNode.idNode != targetNode.idNode) {
			idcurrent = currentNode.idNode;

		
			List<int> neigCurrent = myGraph.neighboors(idcurrent);

	
			foreach (int idn in neigCurrent) 
			{int col = myGraph.GetCol(idn);
				int row = myGraph.GetRow(idn);
                if (!isVisited(idn)) {
                    if (!isUnvisited(idn) )  // no ha sido analizado antes
                    {
                        newNode = myGraph.nodesA[row, col];
                        newNode.GCost = currentNode.GCost+ myGraph.gCalculate(idcurrent, idn); //calcula costos
                        newNode.parent = currentNode;
                        newNode.HCost = myGraph.hCalculate(idn, targetNode.idNode);
                        newNode.FCost = newNode.GCost + newNode.HCost;
                        unvisited.Add(newNode);
                     }
                    else 
                    {
                        recalculateCost(currentNode, idn);
                    }

                }
				
			}
			unvisited.Sort(); 
			currentNode = unvisited [0];
			visited.Add (currentNode);
			unvisited.RemoveAt (0);
			int col2 = myGraph.GetCol(currentNode.idNode);
			int row2 = myGraph.GetRow(currentNode.idNode);
           

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
	
	public void recalculateCost(NodeA current,int idneig )
	{float costEst = 0.0f;
		float costOld = 0.0f;
		foreach (NodeA item in unvisited) 
		{
			if (item.idNode==idneig)  
			{int n0=current.idNode;
			int n1=idneig;
             int col = myGraph.GetCol(n1);
             int row = myGraph.GetRow(n1);

                costEst =current.GCost + myGraph.gCalculate(n0,n1);
				costOld=item.GCost;
				if (costOld> costEst)
				{
                    item.GCost=costEst;
				    item.parent=current;
                    item.FCost = item.GCost + item.HCost;

                }
			}
		}
		unvisited.Sort ();
		return;
	}
	
	//******************************************************
	public bool isUnvisited(int id)
	{ foreach (NodeA item in unvisited)
		if (item.idNode == id)
			return true;
		return false;
	}
	//*********************************************************************
	public bool isVisited(int id)
	{ foreach (NodeA item in visited)
		if (item.idNode == id)
			return true;
		return false;
	}
	//	}
	
	//**************************************************************+ 
	void OnDrawGizmos(){

		if (myGraph!=null)
		if (myGraph.nodesA!=null)

			for (int j=0; j<myGraph.nodesA.GetLength(0)-1; j++)
					for (int i=0; i<myGraph.nodesA.GetLength(1)-1; i++) 
			            {
			            Debug.DrawLine (myGraph.nodesA[j,i].position,myGraph.nodesA[j,i+1].position);
			            Debug.DrawLine (myGraph.nodesA[j,i].position,myGraph.nodesA[j+1,i].position);
                        if (myGraph.nodesA[j, i].boolObst)
                        {
                            Gizmos.DrawSphere(myGraph.nodesA[j, i].position,1.0f);
                        }
             
			            }

	}
	
    //************************************************************************
	public void walk(List<NodeA> pathfound)
	{
		foreach (NodeA item in pathfound)
			Debug.Log ("Node = "+item.idNode);
		
	}
}
