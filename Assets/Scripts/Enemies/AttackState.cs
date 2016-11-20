using UnityEngine;
using System.Collections;
using System;

public class AttackState : IEnemyState {

    private Enemy enemy;
    private float currTime;

    public void enter(Enemy enemy) {
        this.enemy = enemy;
        currTime = 0;
    }

    public void execute() {
        if(enemy.playerController.getIsUp() == enemy.getIsUp()) {
            Debug.Log("I'm attacking and the isUp is the same!");
        } else {
            Debug.Log("I'm attacking but on a different isUp!");
        }
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }
}
