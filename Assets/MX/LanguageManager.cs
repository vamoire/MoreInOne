using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MX
{
	public class LanguageManager : Instance<LanguageManager> {

		const string Key_Language = "LanguageManager_language";

		public SystemLanguage language {
			get {
				int num = PlayerPrefs.GetInt(Key_Language, LanguageToNum(SystemLanguage.Chinese));
				return NumToLanguage(num);
			}
			set {
				languageNum = LanguageToNum(value);
				PlayerPrefs.SetInt(Key_Language, languageNum);
			}
		}

		int languageNum = 1;

		//多语言数据
		string[][]data;

		public LanguageManager() {

			languageNum = LanguageToNum(language);	

			LoadData();
		}

		void LoadData() {
			//加载配置文件
			TextAsset asset = Resources.Load("language", typeof(TextAsset)) as TextAsset;
			string[] lineData = asset.text.Split(new char[]{'\n'});
			data = new string[lineData.Length][];
			for (int i = 0; i < lineData.Length; i++)
			{
				data[i] = lineData[i].Split(new char[]{','});
			}
		}

		/// <summary>
		/// 语言转序号(1:中文 2:英文)
		/// </summary>
		/// <param name="language"></param>
		/// <returns></returns>
		int LanguageToNum(SystemLanguage language) {
			int num = 1; //SystemLanguage.Chinese
			if (language == SystemLanguage.English) {
				num = 2;
			}
			return num;
		}

		/// <summary>
		/// 序号转语言 (1:中文 2:英文)
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		SystemLanguage NumToLanguage(int num) {
			SystemLanguage language = SystemLanguage.Chinese;
			if (num == 2) {
				language = SystemLanguage.English;
			}
			return language;
		}


		/// <summary>
		/// 获取多语言Key对应的当前语言文字
		/// </summary>
		/// <param name="name">多语言Key</param>
		/// <returns>当前语言文字</returns>
		public string Key(string name) {
			string value = name;
			int idx = languageNum;
			for(int i = 0; i < data.Length && name.Length > 0; i++) {
				//key
				string key = data[i][0];
				if (key == name) {
					//find
					value = data[i][idx];
					break;
				}
			}
			return value;
		}
	}
}
