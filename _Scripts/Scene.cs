using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scene {
	private Hitable[] hitables_;
	private Vector3 skyColor_;
	public Scene() {
		var lambertian = new Lambertian(new Vector3(0.5f, 0.5f, 0.5f));
		hitables_ = new Hitable[2];
		hitables_[0] = new Sphere(new Vector3(0.0f, 0.0f, -1.0f), 0.5f, lambertian);
		hitables_[1] = new Sphere(new Vector3(0.0f, -100.5f, -1.0f), 100.0f, lambertian);
		skyColor_ = new Vector3(0.5f, 0.7f, 1.0f);
	}
	public Color GetColor(Ray ray) {
		var v = GetColorVector(ray);
		return new Color(v.x, v.y, v.z, 1.0f);
	}
	public Vector3 GetColorVector(Ray ray) {
		var hit = GetHitRecord(ray);
		if (!hit.IsHit()) return GetBGColor(ray);
		if (!hit.material.Scatter(ray, hit)) return Vector3.zero;
		hit.attenuation.Scale(GetColorVector(hit.scatteredRay));
		return hit.attenuation;
	}
	public HitRecord GetHitRecord(Ray ray) {
		var hit = new HitRecord();
		foreach (var hitable in hitables_) {
			hitable.Hit(ray, hit);
		}
		return hit;
	}
	private Vector3 GetBGColor(Ray ray) {
		float t = 0.5f * (ray.Direction.y + 1.0f);
		return Vector3.Lerp(Vector3.one, skyColor_, t);
	}
}
