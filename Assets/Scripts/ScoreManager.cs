using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    private int score;

    private void Awake()
    {
        EnemyEvents.scoreAdvancedEvent.AddListener(AddScoreEventFunction);
    }
    private void AddScoreEventFunction(int addedScore)
    {
        AddScore(addedScore);
    }

    void Start()
    {
        score = 0;
        SetText();
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        SetText();
    }

    void SetText()
    {
        textScore.text = "Points: " + score;
    }
}
