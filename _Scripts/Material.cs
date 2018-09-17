using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class MaterialData {
	public abstract bool Scatter(Ray ray, HitRecord hit);
}

