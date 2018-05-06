using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private Text scoreText;
    public static int score = 0;

	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    //Score player gets in up right corner
    public void ScoreToGive(int scorePoints)
    {
        score += scorePoints;
        scoreText.text = score.ToString();
    }

    public static void ResetScore()
    {
        score = 0;

    }
}
