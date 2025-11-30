using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;

    int score = 0;
    public int nextTriggerScore = 50;   // milestone
    public int milestoneStep = 50;      // milestone increment

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint()
    {
        score += 1;
        UpdateScoreUI();

        if (score >= nextTriggerScore)
        {
            // Heal the player
            Player.instance.Heal(50);

            // Set next milestone
            nextTriggerScore += milestoneStep;
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString() + " POINT";
    }
}