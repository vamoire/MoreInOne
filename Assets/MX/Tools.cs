using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MX
{
	/// <summary>
	/// 工具
	/// </summary>
	public class Tools {
		
		/// <summary>
		/// 相机显示的范围大小
		/// </summary>
		/// <param name="camera">相机</param>
		/// <returns></returns>
		public static Vector2 CameraSize(Camera camera) {
			float h = camera.orthographicSize;
			float w = h / Screen.height * Screen.width;
			return new Vector2(w * 2, h * 2);
		}

		/// <summary>
		/// 点是否在UI组件范围内
		/// </summary>
		/// <param name="p">Vector3</param>
		/// <param name="rt">RectTransform</param>
		/// <returns></returns>
		public static bool PointInRect(Vector3 p, RectTransform rt) {
			var rtp = rt.position;
			var rts = rt.sizeDelta;
			return p.x > rtp.x - rts.x / 2 && p.x < rtp.x + rts.x / 2 && p.y > rtp.y - rts.y / 2 && p.y < rtp.y + rts.y / 2;
		}

		/// <summary>
		/// 设置GameObject所在的Layer
		/// </summary>
		/// <param name="obj">GameObject</param>
		/// <param name="name">Layer名称</param>
		/// <param name="child">同时改变子对象</param>
		public static void SetGameObjectLayer(GameObject obj, string name, bool child = true) {
			int layer = LayerMask.NameToLayer(name);
			obj.layer = layer;
			if (child) {
				foreach (UnityEngine.Transform item in obj.GetComponentsInChildren<UnityEngine.Transform>())
				{
					item.gameObject.layer = layer;
				}
			}
		}

		/// <summary>
		/// 是否点击到场景中的按钮
		/// </summary>
		/// <returns></returns>
		public static bool TouchInButton() {
			Button[] arr = GameObject.FindObjectsOfType<Button>();
			Vector3 p = Input.mousePosition;
			bool ret = PointInUIComponent<Button>(p, arr);
			return ret;
		}

		/// <summary>
		/// 是否点在UI组件范围内
		/// </summary>
		/// <param name="point">点</param>
		/// <param name="array">UI组件数组</param>
		/// <returns></returns>
		public static bool PointInUIComponent<T>(Vector3 point, T[] array) where T : UnityEngine.EventSystems.UIBehaviour{
			bool ret = false;
			foreach (T item in array)
			{
				GameObject obj = item.gameObject;
				if (obj.activeInHierarchy) {
					RectTransform rt = obj.GetComponent<RectTransform>();
					if(PointInRect(point, rt)) {
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
	}

	/// <summary>
	/// 精灵扩展
	/// </summary>
	public class SpriteExtend {
		/// <summary>
		/// 创建精灵
		/// </summary>
		/// <param name="texture">纹理</param>
		/// <returns></returns>
		public static Sprite Create(Texture2D texture) {
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
			return sprite;
		}

		/// <summary>
		/// 缩放精灵
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		// public static Sprite Scale(Sprite sprite, float scale) {
		// 	Texture2D texture = sprite.texture;
		// 	int width = (int)(texture.width * scale);
		// 	int height = (int)(texture.height * scale);
		// 	Texture2D newTexture = TextureExtend.ScaleTexture(texture, width, height);
		// 	return Create(texture);
		// }

		/// <summary>
		/// 按钮按下缩小的精灵
		/// </summary>
		/// <param name="sprite">按钮普通精灵</param>
		/// <param name="scale">按钮按下缩小比例(0 ~ 1)</param>
		/// <returns></returns>
		public static Sprite ButtonPressScale(Sprite sprite, float scale) {
			Texture2D texture = sprite.texture;
			int width = (int)(texture.width / scale);
			int height = (int)(texture.height / scale);
			Texture2D newTexture = TextureExtend.AddBlankSpace(texture, width, height);
			return Create(newTexture);
		}

		/// <summary>
		/// 精灵叠加
		/// </summary>
		/// <param name="oneSprite"></param>
		/// <param name="otherSprite"></param>
		/// <returns></returns>
		public static Sprite Merge(Sprite oneSprite, Sprite otherSprite) {
			Texture2D texture = TextureExtend.Merge(oneSprite.texture, otherSprite.texture);
			return Create(texture);
		}
	}

	/// <summary>
	/// 纹理扩展
	/// </summary>
	public class TextureExtend {
		/// <summary>
		/// 清空纹理
		/// </summary>
		/// <param name="texture">纹理</param>
		public static void Clear(Texture2D texture) {
			Color color = Color.clear;
			for (int x = 0; x < texture.width; x++)
			{
				for (int y = 0; y < texture.height; y++)
				{
					texture.SetPixel(x, y, color);
				}
			}
			texture.Apply();
		}

		/// <summary>
		/// 纹理边缘添加透明纹理
		/// </summary>
		/// <param name="resTexture">原纹理</param>
		/// <param name="width">新纹理宽</param>
		/// <param name="height">新纹理高</param>
		/// <returns>新纹理</returns>
		public static Texture2D AddBlankSpace(Texture2D resTexture, int width, int height) {
			//new texture
			Texture2D texture = new Texture2D(width, height);
			//clear
			Clear(texture);
			//old texture fill to new texture
			Vector2Int dis = new Vector2Int(texture.width - resTexture.width, texture.height - resTexture.height);
			texture.SetPixels(dis.x / 2, dis.y / 2, resTexture.width, resTexture.height, resTexture.GetPixels());
			//flush
			texture.Apply();
			return texture;
		}

		/// <summary>
		/// 拉伸纹理
		/// </summary>
		/// <param name="source">原纹理</param>
		/// <param name="targetWidth">拉伸宽度</param>
		/// <param name="targetHeight">拉伸高度</param>
		/// <returns></returns>
		public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
		{
			Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

			for (int i = 0; i < result.height; ++i)
			{
				for (int j = 0; j < result.width; ++j)
				{
					Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
					result.SetPixel(j, i, newColor);
				}
			}
			result.Apply();
			return result;
		}

		/// <summary>
		/// 纹理叠加(中心对齐)
		/// </summary>
		/// <param name="oneTexture">背景纹理</param>
		/// <param name="otherTexture">前景纹理</param>
		/// <returns></returns>
		public static Texture2D Merge(Texture2D oneTexture, Texture2D otherTexture) {
			int width = Mathf.Max(oneTexture.width, otherTexture.width);
			int height = Mathf.Max(oneTexture.height, otherTexture.height);
			//new texture
			Texture2D texture = new Texture2D(width, height);
			//clear
			Clear(texture);
			//one
			Vector2Int oneDis = new Vector2Int(width - oneTexture.width, height - oneTexture.height);
			texture.SetPixels(oneDis.x / 2, oneDis.y / 2, oneTexture.width, oneTexture.height, oneTexture.GetPixels());
			texture.Apply();
			//other
			Vector2Int otherDis = new Vector2Int(width - otherTexture.width, height - otherTexture.height);
			for(int x = 0; x < otherTexture.width; ++x) {
				for (int y = 0; y < otherTexture.height; y++)
				{
					Color c = otherTexture.GetPixel(x, y);
					Vector2Int p = new Vector2Int(otherDis.x / 2 + x, otherDis.y / 2 + y);
					Color bgColor = texture.GetPixel(p.x, p.y);
					float r = c.r * c.a + bgColor.r * bgColor.a * (1 - c.a);
					float g = c.g * c.a + bgColor.g * bgColor.a * (1 - c.a);
					float b = c.b * c.a + bgColor.b * bgColor.a * (1 - c.a);
					Color color = new Color(r, g, b);
					texture.SetPixel(p.x, p.y, color);
					// if (c.a > 0.9) {
					// 	texture.SetPixel(p.x, p.y, c);
					// } 
				}
			}
			texture.Apply();
			return texture;
		}
	}
}
