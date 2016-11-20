using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float smoothTime = 0.5f;

    private GameObject player;
    private Vector3 position;
    private Vector2 velocity;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
	
	void Update () {
        position.x = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTime);
        position.y = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(position.x, position.y, transform.position.z);
	}
}
