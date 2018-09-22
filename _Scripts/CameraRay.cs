using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraParam {
	public Vector3 lookFrom;
	public Vector3 lookAt;
	public Vector3 vup;
	public float fov;
	public float aspect;
	public float fstop;
	public CameraParam() {
		vup.Set(0.0f, 1.0f, 0.0f);
		fov = 45.0f;
		fstop = 1.0f / 4.0f;
	}
}
public class CameraRay {
	private Vector3 u_, v_, w_;
	private Vector3 horizontal_, vertical_, origin_;
	private Vector3 lowerLeftCorner_;
	private float lensRadius_;
	public CameraRay(CameraParam param) {
		lensRadius_ = 0.5f / param.fstop;
		var theta = param.fov * Mathf.PI / 180.0f;
		var halfHeight = Mathf.Tan(theta / 2.0f);
		var halfWidth = param.aspect * halfHeight;
		origin_ = param.lookFrom;
		w_ = (param.lookFrom - param.lookAt).normalized;
		u_ = Vector3.Cross(param.vup, w_).normalized;
		v_ = Vector3.Cross(w_, u_);
		var focusDistance = (param.lookFrom - param.lookAt).magnitude;
		lowerLeftCorner_ = origin_ - focusDistance * (halfWidth * u_ + halfHeight * v_ + w_);
		horizontal_ = 2.0f * halfWidth * u_ * focusDistance;
		vertical_ = 2.0f * halfHeight * v_ * focusDistance;
	}
	public Ray GetRay(float s, float t) {
		var rd = lensRadius_ * Util.RandomInUnitSphere();
		var offset = u_ * rd.x + v_ * rd.y;
		var ray = new Ray(origin_ + offset, lowerLeftCorner_ + s * horizontal_ + t * vertical_ - origin_ - offset);
		return ray;
	}
}
