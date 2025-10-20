using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public ParticleSystem hitEffect;

    private int totalScore = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        BallInfo ball = other.GetComponent<BallInfo>();

        if (ball == null)
        {
            return;
        }

        Debug.Log("TINTA A FOST LOVITA de un obiect aruncabil!");

        float distance = Vector3.Distance(ball.ThrowPosition, transform.position);
        int scoreForThisThrow = Mathf.RoundToInt(distance * 10);
        totalScore += scoreForThisThrow;

        UpdateScoreUI();

        if (hitEffect != null)
        {
            hitEffect.transform.position = other.transform.position;
            hitEffect.Play();
        }

        Destroy(other.gameObject);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Scor: " + totalScore;
        }
    }
}