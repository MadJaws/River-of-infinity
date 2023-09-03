using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementObjectTowardsTarget : MonoBehaviour
{
    public float speed = 5f;


    //public GameObject garbage;
    public GameObject garbageObject;
   
    private GameObject _closestObject;
    public GameObject ClosestObject { get { return _closestObject; } }

    public float epsilon = 0.001f;

    public float rotationSpeed = 280f; // Ñêîðîñòü âðàùåíèÿ
   // private Quaternion targetRotation;

    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    private void Start()
    {
        garbageObject = this.gameObject;

        SetClosestObject(ClosestObject);

       // ChangeOfParent();
    }
    void Update()
    {
        dam = GameObject.FindWithTag("Dam");

        if (dam == null)
        {
            movementObjectTowardsTarget();
        }
        else if (dam != null)
        {
            damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;

            if(damdamCompletelyBuilt == true)
            {
                return;
            }
            else
            {
                movementObjectTowardsTarget();
            }
        }

        
    }
    public void movementObjectTowardsTarget()
    {
        if (AreVectorsAlmostEqual(transform.position, _closestObject.transform.position, epsilon))
        {
            // Debug.Log(transform.position+" i");
            // Debug.Log(_closestObject.transform.position+" point");
            // Debug.Log("ÌÎß ÄÎÁÐÀËÑß");
            // Debug.Log(AreVectorsAlmostEqual(transform.position,_closestObject.transform.position,epsilon));
            // garbageObject.AddComponent<MovingGarbageEnemy>();
             garbageObject.GetComponent<MovingGarbageEnemy>().SetClosestObject(_closestObject);
            //garbageObject.AddComponent<BoxCollider2D>();
            Destroy(this);
            return;
        }
        else
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            Vector3 destination = new Vector3(_closestObject.transform.position.x, _closestObject.transform.position.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        }
    }

    public void SetClosestObject(GameObject closestObject)
    {
        _closestObject = closestObject;
    }

    public void ChangeOfParent()
    {
       // garbage = GameObject.FindWithTag("Garbage");
      //  Transform newParentTransform = garbage.GetComponent<Transform>();

      // transform.SetParent(newParentTransform);
    }

    bool AreVectorsAlmostEqual(Vector3 v1, Vector3 v2, float epsilon)
    {
        return Vector3.Distance(v1, v2) < epsilon;
    }
}
