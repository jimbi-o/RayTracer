using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Util {
	private static System.Random random_ = new System.Random();
	public static float UnitRandFloat() {
		var r = random_.NextDouble();
		return (float)r;
	}
}
