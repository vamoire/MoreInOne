using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MX.Json;

public class Level : JsonConfig<Level> {

	public Level() {
		fileName = "level.json";
	}
	
	public int currentLevelIdx {
		get {
			int idx = AppConfig.share.gameCurrentIdx;
			idx = idx % levelInfo.Length;
			return idx;
		}
		set {
			AppConfig.share.gameCurrentIdx = value;
			AppConfig.share.Flush();
		}
	}

	public LevelInfo currentLevelInfo {
		get {
			return levelInfo[currentLevelIdx];
		}
	}

	public LevelInfo[] levelInfo;
}

public class LevelInfo {
	public LevelInfo() {

	}

	public string name = "";
	public string image = "";
	public string scene = "";
}
