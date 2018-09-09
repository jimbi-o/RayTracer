using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitRecord {
	public float t;
	public Vector3 p;
	public Vector3 normal;
}
public abstract class Hitable {
	public abstract bool Hit(Ray ray, float tMin, float tMax, HitRecord hit);
}
