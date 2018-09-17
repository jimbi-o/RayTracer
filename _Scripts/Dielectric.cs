using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dielectric : MaterialData {
	public float ReflectionIndex {
		get { return reflectionIndex_; }
		set { reflectionIndex_ = value; }
	}
	private float reflectionIndex_;
	public Dielectric(float reflectionIndex) {
		ReflectionIndex = reflectionIndex;
	}
	public override bool Scatter(Ray ray, HitRecord hit) {
		hit.attenuation = Vector3.one;
		hit.scatteredRay = new Ray(hit.p, GetScatteredRay(ray, hit));
		return true;
	}
	public Vector3 GetScatteredRay(Ray ray, HitRecord hit) {
		Vector3 scattered = Vector3.zero;
		float cosineR = ReflectionIndex;
		if (Vector3.Dot(ray.Direction, hit.normal) > 0.0f) {
			scattered = Util.GetRefractedRay(ray.Direction, -hit.normal, ReflectionIndex);
		} else {
			scattered = Util.GetRefractedRay(ray.Direction, hit.normal, 1.0f / ReflectionIndex);
			cosineR =-1.0f;
		}
		if (scattered != Vector3.zero) {
			float schlick = Util.GetSchlick(cosineR * Vector3.Dot(ray.Direction, hit.normal), ReflectionIndex);
			if (schlick <= Util.UnitRandFloat()) {
				return scattered;
			}
		}
		return Vector3.Reflect(ray.Direction, hit.normal);
	}
}
