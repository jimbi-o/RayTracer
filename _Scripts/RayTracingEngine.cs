using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayTracingEngine {
	public Color[,] Pixel {
		get { return pixel_; }
	}
	private Color[,] pixel_;
	private Vector3 horizontal_, vertical_, origin_;
	private Vector3 lowerLeftCorner_;
	private int width_, height_;
	public RayTracingEngine(int width, int height) {
		float ratio = (float)width / (float)height;
		lowerLeftCorner_ = new Vector3(-ratio, -1.0f, -1.0f);
		horizontal_ = new Vector3(2.0f * ratio, 0.0f, 0.0f);
		vertical_ = new Vector3(0.0f, 2.0f, 0.0f);
		origin_ = Vector3.zero;
		width_ = width;
		height_ = height;
		pixel_ = new Color[width_, height_];
	}
	public void Update(Scene scene) {
		Ray ray = new Ray();
		ray.Origin = origin_;
		HitRecord hit = new HitRecord();
		for (int y = height_ - 1; y >= 0; --y) {
			for (int x = 0; x < width_; ++x) {
				float u = (float)x / (float)width_;
				float v = (float)y / (float)height_;
				ray.Direction = lowerLeftCorner_ + u * horizontal_ + v * vertical_;
				pixel_[x, y] = scene.GetColor(ray, hit);
			}
		}
	}
}
