using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair : MonoBehaviour {

	public System.Action OnMoveFinished = null;
	public System.Action OnMoveFailed = null;
	public bool moveFinished = false;

	[SerializeField]
	GameObject begin = null;
	[SerializeField]
	GameObject end = null;
	[SerializeField]
	LineRenderer line = null;

	bool moving = false;

	float speed = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			//需要移动的距离
			float moveDis = speed * Time.deltaTime;
			//move
			Vector3 b = begin.transform.position;
			Vector3 e = line.GetPosition(line.positionCount - 2);
			float dis = Vector3.Distance(b, e);
			Vector3 np = b;
			bool move2point = false;
			if (moveDis >= dis) {
				np = e;
				//move to point
				move2point = true;
			}
			else {
				np = b + (e - b) / dis * moveDis;
			}
			begin.transform.position = np;
			line.SetPosition(line.positionCount - 1, np);
			//move 2 point
			if (move2point) {
				//delete point
				line.SetPosition(line.positionCount - 2, np);
				line.positionCount -= 1;
				if (line.positionCount == 1) {
					moving = false;
					moveFinished = true;
					//move finish
					if (OnMoveFinished != null) {
						OnMoveFinished();
					}
				}
			}

			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				GameObject obj = hit.collider.gameObject;
				if (obj == begin) {
					if (line.positionCount >= 2) {
						moving = true;
					}
				}
			}
		}
	}

	public void StopMove() {
		moving = false;
	}

	[ContextMenu("设置起始点")]
	void SetBeginEndPoint() {
		if (line.positionCount < 2) {
			line.positionCount = 2;
		}
		line.SetPosition(line.positionCount - 1, begin.transform.position);
		line.SetPosition(0, end.transform.position);
	}
}
