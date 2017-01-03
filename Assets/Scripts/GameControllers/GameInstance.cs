using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInstance : MonoBehaviour {
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public List<Vector2> stancePositions { get; set; }
    public int enemiesDefeated { get; set; }

    public void debug() {
        for (int i = 0; i < stancePositions.Count; i++)
            Debug.Log("Stance position " + i + " = " + stancePositions[i]);
    }

    public void clearStancePositions() {
        stancePositions.Clear();
    }

    public void addEnemyDefeated() {
        enemiesDefeated++;
    }
}
