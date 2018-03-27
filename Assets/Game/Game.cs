using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static int levelIdx = 0;

	// Use this for initialization
	void Start () {
		Debug.Log(levelIdx);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonTouch_Back() {
		SceneManager.LoadScene("List");
	}
}
