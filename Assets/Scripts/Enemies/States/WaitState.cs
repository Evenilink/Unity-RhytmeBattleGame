using UnityEngine;
using System.Collections;

public class WaitState : IEnemyState {
    private Enemy enemy;
    private float waitingTime;
    private float currTime = 0;

    public void enter(Enemy enemy) {
        this.enemy = enemy;
        waitingTime = Random.Range(2.0f, 4.0f);
    }

    public void execute() {
        currTime += Time.deltaTime;

        if (Mathf.Abs(enemy.currStance - enemy.playerController.getCurrStance()) <= 3) {
            if (Random.Range(0, 4) == 0)
                enemy.changeState(new RunAwayState());
            else
                enemy.changeState(new ChaseState());
        }

        if(currTime >= waitingTime) {
            if (Random.Range(0, 5) == 0)
                enemy.changeState(new RunAwayState());
            else
                enemy.changeState(new ChaseState());
        }
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }

    private void myDebug() {
        Debug.Log("WaitState");
    }
}
