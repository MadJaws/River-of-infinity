using UnityEngine;
using System.Collections;
using UnityEditor.U2D.Animation;

public class ClickCoord : MonoBehaviour
{

    private Vector3 ClickPos;
    private Vector3 ObjPos;
    private float sideALength, sideBLength;
    public Transform target;
    public Camera cam;

    private void Awake()
    {
        //ObjPos = new Vector2[100];
        cam = GetComponent<Camera>();
        //Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //ClickPos = Input.mousePosition;
            Debug.Log(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
            ClickPos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

            //print(ClickPos);
            ObjPos = target.position;
            //print(ObjPos);

            if (target == null) return;
            //������� ���������� �� ����
            Vector3 v = target.position - ClickPos;
            //�������� ������� ���������� �� ��� X
            float sqrX = v.x * v.x;
            //������� ������� ���������� �� ��� Y
            float sqrY = v.y * v.y;
            //��������� ���������� �� ����, �������� ���������� ������ �� ����� ��������� ���������� �� ���� X � Z
            float distance = Mathf.Sqrt(sqrX + sqrY);
            Debug.Log(distance);
            
        }
    }
}