using UnityEngine;
using System.Collections;

public class moveGame : MonoBehaviour {
	public GameObject player1;
	moveVel player1v ;
	Dijkstra myFindPath;
	Vector3 target;
	Vector3 oldtarget;
	int cont=0;

	// Use this for initialization
	void Start () {
		player1v = player1.GetComponent<moveVel>();
		myFindPath = this.GetComponent<Dijkstra>();
		player1v.s_MaxSpeed = 10.0f;

	}
	
	// Update is called once per frame
	void Update () {

	if (myFindPath.pathfound.Count>0)
		{   player1v.OnSeek=true;
			Vector3 distance=player1v.gameObject.transform.position-
				myFindPath.pathfound[cont].gameObject.transform.position;
			Debug.Log(distance);
			if (distance.magnitude <0.5f)
			{
				cont++;
				if (cont==myFindPath.pathfound.Count)
					cont=0;
			}
			player1v.TargetSeek= myFindPath.pathfound[cont].gameObject;

		}

	}
}
