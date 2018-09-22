using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraParam {
	public Vector3 lookFrom;
	public Vector3 lookAt;
	public Vector3 vup;
	public float fov;
	public float aspect;
	public CameraParam() {
		vup.Set(0.0f, 1.0f, 0.0f);
		fov = 45.0f;
	}
}
public class CameraRay {
	private Vector3 horizontal_, vertical_, origin_;
	private Vector3 lowerLeftCorner_;
	public CameraRay(CameraParam param) {
		float theta = param.fov * Mathf.PI / 180.0f;
		float halfHeight = Mathf.Tan(theta / 2.0f);
		float halfWidth = param.aspect * halfHeight;
		origin_ = param.lookFrom;
		var w = (param.lookFrom - param.lookAt).normalized;
		var u = Vector3.Cross(param.vup, w).normalized;
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
