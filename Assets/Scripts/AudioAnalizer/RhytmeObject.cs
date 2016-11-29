using UnityEngine;
using System.Collections;

public class RhytmeObject : MonoBehaviour {

    private float speed = 3;

	void Start () {
        GetComponent<Renderer>().material.SetColor("_SpecColor", Color.blue);
	}
	
	void Update () {
        if (transform.position.z <= -10)
            Destroy(gameObject);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
	}
}