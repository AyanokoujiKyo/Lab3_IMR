using UnityEngine;
using TMPro;

public class BasketballHoop : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public ParticleSystem scoreEffect;

    [Header("Setări Scor")]
    public float baseDistanceForOnePoint = 16.0f;

    private int totalScore = 0;
    private bool ballPassedTopTrigger = false;
    private ThrowableObject currentBall;

    void Start()
    {
        UpdateScoreUI();
    }

    public void BallEnteredTop(ThrowableObject ball)
    {
        ballPassedTopTrigger = true;
        currentBall = ball;
    }

    public void BallEnteredBottom(ThrowableObject ball)
    {
        if (ballPassedTopTrigger && ball == currentBall)
        {
            float distance = Vector3.Distance(transform.position, ball.throwPosition);

            int points = 0;
            if (distance >= baseDistanceForOnePoint)
            {
                points = Mathf.FloorToInt(distance) - Mathf.FloorToInt(baseDistanceForOnePoint) + 1;
            }

            totalScore += points;

            Debug.Log("COȘ! Ai primit " + points + " puncte.");

            if (scoreEffect != null)
            {
                scoreEffect.transform.position = transform.position;
                scoreEffect.Play();
            }

            Destroy(ball.gameObject);
            GameManager.Instance.SpawnNewBall();
            UpdateScoreUI();
            ResetHoopState();
        }
    }

    public void BallExitedTop()
    {
        ResetHoopState();
    }

    private void ResetHoopState()
    {
        ballPassedTopTrigger = false;
        currentBall = null;
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Scor: " + totalScore;
        }
    }
}