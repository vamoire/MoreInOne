using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class List : MonoBehaviour {

	public GameObject content = null;

	public int row = 4;//列数
	public float width = 768;//总宽度

	// Use this for initialization
	void Start () {
		LoadData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadData() {
		int count = Level.share.levelInfo.Length;//总个数

		RectTransform rt = content.GetComponent<RectTransform>();
		float dis = width / row;//列间距
		int line = (count + row - 1) / row;//行数
		float height = line * dis;//总高度
		
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
		Vector3 begin = new Vector3(-width / 2, height / 2, 0);//左上角起点
		Vector3 first = begin + new Vector3(dis / 2, -dis / 2, 0);
		
		for (int i = 0; i < count; i++)
		{
			//info
			LevelInfo info = Level.share.levelInfo[i];
			int x = i % row;
			int y = i / row;
			Vector3 point = first + new Vector3(x * dis, -y * dis, 0);
			GameObject obj = Instantiate(Resources.Load("ButtonLevel")) as GameObject;
			RectTransform child = obj.GetComponent<RectTransform>();
			child.SetParent(rt);
			child.anchoredPosition3D = point;
			//name
			Text text = obj.GetComponentInChildren<Text>();
			text.text = info.name;
			//button
			Button button = obj.GetComponent<Button>();
			int levelIdx = i;
			button.onClick.AddListener(()=>{
				Level.share.currentLevelIdx = levelIdx;
				SceneManager.LoadScene("Game");
			});
		}
		
	}

	public void ButtonTouch_Back() {
		SceneManager.LoadScene("Home");
	}

}
