using UnityEngine;
using System.Collections;
using System;

public class RunAwayState : IEnemyState {
    private Enemy enemy;
    private float decisionTime;
    private float currTime = 0;
    private int destinationStance;

    public void enter(Enemy enemy) {
        this.enemy = enemy;

        /*var numRightStances = enemy.currStance - enemy.gameInstance.stancePositions.Count;
        var numLeftStances = enemy.currStance + 1;*/

        if (UnityEngine.Random.Range(0, 1) == 0) {
            destinationStance = UnityEngine.Random.Range(enemy.currStance - 3, enemy.currStance);
            if (destinationStance < 0)
                destinationStance = 0;
        }
        else {
            destinationStance = UnityEngine.Random.Range(enemy.currStance + 1, enemy.gameInstance.stancePositions.Count);

            if (destinationStance > enemy.gameInstance.stancePositions.Count - 1)
                destinationStance = enemy.gameInstance.stancePositions.Count - 1;
        }

        decisionTime = UnityEngine.Random.Range(0.2f, 0.3f);
    }

    public void execute() {
        currTime += Time.deltaTime;

        if(currTime >= decisionTime) {
            currTime = 0;
            moveAwayFromPlayer();

            if (enemy.currStance == destinationStance) {
                if (UnityEngine.Random.Range(0, 2) == 0)
                    enemy.changeState(new ChaseState());
                else
                    enemy.changeState(new WaitState());
            }

            decisionTime = UnityEngine.Random.Range(0.2f, 0.3f);
            myDebug();
        }
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }

    private void moveAwayFromPlayer() {
        if (destinationStance > enemy.currStance)
            enemy.currStance++;
        else
            enemy.currStance--;

        enemy.updateEnemyPosition();
    }

    private void myDebug() {
        Debug.Log("RunAwayState, " + decisionTime);
    }
}
