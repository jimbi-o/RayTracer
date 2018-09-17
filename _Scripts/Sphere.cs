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
	public MaterialData Material {
		get { return material_; }
		set { material_ = value; }
	}
	private Vector3 center_;
	private float radius_;
	private MaterialData material_;
	public Sphere(Vector3 center, float r, MaterialData material) {
		Center = center;
		Radius = r;
		Material = material;
	}
	public override void Hit(Ray ray, HitRecord hit) {
		Vector3 oc = ray.Origin - Center;
		float a = Vector3.Dot(ray.Direction, ray.Direction);
		float b = Vector3.Dot(oc, ray.Direction);
		float c = Vector3.Dot(oc, oc) - Radius * Radius;
		float discriminant = b * b - a * c;
		if (discriminant <= 0.0f) return;
		discriminant = Mathf.Sqrt(discriminant);
		float t = (-b - discriminant) / a;
		if (hit.SetT(t)) {
			SetHitRecordVal(ray, t, hit);
			return;
		}
		t = (-b + discriminant) / a;
		if (hit.SetT(t)) {
			SetHitRecordVal(ray, t, hit);
		}
	}
	void SetHitRecordVal(Ray ray, float t, HitRecord hit) {
		hit.t = t;
		hit.p = ray.PointAtParameter(hit.t);
		hit.normal = (hit.p - Center) / Radius;
		hit.material = Material;
	}
}
