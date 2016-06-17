using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScore : MonoBehaviour {

    public static int score;

    private Text text;

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + score;
    }
}
