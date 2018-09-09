using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Util {
	private static System.Random random_ = new System.Random();
	public static float UnitRandFloat() {
		var r = random_.NextDouble();
		return (float)r;
	}
	public static Vector3 RandomInUnitSphere() {
		while (true) {
			Vector3 v = new Vector3(UnitRandFloat(), UnitRandFloat(), UnitRandFloat());
			if (v.sqrMagnitude < 1.0f) return v;
		}
	}
}
