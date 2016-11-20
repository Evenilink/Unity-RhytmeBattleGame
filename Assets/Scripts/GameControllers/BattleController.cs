using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

    public GameObject enemy;
    public GameObject battleStance;
    public int amountBattleStances = 4;
    public float stancesOffset = 2;

    /**
     * Called by the PlayerController when he enter a trigger.
     * Generates battle stances and returns their position as a list of vector2.
     * */
    public void generateBattleStances() {
        GameInstance gameInstance = GameObject.Find("GameInstance").GetComponent<GameInstance>();
        List<Vector2> stancePositions = new List<Vector2>();
        GameObject obj;

        if (amountBattleStances < 4) {
            Debug.LogError("Variable amoutBattleStances can't be less than 4! Setting default value of 4.");
            amountBattleStances = 4;
        }

        //If it's an odd number, add one to the center of the battle zone
        if (amountBattleStances % 2 != 0) {
            obj = (GameObject)Instantiate(battleStance, transform.position, transform.rotation);
            stancePositions.Add(new Vector2(transform.position.x, transform.position.y));
            amountBattleStances--;
        }

        int aux = 1;
        for (int i = 0; i < amountBattleStances; i++) {
            Vector3 battleStancePosition;

            if (i % 2 == 0)
                battleStancePosition = new Vector3(transform.position.x - (stancesOffset * aux), transform.position.y, transform.position.z);
            else {
                battleStancePosition = new Vector3(transform.position.x + (stancesOffset * aux), transform.position.y, transform.position.z);
                aux++;
            }

            obj = (GameObject)Instantiate(battleStance, battleStancePosition, transform.rotation);
            stancePositions.Add(battleStancePosition);
        }

        stancePositions.Sort(sortByX);

        //Setting the GameInstance variables
        gameInstance.stancePositions = stancePositions;

        GameObject enemyGO = Instantiate(enemy, new Vector3(stancePositions[0].x, 0.8f, 0), transform.rotation) as GameObject;
        //enemyGO.GetComponent<Enemy>().setPositioningVariables(stancePositions.Count - 1, true);
    }

    /**
     * The sorting method
     * The list is sorted from the smaller 'x' to the bigger 'x'
     * */
    static int sortByX(Vector2 position1, Vector2 position2) {
        return position1.x.CompareTo(position2.x);
    }
}