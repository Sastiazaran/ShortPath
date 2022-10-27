using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEntrances : MonoBehaviour
{

    [SerializeField] private Transform playerSpawn;
    public Rigidbody rb;
    public GameObject[] entrance;

    // Start is called before the first frame update
    void Start()
    {
        int m = Random.Range(0, entrance.Length);
        playerSpawn.transform.position = entrance[m].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
