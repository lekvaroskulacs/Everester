using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private float defaultScorePerHit;

    private TMP_Text text;
    private float score = 0;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = score.ToString();
    }
    public void IncreaseScore()
    {
        IncreaseScore(defaultScorePerHit);
    }

    public void IncreaseScore(float score)
    {
        this.score += score;
        text.text = this.score.ToString();
    }
}
