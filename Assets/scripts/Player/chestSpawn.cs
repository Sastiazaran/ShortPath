using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestSpawn : MonoBehaviour
{
    public float generationRangeMinusX = -100.0f;
    public float generationRangePlusX = 100.0f;
    public float generationRangeMinusZ = -100.0f;
    public float generationRangePlusZ = 100.0f;

    [SerializeField] private Transform chest;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
            float positionGenerationX = Random.Range(generationRangeMinusX, generationRangePlusX);
            float positionGenerationZ = Random.Range(generationRangeMinusZ, generationRangePlusZ);
            chest.transform.position = new Vector3(positionGenerationX,0,positionGenerationZ);        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
