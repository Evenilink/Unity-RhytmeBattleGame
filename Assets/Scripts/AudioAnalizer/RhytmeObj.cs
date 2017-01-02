using UnityEngine;
using System.Collections;

public class RhytmeObj : MonoBehaviour {
    private float speed;
    private int type;
    private float beatValue;
    private Rhytme script_Rhytme;

    void Start() {
        speed = Random.Range(2.0f, 5.0f);
        type = Random.Range(0, 4);
        GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));// ("_SpecColor", Color.blue);       //Doing nothing!!!!!!!!!!!!
        script_Rhytme = GameObject.Find("Main Camera").GetComponent<Rhytme>();
    }

    public int getType() {
        return type;
    }

    void Update() {
        if (transform.position.z <= -10) {
            script_Rhytme.createRhytmeObj();
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);

        beatValue = script_Rhytme.getBeatValue(type);
        transform.localScale = new Vector3(beatValue, beatValue, beatValue);
    }
}
