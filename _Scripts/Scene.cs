using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scene {
	private Hitable[] hitables_;
	private Vector3 skyColor_;
	private int maxDepth_;
	public Scene(int maxDepth) {
		hitables_ = new Hitable[4];
		hitables_[0] = new Sphere(new Vector3(0.0f, 0.0f, -1.0f), 0.5f, new Lambertian(new Vector3(0.8f, 0.3f, 0.3f)));
		hitables_[1] = new Sphere(new Vector3(0.0f, -100.5f, -1.0f), 100.0f, new Lambertian(new Vector3(0.8f, 0.8f, 0.0f)));
		hitables_[2] = new Sphere(new Vector3(1.0f, 0.0f, -1.0f), 0.5f, new Metal(new Vector3(0.8f, 0.6f, 0.2f)));
		hitables_[3] = new Sphere(new Vector3(-1.0f, 0.0f, -1.0f), 0.5f, new Metal(new Vector3(0.8f, 0.8f, 0.8f)));
		skyColor_ = new Vector3(0.5f, 0.7f, 1.0f);
		maxDepth_ = maxDepth;
	}
	public Vector3 GetColor(Ray ray, int depth) {
		var hit = GetHitRecord(ray);
		if (!hit.IsHit()) return GetBGColor(ray);
		if (depth >= maxDepth_ || !hit.material.Scatter(ray, hit)) return Vector3.zero;
		hit.attenuation.Scale(GetColor(hit.scatteredRay, depth + 1));
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
