using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scene {
	private Hitable[] hitables_;
	private Vector3 skyColor_;
	private int maxDepth_;
	public Scene(int maxDepth) {
		maxDepth_ = maxDepth;
		skyColor_ = new Vector3(0.5f, 0.7f, 1.0f);
		var center = new Vector3(4.0f, 2.0f, 0.0f);
		var position = new Vector3(0.0f, -1000.0f, 0.0f);
		var hitables = new List<Hitable>(500);
		hitables.Add(new Sphere(position, -position.y, new Lambertian(0.5f * Vector3.one)));
		float radius = 0.2f;
		var dielectric = new Dielectric(1.5f);
		for (int a = -11; a < 11; ++a) {
			for (int b = -11; b < 11; ++b) {
				position.Set(a + 0.9f * Util.UnitRandFloat(), radius, b + 0.9f * Util.UnitRandFloat());
				if ((position - center).sqrMagnitude <= 0.81) continue;
				var chooseMat = Util.UnitRandFloat();
				if (chooseMat < 0.8f) {
					hitables.Add(new Sphere(position, radius, new Lambertian(new Vector3(Util.UnitRandFloat(), Util.UnitRandFloat(), Util.UnitRandFloat()))));
					continue;
				}
				if (chooseMat < 0.95f) {
					hitables.Add(new Sphere(position, radius,
											new Metal(new Vector3(0.5f * (1.0f + Util.UnitRandFloat()), 0.5f * (1.0f + Util.UnitRandFloat()), 0.5f * (1.0f + Util.UnitRandFloat())),
													  0.5f * (1.0f + Util.UnitRandFloat())))) ;
																		
					continue;
				}
				hitables.Add(new Sphere(position, radius, dielectric));
			}
		}
		radius = 1.0f;
		position.Set(0.0f, 1.0f, 0.0f);
		hitables.Add(new Sphere(position, radius, dielectric));
		position.Set(-4.0f, 1.0f, 0.0f);
		hitables.Add(new Sphere(position, radius, new Lambertian(new Vector3(0.4f, 0.2f, 0.1f))));
		position.Set(4.0f, 1.0f, 0.0f);
		hitables.Add(new Sphere(position, radius, new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0.0f)));
		hitables_ = hitables.ToArray();
	}
	public Vector3 GetColor(Ray ray) {
		var attenuation = Vector3.one;
		for (int i = 0; i < maxDepth_; ++i) {
			var hit = GetHitRecord(ray);
			if (!hit.IsHit()) {
				attenuation.Scale(GetBGColor(ray));
				break;
			}
			if (!hit.material.Scatter(ray, hit)) {
				return Vector3.zero;
			}
			attenuation.Scale(hit.attenuation);
			ray = hit.scatteredRay;
		}
		return attenuation;
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
