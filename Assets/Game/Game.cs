using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	// Use this for initialization
	void Start () {
		SceneManager.LoadScene(Level.share.currentLevelInfo.scene, LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonTouch_Back() {
		SceneManager.LoadScene("List");
	}
}
