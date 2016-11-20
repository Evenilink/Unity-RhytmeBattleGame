using UnityEngine;
using System.Collections;

public interface IEnemyState {
    void enter(Enemy enemy);
    void execute();
    void exit();
    void OnTriggerEnter2D(Collider2D other);
}
