using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制器
/// </summary>
public class Controller : MonoBehaviour {

	[SerializeField]
	KeyCode upKey = KeyCode.W;
	[SerializeField]
	KeyCode downKey = KeyCode.S;
	[SerializeField]
	KeyCode leftKey = KeyCode.A;
	[SerializeField]
	KeyCode rightKey = KeyCode.D;

	public System.Action OnPressUp = null;
	public System.Action OnPressDown = null;
	public System.Action OnPressLeft = null;
	public System.Action OnPressRight = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(upKey)) {
			ButtonTouch_Up();
		}
		else if (Input.GetKeyDown(downKey)) {
			ButtonTouch_Down();
		}
		else if (Input.GetKeyDown(leftKey)) {
			ButtonTouch_Left();
		}
		else if (Input.GetKeyDown(rightKey)) {
			ButtonTouch_Right();
		}
	}

	public void ButtonTouch_Up() {
		if (OnPressUp != null)
			OnPressUp();
	}
	public void ButtonTouch_Down() {
		if (OnPressDown != null)
			OnPressDown();
	}
	public void ButtonTouch_Left() {
		if (OnPressLeft != null)
			OnPressLeft();
	}
	public void ButtonTouch_Right() {
		if (OnPressRight != null)
			OnPressRight();
	}
}
