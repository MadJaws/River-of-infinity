using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
   // private ScoreManager scoreManager;

    private void Update()
    {
        // Обновление текста счета на UI
        scoreText.text = "  " + ScoreManager.Instance.GetScore().ToString();
    }
}

