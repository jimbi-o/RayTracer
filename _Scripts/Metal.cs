using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Metal : MaterialData {
	public Vector3 albedo;
	public float fuzz;
	public Metal(Vector3 a, float f) {
		albedo = a;
		fuzz = f;
	}
	public override bool Scatter(Ray ray, HitRecord hit) {
		hit.scatteredRay = new Ray(hit.p, Vector3.Reflect(ray.Direction, hit.normal) + fuzz * Util.RandomInUnitSphere(), ray.Time);
		hit.attenuation = albedo;
		return Vector3.Dot(hit.scatteredRay.Direction, hit.normal) > 0.0f;
	}
}
