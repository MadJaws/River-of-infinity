using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    // Метод, который будет вызываться при клике на объекте
    public void OnClick()
    {
        Debug.Log("ClickableObject - OnClick() called for object: " + gameObject.name);
        // Здесь можно выполнить нужные действия при клике на объекте
    }
}


