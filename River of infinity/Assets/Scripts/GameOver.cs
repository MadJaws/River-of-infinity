using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject dam;
    private int healthDam;

    public int speed;
    public GameObject targetPoint;
    private Vector3 targetCoordinates;


    private void Start()
    {
       // dam = GameObject.FindWithTag("Dam");
       // healthDam = dam.GetComponent<DamLogic>().currentHealth;

        targetCoordinates = targetPoint.transform.position;
    }

    private void Update()
    {
        dam = GameObject.FindWithTag("Dam");
        if (dam != null)
        {
            healthDam = dam.GetComponent<DamLogic>().currentHealth;

            startEndFameScene();
        } 
    }

    public void startEndFameScene()
    {
        if(healthDam > 5000)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCoordinates, Time.deltaTime * speed);
        }
    }
}
