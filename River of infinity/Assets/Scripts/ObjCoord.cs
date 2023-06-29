using UnityEngine;

public class ObjCoord : MonoBehaviour
{
    public GameObject objName;
    public Transform objTransform;
    void Awake()
    {

    }
    public GameObject objNamee()
    {
        objName = GameObject.FindGameObjectWithTag("trash");
        objTransform = GetComponent<Transform>();
        Debug.Log(objName);
        return objName;
    }

    void Update()
    {

    }
}
