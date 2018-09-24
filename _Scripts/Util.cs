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
	public static Color ConvertToColor(Vector3 v) {
		return new Color(v.x, v.y, v.z, 1.0f);
	}
	public static Vector3 GetRefractedRay(Vector3 direction, Vector3 normal, float n) {
		var dt = Vector3.Dot(direction, normal);
		var discriminant = 1.0f - n * n * (1.0f - dt * dt);
		if (discriminant <= 0.0f) return Vector3.zero;
		return n * (direction - normal * dt) - normal * Mathf.Sqrt(discriminant);
	}
	public static float GetSchlick(float cosine, float reflectionIndex) {
		float r0 = (1.0f - reflectionIndex) / (1.0f + reflectionIndex);
		r0 = r0 * r0;
		return r0 + (1.0f - r0) * Mathf.Pow(1.0f - cosine, 5.0f);
	}
	public static void Swap<T>(ref T lhs, ref T rhs) {
		T temp;
		temp = lhs;
		lhs = rhs;
		rhs = temp;
	}
}
