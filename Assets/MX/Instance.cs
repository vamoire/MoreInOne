using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Instance<T> where T : new() {

	static T __instance = default(T);
	public static T share {
		get {
			if (__instance == null) {
				__instance = new T();
			}
			return __instance;
		}
	}
}

