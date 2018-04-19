using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MX
{
	/// <summary>
	/// 按钮+
	/// </summary>
	public class ButtonPlus : MonoBehaviour {
		/// <summary>
		/// 点击缩小
		/// </summary>
		public bool pressScaleEnabled = false;
		float pressScale = 0.8f;
		
		/// <summary>
		/// 按钮点击音效
		/// </summary>
		public string pressSound = "";
		/// <summary>
		/// 按钮默认点击音效
		/// </summary>
		string pressSoundDefault = "";

		Button button = null;

		// Use this for initialization
		void Start () {
			button = GetComponent<Button>();

			//按钮点击音效
			button.onClick.AddListener(()=>{
				string name = pressSoundDefault;
				if (pressSound.Length > 0) {
					name = pressSound;
				}
				if (name.Length > 0) {
					SoundManager.share.PlayeEffect(name);
				}
			});

			if (pressScaleEnabled) {
				UI_PressScale();
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void UI_PressScale() {
			button.transition = Button.Transition.SpriteSwap;
			SpriteState spriteState = button.spriteState;
			Sprite sprite = button.gameObject.GetComponent<Image>().sprite;
			spriteState.pressedSprite = SpriteExtend.ButtonPressScale(sprite, pressScale);
			button.spriteState = spriteState;
		}
	}
}


