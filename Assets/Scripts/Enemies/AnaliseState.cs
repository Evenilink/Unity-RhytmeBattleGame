using UnityEngine;
using System.Collections;

public class AnaliseState : IEnemyState {

    private Enemy enemy;
    private float currTime;
    private int randomness = 1;

    public void enter(Enemy enemy) {
        this.enemy = enemy;
        currTime = 0;
    }

    public void execute() {
        currTime += Time.deltaTime;

        if (currTime >= enemy.getDecisionTime()) {
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
                    } else
                        enemy.verticalFlip();
                } else {
                    int decider = Random.Range(0, 2);
                    if (decider == 0)
                        enemy.verticalFlip();
                    else
                        moveCloserToPlayer(enemy.playerController);
                }
            } else
                moveCloserToPlayer(enemy.playerController);

            currTime = 0;
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
}