using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Util {
	private static System.Random random_ = new System.Random();
	public static float UnitRandFloat() {
		int max = 10000;
		var r = random_.Next(0, max) / (float)max;
		return r;
	}
	public static Vector3 RandomInUnitSphere() {
		while (true) {
			Vector3 v = 2.0f * new Vector3(UnitRandFloat(), UnitRandFloat(), UnitRandFloat()) - Vector3.one;
			if (v.sqrMagnitude < 1.0f) return v;
		}
	}
}
