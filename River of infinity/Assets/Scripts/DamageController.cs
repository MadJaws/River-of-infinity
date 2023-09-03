using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public List<GameObject> objectsThatCollided = new List<GameObject>();

    // public List<GameObject> objectsCreatorList = new List<GameObject>();

    public ObjectMover objectMover;

    public GameObject explosionPrefab;
    private bool hasExploded = false;

    [SerializeField] private AudioClip soundExplosion;

    private void Start()
    {
        currentHealth = maxHealth;

        objectMover = GetComponent<ObjectMover>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        // ѕроверка на смерть врага
        if (currentHealth <= 0)
        {
            objectsThatCollided = objectMover.objectsThatCollided;

            ScoreManager.Instance.AddScore(20);

            SoundManager.Instance.PlaySound(soundExplosion, 0.2f);
            Die();
        }
    }

    public void Die()
    {
        // ƒополнительна€ логика при смерти врага (например, анимаци€, звук и т.д.)
        Moving();

        for(int i = 0; i < objectsThatCollided.Count; i++)
        {
            objectsThatCollided[i].GetComponent<ObjectMover>().objectsThatCollided.RemoveAll(x => x == gameObject);
        }

        GameObject objectCreator = GameObject.FindGameObjectWithTag("ObjectCreator");
        //  objectsCreatorList = objectCreator.GetComponent<ObjectCreator>().objectsCreatorList;

        //  for(int i = 0;i < objectsCreatorList.Count; i++)
        //  {
        //     if (objectsCreatorList[i] ==  gameObject)
        //     {
        //         objectsCreatorList.RemoveAt(i);
        //    }
        //  }
        if (!hasExploded)
        {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            hasExploded = true;
            Destroy(explosionInstance, 1);
        }

        Destroy(gameObject);
    }

    public void Moving()
    {
        for(int i = 0; i < objectsThatCollided.Count; i++)
        {
            ObjectMover mover = objectsThatCollided[i].GetComponent<ObjectMover>();
            if (objectsThatCollided[i] == null)
            {
                objectsThatCollided.RemoveAt(i);
            }
            else if (mover == null)
            {
                objectsThatCollided.RemoveAt(i);
            }
            else
            {
                mover.gameObject.tag = "MovableObject";
                if (mover != null)
                {
                    mover.isMoving = true;
                }
            }
            
        }
    }

    // private IEnumerator StartTimeout()
    // {
    //  yield return new WaitForSeconds(0f);

    //    Debug.Log("мувинг вызвалс€");
    // for (int i = 0; i < objectsThatCollided1.Count; i++)
    //{

    //   Debug.Log("зашел в цикл");
    //  ObjectMover mover = objectsThatCollided1[i].GetComponent<ObjectMover>();
    // if (mover != null)
    //  {
    //   mover.isMoving = true;
    //     Debug.Log("и даже по идее помен€л на тру");
    // }

    //}



    //}
}
