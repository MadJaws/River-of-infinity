using UnityEngine;

public class ClickCoord : MonoBehaviour
{

    private Vector3 ClickPos;
    private Vector3 ObjPos;
    public Camera cam;
    private static RaycastHit2D hit;
    private Transform target, lastTarget;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        //Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
                 target = GameObject.FindGameObjectWithTag("trash").GetComponent<Transform>();
                //Debug.Log(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
                ClickPos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                //ObjPos = target.position;


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
                

            
            //CircleCast(ClickPos, 5 , target.position);

            


        }
    }
    public static RaycastHit2D CircleCast(Vector3 origin, float radius, Vector3 direction, int layerMask = 10, float distance = Mathf.Infinity, float minDepth = Mathf.Infinity, float maxDepth = Mathf.Infinity)
    {
        Debug.Log(origin);
        Debug.Log(direction);
        Debug.Log(radius);
        return hit;
    }

}