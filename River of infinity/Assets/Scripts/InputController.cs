using UnityEngine;

public class InputController : MonoBehaviour
{
    public ClickHandler clickHandler;

    private void Update()
    {
        // ������������ ���� ����� ������� ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickHandler.HandleClick(clickPosition); // �������� ������ ����� � ClickHandler
        }
    }
}
