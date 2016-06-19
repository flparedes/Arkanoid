using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDLives : MonoBehaviour {
    
    public float activeAlpha = 1f;
    public float inactiveAlpha = 0.5f;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    private Color activeColor;
    private Color inactiveColor;

    void Awake()
    {
        activeColor = heart1.color;
        inactiveColor = activeColor;

        activeColor.a = activeAlpha;
        inactiveColor.a = inactiveAlpha;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerData.lives >= 3)
        {
            heart1.color = activeColor;
            heart2.color = activeColor;
            heart3.color = activeColor;
        }
        else if (PlayerData.lives == 2)
        {
            heart1.color = activeColor;
            heart2.color = activeColor;
            heart3.color = inactiveColor;
        }
        else if (PlayerData.lives == 1)
        {
            heart1.color = activeColor;
            heart2.color = inactiveColor;
            heart3.color = inactiveColor;
        }
        else
        {
            heart1.color = inactiveColor;
            heart2.color = inactiveColor;
            heart3.color = inactiveColor;
        }
	}
}
