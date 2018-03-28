using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	public Text textLevelName = null;

	// Use this for initialization
	void Start () {
		LevelInfo levelInfo = Level.share.currentLevelInfo;
		textLevelName.text = levelInfo.name;

		SceneManager.LoadScene(levelInfo.scene, LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonTouch_Back() {
		SceneManager.LoadScene("List");
	}
}
