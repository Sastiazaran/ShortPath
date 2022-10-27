using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class moveGameAstar : MonoBehaviour {
	public GameObject player1;  
	public GameObject follow;   
    moveVel player1v;
    astar myFindPath;
	Vector3 target;
	int cont=0;
	List<NodeA> pathUse;

	// Use this for initialization
	void Start () {
        player1v = player1.GetComponent<moveVel>();
        myFindPath = this.GetComponent<astar>();
		player1v.s_MaxSpeed = 10.0f;
		pathUse = new List<NodeA> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myFindPath.pathfound.Count == 0) {
			return;
		} else {
            if (cont == 0)
            { pathUse = new List<NodeA>(myFindPath.pathfound);
                cont = 1;
            }
		}

		if (pathUse.Count > 0 && cont < pathUse.Count-1) {
       
            player1v.OnSeek = true;
		
            follow.transform.position = pathUse[cont].position;
            player1v.TargetSeek=follow;
            Vector3 distance = player1v.gameObject.transform.position - pathUse[cont].position;
            if (distance.magnitude < 1.0f) {
                cont++;
                if (cont == pathUse.Count) // llego
                {   cont=0;
                    myFindPath.pathfound.Clear();
                    pathUse.Clear();
                   player1v.OnSeek = false;
                }
                else
                {
                    follow.transform.position = pathUse[cont].position;
                }
            }
    	} 


	}
}
