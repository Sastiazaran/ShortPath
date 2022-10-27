using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningGenerator : MonoBehaviour
{

    public float generationRangeMinusX = -100.0f;
    public float generationRangePlusX = 100.0f;
    public float generationRangeMinusZ = -100.0f;
    public float generationRangePlusZ = 100.0f;
    public GameObject Lightning;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++) {
            float positionGenerationX = Random.Range(generationRangeMinusX, generationRangePlusX);
            float positionGenerationZ = Random.Range(generationRangeMinusZ, generationRangePlusZ);
            Vector3 randomPosition = new Vector3(positionGenerationX,0,positionGenerationZ);
            Instantiate(Lightning, randomPosition, Lightning.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<moveVel>().stamina += 30;
            
        }
        

        
    }
}
