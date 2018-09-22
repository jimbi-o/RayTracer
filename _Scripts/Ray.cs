using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ray {
	public static Ray Clone(Ray r) {
		var ray = new Ray(r.Origin, r.Direction);
		return ray;
	}
	public Vector3 Origin {
		get { return origin_; }
		set { origin_ = value; }
	}
	public Vector3 Direction {
		get { return direction_; }
		set { direction_ = value.normalized; }
	}
	private Vector3 origin_, direction_;
	public Ray(Vector3 origin, Vector3 direction) {
		Origin = origin;
		Direction = direction;
	}
	public Vector3 PointAtParameter(float t) {
		return Origin + t * Direction;
	}
}
