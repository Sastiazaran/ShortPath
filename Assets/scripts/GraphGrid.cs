using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GraphGrid : MonoBehaviour {
	public GameObject myPlane;  
	int numRows;
	int numCols;
	public float gridCellSize;
	public bool showGrid = true;
	public bool showObstacles = true;
	
	private Vector3 origin = new Vector3(0,0,0);
	private GameObject[] obstacleList;  
	public NodeA[,] nodesA ;  
	public float sizecell= 4.0f ; 

	//*********************************************************************
	//*********************************************************************
	void Awake()
	{
		Vector3 sizePlane = new Vector3(0,0,0);
		sizePlane = myPlane.GetComponent<Renderer> ().bounds.size; 
		numCols = (int)Mathf.Floor(sizePlane.x / sizecell);  
		numRows = (int)Mathf.Floor(sizePlane.z / sizecell);
		
		nodesA = new NodeA[numRows, numCols];   
		Vector3 sizeCenter;  
		sizeCenter= new Vector3(numCols/2.0f*sizecell,0,numRows/2.0f*sizecell); 
		origin = myPlane.transform.position - sizeCenter; 

		obstacleList = GameObject.FindGameObjectsWithTag("Obstacle"); 
		AsignObstacles(); 
	}
	//*********************************************************************
	
	void AsignObstacles()
	{
		int index = 0;
        
        for (int j = 0; j < numRows; j++)
        {
            for (int i = 0; i < numCols; i++)
            {   
				Vector3 cellPos = CellCenter(index);  
                NodeA nodeCell; 
                nodeCell = new NodeA(cellPos); 
                nodeCell.idNode=index;
                nodesA[j, i] = nodeCell;  
				index++;
			}
		}
		
		
		if (obstacleList != null && obstacleList.Length > 0)
		{
			foreach (GameObject obst in obstacleList)
			{
                float xo,zo,xf,zf;
                int incx, incz;
                Vector3 centerObs = obst.transform.position;
                Vector3 sizeObs= obst.GetComponent<Renderer>().bounds.size;
                xo = centerObs.x - sizeObs.x / 2;
                xf = centerObs.x + sizeObs.x / 2;
                zo = centerObs.z - sizeObs.z / 2;
                zf = centerObs.z + sizeObs.z / 2;
                if (xo < xf)
                    incx = 1;
                else incx = -1;
                if (zo < zf)
                    incz = 1;
                else incz = -1;
                for (int j = (int)zo; j < zf; j += incx)
                {
                    for (int i = (int)xo; i < xf; i += incz)
                    {
                        int indexCell = CellIndex(new Vector3(i,obst.transform.position.y,j));
                        int col = GetCol(indexCell); 
				     int row = GetRow(indexCell); 
				     
				     nodesA[row, col].MarkObs();
                    }
                }
                    

                      
			}
		}
	} 

	//*********************************************************************

	Vector3 CellCenter(int id)
	{ 
		int rowcell = (int)Math.Floor((decimal)(id / numCols));
		int colcell = id - (numCols * rowcell);

		float xpos = colcell * sizecell + (sizecell/2.0f);
		float ypos = rowcell *sizecell + (sizecell/2.0f);

		Vector3 posNode= new Vector3(xpos,0.0f,ypos);

		return (posNode+origin);
	}
	//*********************************************************************
	public int CellIndex(Vector3 pos)
	{ int idNode=0;
		Vector3 posinPlane = pos - origin;
		int rowcell = (int)Math.Floor(posinPlane.z / sizecell);
		int colcell = (int)Math.Floor(posinPlane.x / sizecell);
		idNode = rowcell * numCols + colcell;
		return idNode;
	}
	//*********************************************************************
	public int GetCol(int cell){
		int col= 0;
		int nrow = (int)Math.Floor(( double)(cell / numCols));
		col = cell - (nrow * numCols);
		return col;
	}
	//*********************************************************************
	public int GetRow(int cell){
		int row=0;
		row = (int)Math.Floor( (double)(cell / numCols));
		return row;
	}
	//*********************************************************************
	public NodeA getNode(int idnod){
		int col= 0;
		int nrow = (int)Math.Floor(( double)(idnod / numCols));
		col = idnod - (nrow * numCols);
		return (nodesA[nrow, col]);
	}
	//*********************************************************************

	public List<int> neighboors(int icell){
		List<int> negh = new List<int> ();
		int col = GetCol (icell);
		int row = GetRow (icell);
		for (int ll=-1; ll<2; ll++)
			for (int kk=-1; kk<2; kk++) {
			if (ll==kk && kk==0)
			  {  }
			else
			{  if ((ll+col)>0 && (ll+col)<numCols)
				if ((kk+row)>0 && (kk+row)<numRows)
				if (!nodesA[kk+row,ll+col].boolObst)
						negh.Add(nodesA[kk+row,ll+col].idNode);

			}
		}//fors
		return negh;
	
	}
	//*********************************************************************
	
	public float gCalculate (int idP, int idN)
	{int col1,row1, col2,row2;
		double cost;

		col1= GetCol(idP);
		row1= GetRow(idP);
		col2=GetCol(idN);
		row2=GetRow(idN);

		cost = Math.Pow (Math.Abs (col2 - col1)*sizecell, 2.0);
		cost = cost +Math.Pow (Math.Abs (row2 - row1)*sizecell, 2.0);
		cost = Math.Sqrt (cost);
		return ((float)cost);

	}
	//*********************************************************************
	
	public float hCalculate(int idnode, int idTarget ){
		int col1,row1, col2,row2;
		double cost;
		
		col1= GetCol(idnode);
		row1= GetRow(idnode);
		col2=GetCol(idTarget);
		row2=GetRow(idTarget);
		

		cost = Math.Abs (col2 - col1) * sizecell;
		cost = cost +Math.Abs (row2 - row1) * sizecell;
	
		return ((float)cost);
	}
	//*********************************************************************
	
	public void fCalculate(int idnode){

		int col1,row1;
		col1= GetCol(idnode);
		row1= GetRow(idnode);
		NodeA nodeAnalyzed = nodesA [row1, col1];

	}

}
