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
    private int currStance;
    private bool isUp;
    private int startingStance = 3;
    private bool startingIsUp = true;

    private Rigidbody2D rigidbody2D;

    void Start() {
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
                else if (Input.GetButtonDown("Transport Right")) {
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
                else if (Input.GetButtonDown("Transport Up") && !isUp) {
                    if (enemy.getCurrStance() == currStance && enemy.getIsUp() != isUp)
                        Debug.Log("Attacking from below!");
                    else {
                        isUp = true;
                        verticalFlip();
                    }
                }
                else if (Input.GetButtonDown("Transport Down") && isUp) {
                    if (enemy.getCurrStance() == currStance && enemy.getIsUp() != isUp)
                        Debug.Log("Attacking from above!");
                    else {
                        isUp = false;
                        verticalFlip();
                    }
                }
                else if (Input.GetButtonDown("Attack")) {
                    int distanceToEnemy = currStance - enemy.getCurrStance();
                    if (Mathf.Abs(distanceToEnemy) == 1 && isUp == enemy.getIsUp()) {
                        if (distanceToEnemy > 0)
                            enemy.receivedHorizontalAttack(true);
                        else
                            enemy.receivedHorizontalAttack(false);
                    } else if(distanceToEnemy == 0 && isUp != enemy.getIsUp())
                        enemy.receivedVerticalAttack();
                    else
                        Debug.Log("The player is not attacking anybody!");
                }
                else if (Input.GetKeyDown(KeyCode.Q)) {
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

    /********** BATTLE_STATE FUNCTIONS **********/

    /**
     * Updates the current stance the player is on
     * */
    private void updateStancePosition(bool goingRight, int numAdvance) {
        if (goingRight)
            currStance += numAdvance;
        else
            currStance -= numAdvance;

        transform.position = new Vector3(gameInstance.stancePositions[currStance].x, transform.position.y, transform.position.z);
    }

    /**
     * Rotates around the floor where the stances are, creating the illusion the player is dropping or rising
     * */
    private void verticalFlip() {
        Vector3 rotateAround;

        if (isUp)
            rotateAround = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        else
            rotateAround = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        transform.RotateAround(rotateAround, new Vector3(0, 0, 1), 180);
    }

    public int getCurrStance() {
        return currStance;
    }

    public bool getIsUp() {
        return isUp;
    }
}