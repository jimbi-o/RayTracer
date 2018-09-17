using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRay {
	private Vector3 horizontal_, vertical_, origin_;
	private Vector3 lowerLeftCorner_;
	public CameraRay(Vector3 lookFrom, Vector3 lookAt, Vector3 vup, float fov, float aspect) {
		float theta = fov * Mathf.PI / 180.0f;
		float halfHeight = Mathf.Tan(theta / 2.0f);
		float halfWidth = aspect * halfHeight;
		origin_ = lookFrom;
		var w = (lookFrom - lookAt).normalized;
		var u = Vector3.Cross(vup, w).normalized;
		var v = Vector3.Cross(w, u);
		lowerLeftCorner_ = origin_ - halfWidth * u - halfHeight * v - w;
		horizontal_ = 2.0f * halfWidth * u;
		vertical_ = 2.0f * halfHeight * v;
	}
	public Ray GetRay(float u, float v) {
		var ray = new Ray(origin_, lowerLeftCorner_ + u * horizontal_ + v * vertical_ - origin_);
		return ray;
	}
}
