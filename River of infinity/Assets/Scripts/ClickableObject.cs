using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    // �����, ������� ����� ���������� ��� ����� �� �������
    public void OnClick()
    {
        Debug.Log("ClickableObject - OnClick() called for object: " + gameObject.name);
        // ����� ����� ��������� ������ �������� ��� ����� �� �������
    }
}


