using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ray {
	public Vector3 Origin {
		get { return origin_; }
		set { origin_ = value; }
	}
	public Vector3 Direction {
		get { return direction_; }
		set { direction_ = value.normalized; }
	}
	public float Time {
		get { return time_; }
		set { time_ = value; }
	}
	private Vector3 origin_, direction_;
	private float time_;
	public Ray(Vector3 origin, Vector3 direction, float time = 0.0f) {
		Origin = origin;
		Direction = direction;
		Time = time;
	}
	public Vector3 PointAtParameter(float t) {
		return Origin + t * Direction;
	}
}
