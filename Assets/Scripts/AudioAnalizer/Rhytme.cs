using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rhytme : MonoBehaviour {

    public GameObject[] rhytmeObjects;
    public float multipliar;
    public float radius = 5;
    public float zDistance = 3;
    public int numObjSameTime = 10;

    private Transform playerTransform;
    private List<GameObject> rhytmeObjC1;
    private List<GameObject> rhytmeObjC2;
    private List<GameObject> rhytmeObjC3;
    private List<GameObject> rhytmeObjC4;
    private float[] spectrum;
    private float c1, c2, c3, c4;
    private float alfa = 0;
    private float angToAdvance;

    // Use this for initialization
    void Start() {
        Random.seed = (int)System.DateTime.Now.Ticks;

        rhytmeObjC1 = new List<GameObject>();
        rhytmeObjC2 = new List<GameObject>();
        rhytmeObjC3 = new List<GameObject>();
        rhytmeObjC4 = new List<GameObject>();

        playerTransform = GameObject.FindWithTag("Player").transform;
        float zDistanceAux = zDistance;
        angToAdvance = 360 / numObjSameTime;

        for (int i = 0; i <= numObjSameTime; i++) {
            GameObject rhytmeObj = Instantiate(rhytmeObjects[0], new Vector3(Mathf.Cos(alfa) * radius, Mathf.Sin(alfa) * radius, 10 + zDistanceAux), transform.rotation) as GameObject;
            rhytmeObj.transform.parent = playerTransform;
            alfa += angToAdvance;
            zDistanceAux += zDistance;

            switch (Random.Range(0, 4)) {
                case 0: rhytmeObjC1.Add(rhytmeObj); break;
                case 1: rhytmeObjC2.Add(rhytmeObj); break;
                case 2: rhytmeObjC3.Add(rhytmeObj); break;
                case 3: rhytmeObjC4.Add(rhytmeObj); break;
                default: break;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);

        float c1 = spectrum[2] + spectrum[4] + spectrum[6] + spectrum[8] + spectrum[10];
        float c2 = spectrum[12] + spectrum[14] + spectrum[16] + spectrum[18] + spectrum[20];
        float c3 = spectrum[22] + spectrum[24] + spectrum[26] + spectrum[28] + spectrum[30];
        float c4 = spectrum[32] + spectrum[34] + spectrum[36] + spectrum[38] + spectrum[40];

        for (int i = 0; i < rhytmeObjC1.Count; i++) {
            rhytmeObjC1[i].transform.localScale = new Vector3(c1 * multipliar, c1 * multipliar, c1 * multipliar);
        }

        for (int i = 0; i < rhytmeObjC2.Count; i++) {
            rhytmeObjC2[i].transform.localScale = new Vector3(c2 * multipliar, c2 * multipliar, c2 * multipliar);
        }

        for (int i = 0; i < rhytmeObjC3.Count; i++) {
            rhytmeObjC3[i].transform.localScale = new Vector3(c3 * multipliar, c3 * multipliar, c3 * multipliar);
        }

        for (int i = 0; i < rhytmeObjC4.Count; i++) {
            rhytmeObjC4[i].transform.localScale = new Vector3(c4 * multipliar, c4 * multipliar, c4 * multipliar);
        }
    }
}
