using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {

    private Enemy enemy;
    private float decisionTime;
    private float currTime = 0;

    public void enter(Enemy enemy) {
        this.enemy = enemy;
        decisionTime = Random.Range(0.2f * enemy.getDifficulty(), 0.4f * enemy.getDifficulty());
    }

    public void execute() {
        currTime += Time.deltaTime;

        if (currTime >= decisionTime) {
            int distanceToPlayer = Mathf.Abs(enemy.getCurrStance() - enemy.playerController.getCurrStance());

            if (distanceToPlayer == 0) {
                if (enemy.getIsUp())
                    enemy.changeState(new AttackState(4));
                else
                    enemy.changeState(new AttackState(3));
            }
            else if (distanceToPlayer == 1) {
                if (enemy.getIsUp() == enemy.playerController.getIsUp()) {
                    int decider = Random.Range(0, 2);
                    if (decider == 0) {
                        if (enemy.currStance > enemy.playerController.getCurrStance())
                            enemy.changeState(new AttackState(1));
                        else
                            enemy.changeState(new AttackState(2));
                    }
                    else
                        enemy.verticalFlip();
                }
                else {
                    int decider = Random.Range(0, 2);
                    if (decider == 0)
                        enemy.verticalFlip();
                    else
                        moveCloserToPlayer(enemy.playerController);
                }
            }
            else {
                if (Random.Range(0, 9) == 0)
                    enemy.changeState(new WaitState());
                else
                    moveCloserToPlayer(enemy.playerController);
            }

            currTime = 0;
            decisionTime = Random.Range(0.2f * enemy.getDifficulty(), 0.4f * enemy.getDifficulty());
            myDebug();
        }
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }

    private void moveCloserToPlayer(PlayerController player) {
        if (enemy.currStance > player.getCurrStance())
            enemy.currStance--;
        else
            enemy.currStance++;

        enemy.updateEnemyPosition();
    }

    private void myDebug() {
        Debug.Log("ChaseState, " + decisionTime);
    }
}