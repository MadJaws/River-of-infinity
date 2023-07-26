using UnityEngine;

public class InputController : MonoBehaviour
{
    public ClickHandler clickHandler;

    private void Update()
    {
        // Обрабатываем клик левой кнопкой мыши
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickHandler.HandleClick(clickPosition); // Передаем вектор клика в ClickHandler
        }
    }
}
