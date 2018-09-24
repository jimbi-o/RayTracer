using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitableList : Hitable {
	private Hitable[] hitables_;
	public HitableList() {
		hitables_ = OneWeekScene();
	}
	private Hitable[] OneWeekScene() {
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
					// hitables.Add(new Sphere(position, radius, new Lambertian(new Vector3(Util.UnitRandFloat(), Util.UnitRandFloat(), Util.UnitRandFloat()))));
					hitables.Add(new MovingSphere(position, position + new Vector3(0.0f, 0.5f * Util.UnitRandFloat(), 0.0f), 0.0f, 1.0f, radius, new Lambertian(new Vector3(Util.UnitRandFloat(), Util.UnitRandFloat(), Util.UnitRandFloat()))));
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
		var retval = new Hitable[1];
		retval[0] = new BvhNode(hitables.ToArray(), 0, hitables.Count, 0.0f, 1.0f);
		return retval;
	}
	public override void Hit(Ray ray, HitRecord hit) {
		foreach (var hitable in hitables_) {
			hitable.Hit(ray, hit);
		}
	}
	public override Boundary BoundingBox(float t0, float t1) {
		var retval = hitables_[0].BoundingBox(t0, t1);
		if (!retval.IsValid()) return null;
		for (int i = 1; i < hitables_.Length; ++i) {
			var tmp = hitables_[i].BoundingBox(t0, t1);
			if (! tmp.IsValid()) return null;
			retval = Boundary.SurroundingBoundary(tmp, retval);
		}
		return retval;	
	}
}
