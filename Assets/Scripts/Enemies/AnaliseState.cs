using UnityEngine;
using System.Collections;

public class AnaliseState : IEnemyState {

    private Enemy enemy;
    private float currTime;

    public void enter(Enemy enemy) {
        this.enemy = enemy;
        currTime = 0;
    }

    public void execute() {
        currTime += Time.deltaTime;

        if (currTime >= enemy.getDecisionTime()) {
            int distanceToPlayer = Mathf.Abs(enemy.currStance - enemy.playerController.getCurrStance());
            if (distanceToPlayer == 1) {
                enemy.changeState(new AttackState());
                return;
            }

            moveEnemy();
            currTime = 0;
        }
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }

    private void moveEnemy() {
        if (enemy.currStance > enemy.playerController.getCurrStance()) {
            enemy.currStance--;
            enemy.transform.position = new Vector3(enemy.gameInstance.stancePositions[enemy.currStance].x, enemy.transform.position.y, enemy.transform.position.z);
        }
        else {
            enemy.currStance++;
            enemy.transform.position = new Vector3(enemy.gameInstance.stancePositions[enemy.currStance].x, enemy.transform.position.y, enemy.transform.position.z);
        }
    }
}
