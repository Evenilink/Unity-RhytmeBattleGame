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
        Debug.Log("I'm attacking!");
    }

    public void exit() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

    }
}
