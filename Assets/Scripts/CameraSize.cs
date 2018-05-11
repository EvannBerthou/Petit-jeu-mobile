using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour {

	[SerializeField]
	int Ratio = 0;

	void Start () {
		float aspect = (float) Screen.width / (float) Screen.height;
		Camera.main.orthographicSize = 24;
	}
}
