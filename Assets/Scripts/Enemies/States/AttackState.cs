using UnityEngine;
using System.Collections;
using System;

public class AttackState : IEnemyState {

    private Enemy enemy;
    private float currTime;
    private int attackFrom;

    /*
     * 1 --> Left Attack
     * 2 --> Right Attack
     * 3 --> Up Attack
     * 4 --> Down Attack
     * */

    public AttackState(int attackFrom) {
        this.attackFrom = attackFrom;
    }

    public void enter(Enemy enemy) {
        this.enemy = enemy;
        currTime = 0;
    }

    public void execute() {
        switch(attackFrom) {
            case 1: enemy.playerController.receivedHorizontalAttack(true); break;
            case 2: enemy.playerController.receivedHorizontalAttack(false); break;
            case 3: enemy.playerController.receivedVerticalAttack(true); break;
            case 4: enemy.playerController.receivedVerticalAttack(false); break;
            default: break;
        }

        int decider = UnityEngine.Random.Range(0, 2);
        switch(decider) {
            case 0: enemy.changeState(new ChaseState()); break;
            case 1: enemy.changeState(new RunAwayState()); break;
            default: break;
        }
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }
}
