using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [System.NonSerialized]
    public GameInstance gameInstance;
    [System.NonSerialized]
    public PlayerController playerController;
    [System.NonSerialized]
    public int currStance;

    private IEnemyState currentState;
    private bool isUp = true;
    private int health = 1;
    private float difficulty = 3.0f;

    void Start() {
        gameInstance = GameObject.Find("GameInstance").GetComponent<GameInstance>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        changeState(new ChaseState());

        currStance = 2;
        isUp = true;
        updateEnemyPosition();
    }

    void Update() {
        currentState.execute();
    }

    public int getCurrStance() {
        return currStance;
    }

    public bool getIsUp() {
        return isUp;
    }

    public float getDifficulty() {
        return difficulty;
    }

    /**
     * Moves the enemy to the new 'x' position.
     * */
    public void updateEnemyPosition() {
        transform.position = new Vector3(gameInstance.stancePositions[currStance].x, transform.position.y, transform.position.z);
    }

    /**
     * Rotates around the floor where the stances are, creating the illusion the enemy is dropping or rising
     * */
    public void verticalFlip() {
        Vector3 rotateAround;
        isUp = !isUp;

        if (isUp)
            rotateAround = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        else
            rotateAround = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);

        transform.RotateAround(rotateAround, new Vector3(0, 0, 1), 180);
    }

    /**
     * Calls the exit function on the current state and changes the current state to 'state'
     * */
    public void changeState(IEnemyState state) {
        if (currentState != null)
            currentState.exit();

        currentState = state;
        currentState.enter(this);
        Debug.Log(state);
    }

    /**
     * Called by the player.
     * Handles what happens when player attacks the enemy from the sides.
     * */
    public void receivedHorizontalAttack(bool fromRight) {
        if (fromRight)
            currStance--;
        else
            currStance++;

        health--;

        if (health <= 0 || currStance < 0 || currStance >= gameInstance.stancePositions.Count) {
            Debug.Log("The enemy has died!");
            playerController.winner();
            Destroy(gameObject);
            return;
        }

        updateEnemyPosition();
    }

    /**
     * Called by the player.
     * Handles what happens when player attacks the enemy from above or below.
     * */
    public void receivedVerticalAttack(bool fromBelow) {
        health--;
        if (health <= 0) {
            Debug.Log("The enemy has died!");
            playerController.winner();
            Destroy(gameObject);
            return;
        }
    }
}