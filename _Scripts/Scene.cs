using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scene {
	private Hitable[] hitables_;
	public Scene() {
		hitables_ = new Hitable[2];
		hitables_[0] = new Sphere(new Vector3(0.0f, 0.0f, -1.0f), 0.5f);
		hitables_[1] = new Sphere(new Vector3(0.0f, -100.5f, -1.0f), 100.0f);
	}
	public Color GetColor(Ray ray, HitRecord hit) {
		foreach (var hitable in hitables_) {
			if (hitable.Hit(ray, 0.0f, float.MaxValue, hit)) {
				Vector3 val = 0.5f * (hit.normal + Vector3.one);
				return new Color(val.x, val.y, val.z, 1.0f);
			}
		}
		return GetBGColor(ray);
	}
	private Color GetBGColor(Ray ray) {
		float t = 0.5f * (ray.Direction.y + 1.0f);
		return Color.Lerp(Color.white, new Color(0.5f, 0.7f, 1.0f, 1.0f), t);
	}
}
