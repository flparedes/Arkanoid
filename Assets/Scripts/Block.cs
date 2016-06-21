using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public int scorePoints = 10;
    // public PowerUp powerUp

    public void DestroyBlock()
    {
        PlayerData.score += scorePoints;
        PlayerData.blockCount--;
        Destroy(this.gameObject);
    }
}
