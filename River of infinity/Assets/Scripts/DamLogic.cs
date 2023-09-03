using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamLogic : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;

    public bool damCompletelyBuilt = false;

    public GameObject restartScene;

    private SpriteRenderer sr;

    [SerializeField] private Sprite spriteDam0;
    [SerializeField] private Sprite spriteDam1;
    [SerializeField] private Sprite spriteDam2;
    [SerializeField] private Sprite spriteDam3;
    [SerializeField] private Sprite spriteDam4;
    [SerializeField] private Sprite spriteDam5;

    private float canUp = 1;
    private float canDown = 0;

    private Vector3 startPos;
    private Vector3 upPos;

    private void Start()
    {
        currentHealth = maxHealth;

        restartScene = GameObject.FindWithTag("RestartButton");

        sr = GetComponent<SpriteRenderer>();

        startPos = gameObject.transform.position;

        upPos = startPos + new Vector3(0, 0.3f, 0);
    }

    private void Update()
    {
        if ((currentHealth >= 300) && (currentHealth <= 500))
        {
            sr.sprite = spriteDam0;

            transform.position = startPos;
        }
        else if ((currentHealth >= 500) && (currentHealth <= 800))
        {
            sr.sprite = spriteDam1;

            transform.position = startPos;
        }
        else if ((currentHealth >= 800) && (currentHealth <= 1200))
        {
            sr.sprite = spriteDam2;

            transform.position = startPos;
        }
        else if ((currentHealth >= 1200) && (currentHealth <= 2000))
        {
            sr.sprite = spriteDam3;

            transform.position = startPos;
        }
        else if ((currentHealth >= 2000) && (currentHealth <= 3000))
        {
            sr.sprite = spriteDam4;

            transform.position = upPos;
        }
        else if ((currentHealth >= 3000) && (currentHealth <= 5000))
        {
            sr.sprite = spriteDam5;

            transform.position = upPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Garbage") 
        {
            collision.gameObject.GetComponent<GarbageDamageController>().Die();

           // currentHealth += 20;
           // amountAbsorbedGarbage++;
            currentHealth += collision.gameObject.GetComponent<GarbageDamageController>().currentHealth;

            damGrowth();
        }
    }

    public void damGrowth()
    {
        if ((currentHealth >= 300) && (currentHealth <= 500))
        {
            currentHealth += 20;

            transform.position = startPos;
        } 
        else if ((currentHealth >= 500) && (currentHealth <= 800))
        {
            currentHealth += 50;

            sr.sprite = spriteDam1;

            transform.position = startPos;
        }
        else if ((currentHealth >= 800) && (currentHealth <= 1200))
        {
            currentHealth += 80;

            sr.sprite = spriteDam2;

            transform.position = startPos;
        }
        else if ((currentHealth >= 1200) && (currentHealth <= 2000))
        {
            currentHealth += 100;

            sr.sprite = spriteDam3;

            transform.position = startPos;
        }
        else if ((currentHealth >= 2000) && (currentHealth <= 3000))
        {
            currentHealth += 200;

            sr.sprite = spriteDam4;

            transform.position = upPos;
        }
        else if ((currentHealth >= 3000) && (currentHealth <= 5000))
        {
            currentHealth += 300;

            sr.sprite = spriteDam5;

            transform.position = upPos;
        }
        else if (currentHealth > 5000)
        {
            damCompletelyBuilt = true;
            
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        // Проверка на смерть врага
        if (currentHealth <= 0)
        {
            ScoreManager.Instance.AddScore(1000);
            Die();
        }
    }

    public void Die()
    {
        

        gameObject.SetActive(false);
        // Destroy(gameObject);
        ScoreManager.Instance.SaveScore();

        restartScene.GetComponent<RestartScene>().RestartCurrentScene();
    }
}
