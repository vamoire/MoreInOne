using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MX.Json;

/// <summary>
/// 应用设置信息
/// </summary>
public class AppConfig : JsonConfig<AppConfig> {

	public AppConfig() {
		fileName = "appConfig.json";
	}

	/// <summary>
	/// 当前游戏关卡
	/// </summary>
	public int gameCurrentIdx = 0;
}
