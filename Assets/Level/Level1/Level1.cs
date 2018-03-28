using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {

	[SerializeField]
	Controller controller = null;

	// Use this for initialization
	void Start () {
		controller.OnPressUp = ()=>{
			Debug.Log("Level1 up");
			
		};
		controller.OnPressDown = ()=>{
			Debug.Log("Level1 down");

		};
		controller.OnPressLeft = ()=>{
			Debug.Log("Level1 left");

		};
		controller.OnPressRight = ()=>{
			Debug.Log("Level1 right");

		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
