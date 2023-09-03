using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndButtonManager : MonoBehaviour
{
    public Button rewardButton;

    private void Start()
    {
        rewardButton.gameObject.SetActive(false); // Скрываем кнопку в начале
    }

    private void Update()
    {
        IncrementScore();
    }
    public void IncrementScore()
    {
        if (ScoreManager.Instance.GetScore() >= 100)
        {
            rewardButton.gameObject.SetActive(true); // Показываем кнопку при достижении 1000 очков
        }
        else
        {
            rewardButton.gameObject.SetActive(false);
        }
    }

    public void RewardButtonClicked()
    {
        // Выполните здесь действие, которое должно произойти при нажатии на кнопку
        ScoreManager.Instance.SubtractScore(100);
        FindAllObjectsLayer();
        Debug.Log("Button clicked! Reward action executed.");
    }

    public void FindAllObjectsLayer()
    {
        int damageAmount = 200;
        float radius = 1000;
        Vector2 direction = Vector2.up;
        float distance = 100;
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LayerMask singleLayerMask = 1 << LayerMask.NameToLayer("clickableLayer");
        RaycastHit2D[] colliders = Physics2D.CircleCastAll(clickPosition, radius, direction, distance, singleLayerMask);

        foreach (RaycastHit2D collider in colliders)
        {
            ClickableObject clickableObject = collider.collider.GetComponent<ClickableObject>();
            DamageController damageController = collider.collider.GetComponent<DamageController>();
            GarbageDamageController garbageDamageController = collider.collider.GetComponent<GarbageDamageController>();
            string tagObj = collider.collider.tag;
            if ((damageController != null) || (garbageDamageController != null))
            {
                // Debug.Log(tagObj);
                // objectMover = GetComponent<ObjectMover>();
                if (tagObj == "Garbage")
                {
                    garbageDamageController.TakeDamage(damageAmount);
                    
                    ScoreManager.Instance.AddScore(5);
                }
                else
                {
                    damageController.TakeDamage(damageAmount);

                    
                    ScoreManager.Instance.AddScore(10);
                }
            }
        }
    }
}
