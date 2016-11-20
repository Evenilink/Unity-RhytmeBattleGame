using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInstance : MonoBehaviour {

    public List<Vector2> stancePositions { get; set; }
    public List<bool> stanceUpOccupations { get; set; }
    public List<bool> stanceDownOccupations { get; set; }
    public int enemiesDefeated { get; set; }

    public void setStanceUpOccupations(int index, bool isUp) {
        stanceUpOccupations[index] = isUp;
    }

    public void setStanceDownOccupations(int index, bool isDown) {
        stanceDownOccupations[index] = isDown;
    }

    public void debug() {
        for (int i = 0; i < stancePositions.Count; i++)
            Debug.Log("Stance position " + i + " = " + stancePositions[i] + ", up = " + stanceUpOccupations[i] + ", down = " + stanceDownOccupations[i]);
    }
}
