using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        myText.text = "0";
        Reset();
    }

    public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}
