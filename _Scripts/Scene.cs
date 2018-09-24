using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scene {
	private HitableList hitableList_;
	private Vector3 skyColor_;
	private int maxDepth_;
	public Scene(int maxDepth) {
		maxDepth_ = maxDepth;
		skyColor_ = new Vector3(0.5f, 0.7f, 1.0f);
		hitableList_ = new HitableList();
	}
	public Vector3 GetColor(Ray ray) {
		var attenuation = Vector3.one;
		for (int i = 0; i < maxDepth_; ++i) {
			var hit = hitableList_.Hit(ray, new HitRecord());
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
	private Vector3 GetBGColor(Ray ray) {
		float t = 0.5f * (ray.Direction.y + 1.0f);
		return Vector3.Lerp(Vector3.one, skyColor_, t);
	}
}
