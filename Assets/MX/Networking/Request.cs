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

		static Request __request = null;
		/// <summary>
		/// 网络请求
		/// </summary>
		/// <returns></returns>
		public static Request Share {
			get {
				if (__request == null) {
					__request = Request.Create();
					DontDestroyOnLoad(__request.gameObject);
				}
				return __request;
			}
		}

		public static Request Create() {
			GameObject obj = new GameObject("MX Request");
			Request request = obj.AddComponent<Request>();
			return request;
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		void OnDestroy()
		{
			StopAllCoroutines();
		}

		/// <summary>
		/// Get请求
		/// </summary>
		/// <param name="url">接口地址</param>
		/// <param name="action">接口回调</param>
		/// <returns></returns>
		public Coroutine Get(string url, System.Action<bool, string> action) {
			Coroutine cor = StartCoroutine(GetText(url, (bool ret, string text)=>{
				action(ret, text);
			}));
			return cor;
		}

		/// <summary>
		/// Post请求
		/// </summary>
		/// <param name="url">接口地址</param>
		/// <param name="form">Post参数</param>
		/// <param name="action">接口回调</param>
		/// <returns></returns>
		public Coroutine Post(string url, WWWForm form, System.Action<bool, string> action) {
			Coroutine cor = StartCoroutine(PostForm(url, form, (bool ret, string text)=>{
				action(ret, text);
			}));
			return cor;
		}

		/// <summary>
		/// 取消请求
		/// </summary>
		/// <param name="cor"></param>
		public void Cancel(Coroutine cor) {
			StopCoroutine(cor);
		}

		/// <summary>
		/// 取消所有请求
		/// </summary>
		public void CancelAll() {
			StopAllCoroutines();
		}

		IEnumerator GetText(string url, System.Action<bool,string> action) {
			UnityWebRequest www = UnityWebRequest.Get(url);
			yield return www.SendWebRequest();
	
			if(www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
				action(false, "");
			}
			else {
				// Show results as text
				Debug.Log(www.downloadHandler.text);
				action(true, www.downloadHandler.text);
				// Or retrieve results as binary data
				// byte[] results = www.downloadHandler.data;
			}
		}

		IEnumerator PostForm(string url, WWWForm form, System.Action<bool,string> action) {
			UnityWebRequest www = UnityWebRequest.Post(url, form);
			yield return www.SendWebRequest();
	
			if(www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
				action(false, "");
			}
			else {
				// Show results as text
				Debug.Log(www.downloadHandler.text);
				action(true, www.downloadHandler.text);
				// Or retrieve results as binary data
				// byte[] results = www.downloadHandler.data;
			}
		}

		// void CreateUnityWebRequest() {
		// 	UnityWebRequest wr = new UnityWebRequest(); // Completely blank
		// 	UnityWebRequest wr2 = new UnityWebRequest("http://www.mysite.com"); // Target URL is set

		// 	// the following two are required to web requests to work
		// 	wr.url = "http://www.mysite.com";
		// 	wr.method = UnityWebRequest.kHttpVerbGET;   // can be set to any custom method, common constants privided

		// 	wr.useHttpContinue = false;
		// 	wr.chunkedTransfer = false;
		// 	wr.redirectLimit = 0;  // disable redirects
		// 	wr.timeout = 60;       // don't make this small, web requests do take some time
		// }
	}
}
