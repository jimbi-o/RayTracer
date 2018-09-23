using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingSphere : Sphere {
	private Vector3 center1_;
	private float time0_, time1_;
	public MovingSphere(Vector3 center0, Vector3 center1, float t0, float t1, float r, MaterialData material) : base(center0, r, material) {
		center1_ = center1;
		time0_ = t0;
		time1_ = t1;
	}
	public override Vector3 Center(float time) {
		return center_ + ((time - time0_) / (time1_ - time0_)) * (center1_ - center_);
	}
}
