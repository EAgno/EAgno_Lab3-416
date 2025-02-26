using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CollectCoin()
    {
        score += 1;
        UpdateScoreText();
        Debug.Log("Score: " + score);
    }


    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}