using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scene {
	private Hitable[] hitables_;
	private Color skyColor_;
	public Scene() {
		hitables_ = new Hitable[2];
		hitables_[0] = new Sphere(new Vector3(0.0f, 0.0f, -1.0f), 0.5f);
		hitables_[1] = new Sphere(new Vector3(0.0f, -100.5f, -1.0f), 100.0f);
		skyColor_ = new Color(0.5f, 0.7f, 1.0f, 1.0f);
	}
	public Color GetColor(Ray ray) {
		var hit = GetHitRecord(ray);
		if (hit.IsHit()) {
			var target = hit.normal + Util.RandomInUnitSphere();
			return 0.5f * GetColor(new Ray(hit.p, target));
		}
		return GetBGColor(ray);
	}
	public HitRecord GetHitRecord(Ray ray) {
		var hit = new HitRecord();
		foreach (var hitable in hitables_) {
			hitable.Hit(ray, hit);
		}
		return hit;
	}
	private Color GetBGColor(Ray ray) {
		float t = 0.5f * (ray.Direction.y + 1.0f);
		return Color.Lerp(Color.white, skyColor_, t);
	}
}
