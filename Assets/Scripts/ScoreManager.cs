using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    private TMP_Text scoreBoard;

    private void Awake()
    {
        scoreBoard = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreBoard.SetText("Score: " + score.ToString());
    }
}
