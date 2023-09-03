using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRiverTransformPosition : MonoBehaviour
{
   
    void Start()
    {
        transform.position = new Vector3(transform.position.x - 21.8f, transform.position.y - 11, 0);
    }

    
}
