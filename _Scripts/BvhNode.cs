using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BvhNode : Hitable {
	private Hitable left_, right_;
	private Boundary boundary_;
	public BvhNode(Hitable[] hitables, int start, int length, float time0, float time1) {
		// TODO recursive -> iteration loop
		IComparer<Hitable> comparer = null;
		switch ((int)(3.0f * Util.UnitRandFloat())) {
			case 0:
				comparer = Boundary.compareX;
				break;
			case 1:
				comparer = Boundary.compareY;
				break;
			case 2:
			default:
				comparer = Boundary.compareZ;
				break;
		}
		System.Array.Sort(hitables, start, length, comparer);
		var halfLength = length / 2;
		switch (length) {
			case 1:
				left_ = hitables[start];
				right_ = hitables[start];
				break;
			case 2:
				left_ = hitables[start];
				right_ = hitables[start + 1];
				break;
			default:
				left_ = new BvhNode(hitables, start, halfLength, time0, time1);
				right_ = new BvhNode(hitables, start + halfLength, length - halfLength, time0, time1);
				break;
		}
		var boxLeft = left_.BoundingBox(time0, time1);
		if (!boxLeft.IsValid()) {
			Debug.Log("no bounding box detected.");
		}
		var boxRight = right_.BoundingBox(time0, time1);
		if (!boxRight.IsValid()) {
			Debug.Log("no bounding box detected.");
		}
		boundary_ = Boundary.SurroundingBoundary(boxLeft, boxRight);
	}
	public override void Hit(Ray ray, HitRecord hit) {
		if (!boundary_.Hit(ray, HitRecord.tMin, hit.t)) return;
		left_.Hit(ray, hit);
		right_.Hit(ray, hit);
	}
	public override Boundary BoundingBox(float t0, float t1) {
		return boundary_;
	}
}
