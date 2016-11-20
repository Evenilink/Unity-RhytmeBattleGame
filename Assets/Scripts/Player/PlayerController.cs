using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    private enum State {
        FREE_MOVEMENT_STATE,
        BATTLE_STATE
    };
    private State currState = State.FREE_MOVEMENT_STATE;

    //FREE_MOVEMENT_STATE variables
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float movementSpeed = 10f;
    public float jumpForce = 400f;
    private float axisMovement;
    private float groundRadius = 0.2f;
    private bool grounded;

    //BATTLE_STATE variables
    private GameInstance gameInstance;
    private Enemy enemy;
    private int health = 1;
    private int currStance;
    private bool isUp;
    private int startingStance = 3;
    private bool startingIsUp = true;

    private Rigidbody2D rigidbody2D;

    void Start() {
        Random.seed = System.Environment.TickCount;

        rigidbody2D = GetComponent<Rigidbody2D>();
        gameInstance = GameObject.Find("GameInstance").GetComponent<GameInstance>();
    }

    void Update() {
        if (currState == State.FREE_MOVEMENT_STATE)
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        handleInput();
    }

    /**
     * Handles the movement input functionalities
     * */
    void handleInput() {
        switch(currState) {

            case State.FREE_MOVEMENT_STATE:
                axisMovement = Input.GetAxis("Horizontal");
                rigidbody2D.velocity = new Vector2(axisMovement * movementSpeed, rigidbody2D.velocity.y);
                if (Input.GetButtonDown("Jump") && grounded)
                    rigidbody2D.AddForce(new Vector2(0, jumpForce));
                break;

            case State.BATTLE_STATE:
                if (Input.GetButtonDown("Transport Left")) {
                    int numAdvanceStance;
                    if (enemy.getCurrStance() == (currStance - 1) && enemy.getIsUp() == isUp)
                        numAdvanceStance = 2;
                    else
                        numAdvanceStance = 1;

                    if (currStance - numAdvanceStance < 0)
                        Debug.Log("You want to be dead?");
                    else
                        updateStancePosition(false, numAdvanceStance);
                }

                if (Input.GetButtonDown("Transport Right")) {
                    int numAdvanceStance;
                    if (enemy.getCurrStance() == (currStance + 1) && enemy.getIsUp() == isUp)
                        numAdvanceStance = 2;
                    else
                        numAdvanceStance = 1;

                    if (currStance + numAdvanceStance >= gameInstance.stancePositions.Count)
                        Debug.Log("You want to be dead?");
                    else
                        updateStancePosition(true, numAdvanceStance);
                }

                if (Input.GetButtonDown("Transport Up") && !isUp) {
                    if (enemy.getCurrStance() != currStance)
                        verticalFlip();
                }

                if (Input.GetButtonDown("Transport Down") && isUp) {
                    if (enemy.getCurrStance() != currStance)
                        verticalFlip();
                }

                if (Input.GetButtonDown("Left Attack")) {
                    if (enemy.getCurrStance()  == currStance - 1 && enemy.getIsUp() == isUp) {
                        enemy.receivedHorizontalAttack(true);
                        Debug.Log("Player attacking from the right!");
                    }
                }

                if (Input.GetButtonDown("Right Attack")) {
                    if (enemy.getCurrStance() == currStance + 1 && enemy.getIsUp() == isUp)
                        enemy.receivedHorizontalAttack(false);
                }

                if (Input.GetButtonDown("Up Attack")) {
                    if (enemy.getCurrStance() == currStance && enemy.getIsUp() != isUp && isUp == false)
                        enemy.receivedVerticalAttack(true);
                }

                if (Input.GetButtonDown("Down Attack")) {
                    if (enemy.getCurrStance() == currStance && enemy.getIsUp() == isUp && isUp == true)
                        enemy.receivedVerticalAttack(false);
                }

                if (Input.GetKeyDown(KeyCode.Q)) {
                    gameInstance.debug();
                }
                break;
            default: break;
        }
    }

    /**
     * Called every time the player collides with a trigger
     * */
    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "BattleTrigger") {
            currState = State.BATTLE_STATE;
            rigidbody2D.velocity = new Vector2(0, 0);

            other.transform.parent.gameObject.GetComponent<BattleController>().generateBattleStances();
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

            currStance = startingStance;
            isUp = startingIsUp;

            if (!isUp)
                verticalFlip();

            transform.position = new Vector3(gameInstance.stancePositions[currStance].x, transform.position.y, transform.position.z);
            rigidbody2D.gravityScale = 0;

            Destroy(other.gameObject);
        }
    }

    /********************************************/
    /********** BATTLE_STATE FUNCTIONS **********/
    /********************************************/

    public int getCurrStance() {
        return currStance;
    }

    public bool getIsUp() {
        return isUp;
    }

    /**
     * Updates the player transform
     * */
    private void updatePlayerPosition() {
        transform.position = new Vector3(gameInstance.stancePositions[currStance].x, transform.position.y, transform.position.z);
    }

    /**
     * Rotates around the floor where the stances are, creating the illusion the player is dropping or rising
     * */
    private void verticalFlip() {
        Vector3 rotateAround;
        isUp = !isUp;

        if (isUp)
            rotateAround = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        else
            rotateAround = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        transform.RotateAround(rotateAround, new Vector3(0, 0, 1), 180);
    }

    /**
     * Updates the current stance the player is on
     * */
    private void updateStancePosition(bool goingRight, int numAdvance) {
        if (goingRight)
            currStance += numAdvance;
        else
            currStance -= numAdvance;

        updatePlayerPosition();
    }

    public void receivedHorizontalAttack(bool fromRight) {
        if (fromRight)
            currStance--;
        else
            currStance++;

        health--;

        if(health <= 0 || currStance < 0 || currStance >= gameInstance.stancePositions.Count) {
            Debug.Log("The player has died!");
            Destroy(gameObject);
            return;
        }

        updatePlayerPosition();
    }

    public void receivedVerticalAttack(bool fromBelow) {
        health--;

        if(health <= 0) {
            Debug.Log("The player has died!");
            Destroy(gameObject);
            return;
        }
    }
}