/*using UnityEngine;
using System.Collections;

public class FrameRateManager : MonoBehaviour {

	public int frameRate = 120;

    void Awake () {
        //Screen.SetResolution (960, 720, false);
	    QualitySettings.vSyncCount = 0;  // VSync must be disabled
	    Application.targetFrameRate = frameRate;
    }
	void Start() {
		StartCoroutine(changeFramerate());
	}
	IEnumerator changeFramerate() {
        yield return new WaitForSeconds(1);
        Application.targetFrameRate = frameRate;
    }
}*/