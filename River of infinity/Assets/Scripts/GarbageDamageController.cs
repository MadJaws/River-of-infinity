using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDamageController : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    public GameObject explosionPrefab;
    private bool hasExploded = false;

    [SerializeField] private AudioClip soundExplosion;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        // Проверка на смерть врага
        if (currentHealth <= 0)
        {
            if (!hasExploded)
            {
                GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                hasExploded = true;
                Destroy(explosionInstance, 1);
            }
            ScoreManager.Instance.AddScore(10);

            SoundManager.Instance.PlaySound(soundExplosion, 0.2f);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
