using UnityEngine;
using System.Collections;

public class Rhytme : MonoBehaviour {

    public float multipliar;

    private float[] spectrum;
    private float c1, c2, c3, c4;

    private GameObject[] rhytmeCubes;

    // Use this for initialization
    void Start() {
        Random.seed = (int)System.DateTime.Now.Ticks;
        rhytmeCubes = GameObject.FindGameObjectsWithTag("RhytmeCube");
    }

    // Update is called once per frame
    void Update() {
        spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);

        float c1 = spectrum[2] + spectrum[4] + spectrum[6] + spectrum[8] + spectrum[10];
        float c2 = spectrum[12] + spectrum[14] + spectrum[16] + spectrum[18] + spectrum[20];
        float c3 = spectrum[22] + spectrum[24] + spectrum[26] + spectrum[28] + spectrum[30];
        float c4 = spectrum[32] + spectrum[34] + spectrum[36] + spectrum[38] + spectrum[40];

        for(int i = 0; i < rhytmeCubes.Length; i++) {
            rhytmeCubes[i].transform.localScale = new Vector3(c1 * multipliar, c1 * multipliar, c1 * multipliar);
            rhytmeCubes[i].transform.localEulerAngles += new Vector3(0, Random.value * 20 * c1, Random.value * 20 * c1);
        }
    }
}
