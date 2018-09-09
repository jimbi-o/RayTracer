using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ray {
	public Vector3 Origin {
		get { return origin_; }
		set { origin_ = value; origin_.Normalize(); }
	}
	public Vector3 Direction {
		get { return direction_; }
		set { direction_ = value; direction_.Normalize(); }
	}
	private Vector3 origin_, direction_;
	public Ray() {}
	public Ray(Vector3 origin, Vector3 direction) {
		Origin = origin;
		Direction = direction;
	}
	public Vector3 PointAtParameter(float t) {
		return origin_ + t * direction_;
	}
}
