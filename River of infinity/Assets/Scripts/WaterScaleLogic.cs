using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WaterScaleLogic : MonoBehaviour
{
    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    public Sprite scaleFillingSteps0;
    public Sprite scaleFillingSteps1;
    public Sprite scaleFillingSteps2;
    public Sprite scaleFillingSteps3;
    public Sprite scaleFillingSteps4;
    public Sprite scaleFillingSteps5;
    public Sprite scaleFillingSteps6;

    UnityEngine.UI.Image image;

    private int health;

    private void Start()
    {
        image = GetComponent < UnityEngine.UI.Image> ();
    }
    void Update()
    {
        dam = GameObject.FindWithTag("Dam");

        if (dam == null)
        {
            return;
        }
        else
        {
           health = dam.GetComponent<DamLogic>().currentHealth;

            if ((health >= 300) && (health <= 500))
            {
                image.sprite = scaleFillingSteps1;
            }
            else if ((health >= 500) && (health <= 800))
            {
                image.sprite = scaleFillingSteps2;
            }
            else if ((health >= 800) && (health <= 1200))
            {
                image.sprite = scaleFillingSteps3;
            }
            else if ((health >= 1200) && (health <= 2000))
            {
                image.sprite = scaleFillingSteps4;
            }
            else if ((health >= 2000) && (health <= 3000))
            {
                image.sprite = scaleFillingSteps5;
            }
            else if ((health >= 3000) && (health <= 5000))
            {
                image.sprite = scaleFillingSteps6;
            }
        }
    }
}
