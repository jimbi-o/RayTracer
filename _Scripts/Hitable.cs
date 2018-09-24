using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Hitable {
	public abstract HitRecord Hit(Ray ray, HitRecord hit);
	public abstract Boundary BoundingBox(float t0, float t1);
}
