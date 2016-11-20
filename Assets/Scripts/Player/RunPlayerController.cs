using UnityEngine;
using System.Collections;

public class RunPlayerController : MonoBehaviour {

    public LayerMask whatIsGround;
    public Transform groundTransform;
    public float movementSpeed = 5f;
    public float jumpPower = 200f;

    private Rigidbody2D rigidbody2d;
    private bool isInGround;
    private float checkRadius = 0.1f;

	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate () {
        isInGround = Physics2D.OverlapCircle(groundTransform.position, checkRadius, whatIsGround);

        rigidbody2d.velocity = new Vector2(movementSpeed, rigidbody2d.velocity.y);

        if (Input.GetButtonDown("Jump") && isInGround) {
            rigidbody2d.AddForce(new Vector2(0, jumpPower));
        }
	}
}
