using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer; // Слой, на котором находятся кликабельные объекты
    public float r;
    public Vector2 direction = Vector2.up;

    public void HandleClick(Vector3 clickPosition)
    {
        RaycastHit2D[] colliders = Physics2D.CircleCastAll(clickPosition,r,direction);
        
        if (colliders.Length > 0)
        {
            ClickableObject closestClickableObject = null;
            float closestDistance = Mathf.Infinity;

            foreach (RaycastHit2D collider in colliders)
            {
                ClickableObject clickableObject = collider.collider.GetComponent<ClickableObject>();
                if (clickableObject != null)
                {
                    float distanceToClickableObject = Vector2.Distance(clickPosition, collider.transform.position);
                    if (distanceToClickableObject < closestDistance)
                    {
                        closestClickableObject = clickableObject;
                        closestDistance = distanceToClickableObject;
                    }
                }
            }

            if (closestClickableObject != null)
            {

                closestClickableObject.OnClick();
            }
        }
        else
        {
            Debug.Log("ClickHandler - HandleClick() called. No clickable objects found at click position.");
        }
    }
}


