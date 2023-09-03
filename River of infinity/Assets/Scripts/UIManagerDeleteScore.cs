using UnityEngine;

public class UIManagerDeleteScore : MonoBehaviour
{
   // public ScoreManager scoreManager;

    public void OnButtonClick()
    {
        ScoreManager.Instance.DeleteScore();
    }
}
