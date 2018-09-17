using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Lambertian : MaterialData {
	public Vector3 Albedo {
		get { return albedo_; }
		set { albedo_ = value; }
	}
	private Vector3 albedo_;
	public Lambertian(Vector3 a) {
		Albedo = a;
	}
	public override bool Scatter(Ray ray, HitRecord hit) {
		hit.scatteredRay = new Ray(hit.p, hit.normal + Util.RandomInUnitSphere());
		hit.attenuation = Albedo;
		return true;
	}
}
