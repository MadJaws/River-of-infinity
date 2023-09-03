using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer; // Слой, на котором находятся кликабельные объекты
    public float radius;             // Радиус обнаружения 
    public Vector2 direction = Vector2.up;
    public float distance;
    public int damageAmount;
    // public ObjectCreator objectCreator;
    public ObjectMover objectMover;
    public List<GameObject> objectsThatCollided = new List<GameObject>();
    // public List<GameObject> objectsCreatorList = new List<GameObject>();
    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    private bool disableClicks = false;

    private void Start()
    {

        // objectMover = GetComponent<ObjectMover>();
    }

    private void Update()
    {
        dam = GameObject.FindWithTag("Dam");

        if (dam != null)
        {
            damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;

            if (damdamCompletelyBuilt)
            {
                disableClicks = true;
            }
        }
    }
    public void HandleClick(Vector3 clickPosition)
    {

        if (!damdamCompletelyBuilt)
        {
            LayerMask singleLayerMask = 1 << LayerMask.NameToLayer("clickableLayer");
            RaycastHit2D[] colliders = Physics2D.CircleCastAll(clickPosition, radius, direction, distance, singleLayerMask);
            // Debug.Log(colliders.Length);
            if (colliders.Length > 0)
            {
                string tagObj = null;
                DamageController damageController1 = null;
                GarbageDamageController garbageDamageController1 = null;
                ClickableObject closestClickableObject = null;
                DamLogic damLogic1 = null;
                float closestDistance = Mathf.Infinity;

                foreach (RaycastHit2D collider in colliders)
                {
                    ClickableObject clickableObject = collider.collider.GetComponent<ClickableObject>();
                    DamageController damageController = collider.collider.GetComponent<DamageController>();
                    GarbageDamageController garbageDamageController = collider.collider.GetComponent<GarbageDamageController>();
                    DamLogic damLogic = collider.collider.GetComponent<DamLogic>();
                    if (clickableObject != null)
                    {
                        float distanceToClickableObject = Vector2.Distance(clickPosition, collider.transform.position);
                        if (distanceToClickableObject < closestDistance)
                        {
                            garbageDamageController1 = garbageDamageController;
                            damageController1 = damageController;
                            closestClickableObject = clickableObject;
                            closestDistance = distanceToClickableObject;
                            tagObj = collider.collider.tag;
                            damLogic1 = damLogic;
                        }
                    }
                }
                if (closestClickableObject != null)
                {
                    if ((damageController1 != null) || (garbageDamageController1 != null) || (damLogic1 != null))
                    {
                        // Debug.Log(tagObj);
                        // objectMover = GetComponent<ObjectMover>();
                        if (tagObj == "Garbage")
                        {
                            garbageDamageController1.TakeDamage(damageAmount);
                            closestClickableObject.OnClick();

                        }
                        else if ((tagObj == "MovableObject") || (tagObj == "StopMovableObject") || (tagObj == "ObjectTouchedRiver"))
                        {
                            damageController1.TakeDamage(damageAmount);

                            closestClickableObject.OnClick();

                        }
                        else
                        {
                            damLogic1.TakeDamage(damageAmount);
                        }
                    }
                }
            }
        }
        else if (disableClicks)
        {
            return;
        }
    }
}
//}


