using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ObjCoord : MonoBehaviour
{
    public GameObject objN;
    public Transform objTransform;
    void Awake()
    {

    }
    public GameObject objName()
    { 
        objN = GameObject.FindGameObjectWithTag("trash");
        objTransform = GetComponent<Transform>();
        //Debug.Log(objName);
        return objN;
    }

    void Update()
    {
        
    }
}
