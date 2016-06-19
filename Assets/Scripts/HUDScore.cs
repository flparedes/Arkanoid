﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScore : MonoBehaviour {

    private Text text;

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + PlayerData.score;
    }
}
