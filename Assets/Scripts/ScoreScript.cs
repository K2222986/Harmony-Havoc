using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;
    public int score;
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void IncrementScore(int increase)
    {
        score = score + increase;
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        ScoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
