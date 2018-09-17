using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Metal : MaterialData {
	public Vector3 Albedo {
		get { return albedo_; }
		set { albedo_ = value; }
	}
	private Vector3 albedo_;
	public Metal(Vector3 a) {
		Albedo = a;
	}
	public override bool Scatter(Ray ray, HitRecord hit) {
		hit.scatteredRay = new Ray(hit.p, Vector3.Reflect(ray.Direction, hit.normal));
		hit.attenuation = Albedo;
		return Vector3.Dot(hit.scatteredRay.Direction, hit.normal) > 0.0f;
	}
}
