using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public int scorePoints = 10;
    // public PowerUp powerUp

    public void DestroyBlock()
    {
        HUDScore.score += scorePoints;
        Destroy(this.gameObject);
    }
}
