using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitRecord {
	public float t = float.MaxValue;
	public Vector3 p;
	public Vector3 normal;
	public Vector3 attenuation;
	public MaterialData material;
	public Ray scatteredRay;
	private static float tMin_ = 0.001f;
	public bool IsHit() { return t != float.MaxValue; }
	public bool SetT(float tIn) {
		if (tIn >= t) return false;
		if (tIn <= tMin_) return false;
		t = tIn;
		return true;
	}
}
