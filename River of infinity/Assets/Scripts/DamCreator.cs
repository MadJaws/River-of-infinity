using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamCreator : MonoBehaviour
{
    public float lastXRiver;
    public float lastYRiver;
    public GameObject prefabDam;
    public GameObject river;

    private bool flag = true;
    
    void Start()
    {
        river = GameObject.FindGameObjectWithTag("River");
    }

    
    void Update()
    {
        lastXRiver = river.GetComponent<RiverMapGenerator>().lastX;
        lastYRiver = river.GetComponent<RiverMapGenerator>().lastY;

        if (lastXRiver == 0 && lastYRiver == 0)
        {
            return;
        }
        else if (flag)
        {
            Instantiate(prefabDam, new Vector3(lastXRiver - 19.8f, lastYRiver - 15f, 0), Quaternion.identity);

            flag = false;
        }
        else
        {
            return;
        }
    }
}
