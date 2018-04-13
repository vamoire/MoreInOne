using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 网络请求
/// </summary>
namespace MX.Networking
{
	public class Request : MonoBehaviour {
		void Start()
		{
			
		}

		void ShowCreate() {
			
		}

		static Request __request = null;
		static Request Create() {
			GameObject obj = new GameObject("MX Request");
			Request request = obj.AddComponent<Request>();
			return request;
		}
		
	}
}
