using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitRecord {
	public static readonly float tMin = 0.001f;
	public float t = float.MaxValue;
	public Vector3 p;
	public Vector3 normal;
	public Vector3 attenuation;
	public MaterialData material;
	public Ray scatteredRay;
	public bool IsHit() { return t != float.MaxValue; }
	public bool SetT(float tIn) {
		if (tIn >= t) return false;
		if (tIn <= tMin) return false;
		t = tIn;
		return true;
	}
}
