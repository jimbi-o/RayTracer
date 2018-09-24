using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Aabb {
	public static float Min(float a, float b) { return a < b ? a : b; } 
	public static float Max(float a, float b) { return a > b ? a : b; }
	public static Aabb SurroundingBox(Aabb box0, Aabb box1) {
		var small = new Vector3(Min(box0.min.x, box1.min.x), Min(box0.min.y, box1.min.y), Min(box0.min.z, box1.min.z));
		var big = new Vector3(Max(box0.max.x, box1.max.x), Max(box0.max.y, box1.max.y), Max(box0.max.z, box1.max.z));
		return new Aabb(small, big);
	}
	public Vector3 min, max;
	public Aabb(Vector3 a, Vector3 b) {
		min = a;
		max = b;
	}
	public bool Hit(Ray ray, float tMin, float tMax) {
		for (int a = 0; a < 3; ++a) {
			var invD = 1.0f / ray.Direction[a];
			var t0 = (min[a] - ray.Origin[a]) * invD;
			var t1 = (max[a] - ray.Origin[a]) * invD;
			if (invD < 0.0f) Util.Swap<float>(ref t0, ref t1);
			tMin = t0 > tMax ? t0 : tMin;
			tMax = t1 < tMax ? t1 : tMax;
			if (tMax <= tMin) return false;
		}
		return true;
	}
}
