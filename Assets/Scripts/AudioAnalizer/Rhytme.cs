using UnityEngine;
using System.Collections;

public class Rhytme : MonoBehaviour {
    public GameObject prefab_rhytmeObject;
    public float multipliar;
    public float radius = 5;
    public float distanceBetweenRhytmeObjs = 3;
    public int numObjSameTime = 10;
    public float initialObjDistance = 5;

    private Transform camTransform;
    private float yOffset;
    private float[] spectrum;
    private float c1, c2, c3, c4;
    private float alfa = 0;
    private float angToAdvance;

    // Use this for initialization
    void Start() {
        Random.seed = (int)System.DateTime.Now.Ticks;

        camTransform = GameObject.Find("Main Camera").transform;
        yOffset = camTransform.GetComponent<CameraFollow>().yOffset;
        float zDistanceAux = distanceBetweenRhytmeObjs;
        angToAdvance = 360 / numObjSameTime;


        for (int i = 0; i <= numObjSameTime; i++) {
            GameObject rhytmeObj = Instantiate(prefab_rhytmeObject, new Vector3(camTransform.position.x + Mathf.Cos(alfa) * radius, camTransform.position.y + yOffset + Mathf.Sin(alfa) * radius, initialObjDistance + zDistanceAux), transform.rotation) as GameObject;
            rhytmeObj.transform.parent = camTransform;
            alfa += angToAdvance;
            zDistanceAux += distanceBetweenRhytmeObjs;
        }
    }

    // Update is called once per frame
    void Update() {
        spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);

        c1 = spectrum[2] + spectrum[4] + spectrum[6] + spectrum[8] + spectrum[10];
        c2 = spectrum[12] + spectrum[14] + spectrum[16] + spectrum[18] + spectrum[20];
        c3 = spectrum[22] + spectrum[24] + spectrum[26] + spectrum[28] + spectrum[30];
        c4 = spectrum[32] + spectrum[34] + spectrum[36] + spectrum[38] + spectrum[40];
    }

    public void createRhytmeObj() {
        GameObject rhytmeObj = Instantiate(prefab_rhytmeObject, new Vector3(camTransform.position.x + Mathf.Cos(alfa) * radius, camTransform.position.y + yOffset + Mathf.Sin(alfa) * radius, initialObjDistance + distanceBetweenRhytmeObjs), transform.rotation) as GameObject;
        rhytmeObj.transform.parent = camTransform;
        alfa += angToAdvance;
    }

    public float getBeatValue(int type) {
        switch (type) {
            case 1: return c1 * multipliar; break;
            case 2: return c2 * multipliar; break;
            case 3: return c3 * multipliar; break;
            case 4: return c4 * multipliar; break;
            default: return c1 * multipliar; break;
        }
    }
}
