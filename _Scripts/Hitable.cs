using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Hitable {
	public abstract void Hit(Ray ray, HitRecord hit);
}
