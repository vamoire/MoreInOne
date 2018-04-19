﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace MX.Json
{
	/// <summary>
	/// 配置文件
	/// </summary>
	public abstract class JsonConfig<T> where T : JsonConfig<T>, new() {


		static T __instance = default(T);

		public static T share {
			get {
				if (__instance == null) {
					__instance = new T();
					__instance = Read(__instance.fileName);
				}
				return __instance;
			}
		}

		/// <summary>
		/// 配置文件路径
		/// </summary>
		public string fileName = "config.json";
		
		/// <summary>
		/// 读取配置文件并反序列化
		/// </summary>
		/// <param name="fileName">配置文件名称</param>
		/// <returns>配置文件对象</returns>
		public static T Read(string fileName) {
			string path = FileHelper.PersistentDataPath(fileName);
			Debug.Log(path);
			if (!File.Exists(path)) {
				path = FileHelper.StreamingAssetsPath(fileName);
			}
			string json = FileHelper.ReadStringFromFile(path);
			T config = JsonConvert.DeserializeObject<T>(json);
			config.fileName = fileName;
			return config;
		}

		/// <summary>
		/// 数据保存到本地
		/// </summary>
		public void Flush() {
			string json = JsonConvert.SerializeObject(this);
			string path = FileHelper.PersistentDataPath(fileName);
			FileHelper.WriteStringToFile(json, path);
		}

	}

}
