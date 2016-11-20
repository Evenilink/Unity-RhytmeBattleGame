using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInstance : MonoBehaviour {

    public List<Vector2> stancePositions { get; set; }
    public int enemiesDefeated { get; set; }

    public void debug() {
        for (int i = 0; i < stancePositions.Count; i++)
            Debug.Log("Stance position " + i + " = " + stancePositions[i]);
    }

    /*public List<Vector2> getStancePositions() {
        return stancePositions;
    }

    public int getEnemiesDefeated() {
        return enemiesDefeated;
    }*/
}
