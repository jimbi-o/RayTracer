using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Metal : MaterialData {
	public Vector3 Albedo {
		get { return albedo_; }
		set { albedo_ = value; }
	}
	public float Fuzz {
		get { return fuzz_; }
		set { fuzz_ = Mathf.Min(value, 1.0f); }
	}
	private Vector3 albedo_;
	private float fuzz_;
	public Metal(Vector3 a, float fuzz) {
		Albedo = a;
		Fuzz = fuzz;
	}
	public override bool Scatter(Ray ray, HitRecord hit) {
		hit.scatteredRay = new Ray(hit.p, Vector3.Reflect(ray.Direction, hit.normal) + Fuzz * Util.RandomInUnitSphere());
		hit.attenuation = Albedo;
		return Vector3.Dot(hit.scatteredRay.Direction, hit.normal) > 0.0f;
	}
}
