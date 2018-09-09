using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRay {
	private int width_, height_;
	private Vector3 horizontal_, vertical_, origin_;
	private Vector3 lowerLeftCorner_;
	private Ray ray_;
	public CameraRay(int width, int height) {
		width_ = width;
		height_ = height;
		float ratio = (float)width / (float)height;
		lowerLeftCorner_ = new Vector3(-ratio, -1.0f, -1.0f);
		horizontal_ = new Vector3(2.0f * ratio, 0.0f, 0.0f);
		vertical_ = new Vector3(0.0f, 2.0f, 0.0f);
		origin_ = Vector3.zero;
		ray_ = new Ray();
		ray_.Origin = origin_;
	}
	public Ray GetRay(float u, float v) {
		ray_.Direction = lowerLeftCorner_ + u * horizontal_ + v * vertical_;
		return ray_;
	}
}
