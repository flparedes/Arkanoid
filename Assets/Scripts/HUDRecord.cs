using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDRecord : MonoBehaviour
{

    private Text text;

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();
    }


    void Update()
    {
        // Set the displayed text to be the word "Record" followed by the scoreRecord value.
        text.text = "Record: " + PlayerData.scoreRecord;
    }
}
