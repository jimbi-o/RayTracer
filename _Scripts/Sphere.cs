using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sphere : Hitable {
	public Vector3 Center {
		get { return center_; }
		set { center_ = value; }
	}
	public float Radius {
		get { return radius_; }
		set { radius_ =  value; }
	}
	private Vector3 center_;
	private float radius_;
	public Sphere() {}
	public Sphere(Vector3 center, float r) {
		Center = center;
		Radius = r;
	}
	public override bool Hit(Ray ray, float tMin, float tMax, HitRecord hit) {
		Vector3 oc = ray.Origin - Center;
		float a = Vector3.Dot(ray.Direction, ray.Direction);
		float b = Vector3.Dot(oc, ray.Direction);
		float c = Vector3.Dot(oc, oc) - Radius * Radius;
		float discriminant = b * b - a * c;
		if (discriminant <= 0.0f) return false;
		discriminant = Mathf.Sqrt(discriminant);
		float t = (-b - discriminant) / a;
		bool ret = false;
		if (tMin < t && t < tMax) {
			ret = true;
			hit.t = t;
		}
		t = (-b + discriminant) / a;
		if (tMin < t && t < tMax) {
			ret = true;
			hit.t = t;
		}
		if (!ret) return false;
		hit.p = ray.PointAtParameter(hit.t);
		hit.normal = (hit.p - Center) / Radius;
		return true;
	}
}
