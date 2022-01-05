using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("blue = " + collision.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("blue collision = " + other.gameObject.tag);

    }
}
