using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MX;

namespace MX
{
	public class ImagePlus : MonoBehaviour {
		public Sprite chinese = null;
		public Sprite english = null;

		Image image = null;

		// Use this for initialization
		void Start () {

			image = GetComponent<Image>();

			if (image && english && LanguageManager.share.language == SystemLanguage.English) {
				image.sprite = english;
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}

