using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveVel : MonoBehaviour {

    // general data
    public Vector3 vc_Velocity;
    public Vector3 vc_Heading;
    Vector3 vn_Velocity;
    public float s_rotSpeed=30.0f;

    public float s_MaxSpeed=5.0f;
    public float s_MinSpeed = 1.0f;

    private Vector3 newPosition;

    public GameObject TargetSeek;
    public GameObject TargetFlee;

    public float stamina;
  
    //*******************************
   
    public bool OnSeek = false;
    public bool OnFlee = false;
    public bool OnPursuit = false;
    public bool OnWander = false;
    public float s_panicDist;

    // Use this for initialization
    void Start () {
       // s_MaxSpeed = 8.0f;
        s_MinSpeed = 0.2f;
        s_panicDist = 5.0f;
        vn_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        vc_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        stamina = 100.0f;
    }
	
	// Update is called once per frame
	void Update () {


        stamina -= Time.deltaTime;
        if (stamina <= 0)
        {
            s_MaxSpeed = 0;
            s_MinSpeed = 0;

            gameObject.GetComponent<PlayerMovementTutorial>().moveSpeed = 0; 
        }

        vn_Velocity = Vector3.zero;

        if (OnSeek )
        { if (Vector3.Distance(TargetSeek.transform.position, transform.position) > 0.01f)
                vn_Velocity = vn_Velocity + Seek(TargetSeek.transform.position);
            else
                vn_Velocity = vc_Velocity * -1.0f;
        }

        if (OnFlee)
        {
            vn_Velocity = vn_Velocity + Flee(TargetFlee.transform.position);
        }
                
        //**********************************************************

        vc_Velocity += vn_Velocity;
        vc_Velocity = Vector3.ClampMagnitude(vc_Velocity, s_MaxSpeed);
        newPosition = transform.position + (vc_Velocity * Time.deltaTime);

        if(vc_Velocity.magnitude>s_MinSpeed)
             transform.position = newPosition;
        vc_Heading = vc_Velocity.normalized;

        //****************************************

        float angle = Vector3.SignedAngle(transform.forward, vc_Heading, Vector3.up);
        float rotAngle = 0.0f;
        if (angle > 0.0f)
            if (angle <= 45.0f)
                rotAngle = s_rotSpeed * Time.deltaTime;
            else
                rotAngle = angle-45.0f;
        if (angle < 0.0f)
            if (angle >= -45.0f)
                rotAngle = -s_rotSpeed * Time.deltaTime;
            else
                rotAngle = angle + 45.0f;
        //Debug.Log("rotAngle" + rotAngle);
        transform.Rotate(0.0f, rotAngle, 0.0f, Space.Self);
        //*****************************************




    }
    //******************************************************************

    public Vector3 Seek(Vector3 targetSeek)
    {
        Vector3 direction;
       
       direction =  targetSeek-transform.position;
        direction.y = 0;

        if (direction.magnitude < 0.001f )
        {
            
            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);


    }

    //******************************************************************
    public Vector3 Flee(Vector3 targetSeek)
    {
        Vector3 direction;

        direction =  transform.position-targetSeek;
        direction.y = 0;

        if (direction.magnitude>s_panicDist)
        {
           
            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);


    }

    //******************************************************************
   
    //*************************************************************************
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, vc_Heading * 10.0f + transform.position, Color.red);
        Debug.DrawLine(transform.position, transform.forward * 10.0f + transform.position, Color.green);
       
    }
}
