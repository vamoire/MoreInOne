﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	public Text textLevelName = null;

	Pair[] pairs;

	bool gameRunning = false;

	// Use this for initialization
	void Start () {
		LevelInfo levelInfo = Level.share.currentLevelInfo;
		textLevelName.text = levelInfo.name;

		SceneManager.LoadScene(levelInfo.scene, LoadSceneMode.Additive);

		SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode)=>{
			if (scene.name == levelInfo.scene) {
				LoadLevelSuccess();
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonTouch_Back() {
		SceneManager.LoadScene("List");
	}

	void LoadLevelSuccess() {
		pairs = FindObjectsOfType<Pair>();
		foreach (Pair pair in pairs)
		{
			pair.OnMoveFinished = ()=>{
				bool success = true;
				foreach (Pair item in pairs)
				{
					if (item.moveFinished == false) {
						success =false;
					}
				}
				if (success) {
					GameSuccess();
				}
			};
			pair.OnMoveFailed = ()=>{
				GameFail();
			};
		}

		gameRunning = true;
	}

	void GameSuccess() {
		if (gameRunning == false) {
			return;
		}
		gameRunning = false;
		Debug.Log("Success");
		SceneManager.LoadScene("GameSuccess");
	}

	void GameFail() {
		if (gameRunning == false) {
			return;
		}
		gameRunning = false;
		foreach (Pair pair in pairs)
		{
			pair.StopMove();
		}
		Debug.Log("Fail");
		SceneManager.LoadScene("GameFail");
	}
}
