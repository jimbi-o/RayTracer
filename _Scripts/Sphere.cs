using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sphere : Hitable {
	public MaterialData Material {
		get { return material_; }
		set { material_ = value; }
	}
	protected Vector3 center_;
	private float radius_;
	private MaterialData material_;
	public Sphere(Vector3 center, float r, MaterialData material) {
		center_ = center;
		radius_ = r;
		Material = material;
	}
	public override void Hit(Ray ray, HitRecord hit) {
		Vector3 oc = ray.Origin - Center(ray.Time);
		float a = Vector3.Dot(ray.Direction, ray.Direction);
		float b = Vector3.Dot(oc, ray.Direction);
		float c = Vector3.Dot(oc, oc) - radius_ * radius_;
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
	public virtual Vector3 Center(float time) {
		return center_;
	}
	void SetHitRecordVal(Ray ray, float t, HitRecord hit) {
		hit.t = t;
		hit.p = ray.PointAtParameter(hit.t);
		hit.normal = (hit.p - center_) / radius_;
		hit.material = Material;
	}
}
