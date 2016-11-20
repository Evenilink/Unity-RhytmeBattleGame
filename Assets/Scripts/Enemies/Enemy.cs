using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private IEnemyState currentState;
    [System.NonSerialized] public GameInstance gameInstance;
    [System.NonSerialized] public PlayerController playerController;
    [System.NonSerialized] public int currStance;
    private float decisionTime = 1;
    private bool isUp;

    void Start () {
        gameInstance = GameObject.Find("GameInstance").GetComponent<GameInstance>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        changeState(new AnaliseState());
	}
	
	void Update () {
        currentState.execute();
	}

    public float getDecisionTime() {
        return decisionTime;
    }

    public int getCurrStance() {
        return currStance;
    }

    public bool getIsUp() {
        return isUp;
    }

    /**
     * Used by the BattleController.
     * Gives a more random approach to the initial condicions.
     * */
    public void setPositioningVariables(int currStance, bool isUp) {
        this.currStance = currStance;
        this.isUp = isUp;
    }

    /**
     * Calls the exit function on the current state and changes the current state to 'state'
     * */
    public void changeState(IEnemyState state) {
        if (currentState != null)
            currentState.exit();

        currentState = state;
        currentState.enter(this);
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

        transform.position = new Vector3(gameInstance.stancePositions[currStance].x, transform.position.y, transform.position.z);
    }

    /**
     * Called by the player.
     * Handles what happens when player attacks the enemy from above or below.
     * */
    public void receivedVerticalAttack() {
        float offset;

        if (isUp)
            offset = 0.5f;
        else
            offset = -0.5f;

        transform.RotateAround(new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), new Vector3(0, 0, 1), 90);
    }
}