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


		Button button = null;

		// Use this for initialization
		void Start () {
			button = GetComponent<Button>();

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


