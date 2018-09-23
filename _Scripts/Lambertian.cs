using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Lambertian : MaterialData {
	public Vector3 albedo;
	public Lambertian(Vector3 a) {
		albedo = a;
	}
	public override bool Scatter(Ray ray, HitRecord hit) {
		hit.scatteredRay = new Ray(hit.p, hit.normal + Util.RandomInUnitSphere(), ray.Time);
		hit.attenuation = albedo;
		return true;
	}
}
