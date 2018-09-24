using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boundary {
	private class CompareX : IComparer<Hitable> {
		public int Compare(Hitable a, Hitable b) {
			if (a.BoundingBox(0.0f, 1.0f).IsXSmaler(b.BoundingBox(0.0f, 1.0f))) return 1;
			return -1;
		}
	}
	private class CompareY : IComparer<Hitable> {
		public int Compare(Hitable a, Hitable b) {
			if (a.BoundingBox(0.0f, 1.0f).IsYSmaler(b.BoundingBox(0.0f, 1.0f))) return 1;
			return -1;
		}
	}
	private class CompareZ : IComparer<Hitable> {
		public int Compare(Hitable a, Hitable b) {
			if (a.BoundingBox(0.0f, 1.0f).IsZSmaler(b.BoundingBox(0.0f, 1.0f))) return 1;
			return -1;
		}
	}
	public static readonly IComparer<Hitable> compareX = new CompareX();
	public static readonly IComparer<Hitable> compareY = new CompareY();
	public static readonly IComparer<Hitable> compareZ = new CompareZ();
	public static Boundary SurroundingBoundary(Boundary a, Boundary b) {
		var boundary = new Boundary();
		boundary.box = Aabb.SurroundingBox(a.box, b.box);
		return boundary;
	}
	public Aabb box;
	public bool IsValid() {
		return box != null;
	}
	public bool IsXSmaler(Boundary other) {
		if (!IsValid() || !other.IsValid()) return false;
		return box.min.x < other.box.min.x;
	}
	public bool IsYSmaler(Boundary other) {
		if (!IsValid() || !other.IsValid()) return false;
		return box.min.y < other.box.min.y;
	}
	public bool IsZSmaler(Boundary other) {
		if (!IsValid() || !other.IsValid()) return false;
		return box.min.z < other.box.min.z;
	}
	public bool Hit(Ray ray, float tMin, float tMax) {
		if (box == null) return false;
		return box.Hit(ray, tMin, tMax);
	}
}
