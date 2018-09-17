using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayTracingEngine {
	public Color[,] Pixel {
		get { return pixel_; }
	}
	private int aaQuality_;
	private int width_, height_;
	private Color[,] pixel_;
	private CameraRay cameraRay_;
	public RayTracingEngine(int quality, int width, int height) {
		aaQuality_ = quality;
		width_ = width;
		height_ = height;
		cameraRay_ = new CameraRay(width_, height_);
		pixel_ = new Color[width_, height_];
	}
	public void Update(Scene scene) {
		for (int y = height_ - 1; y >= 0; --y) {
			for (int x = 0; x < width_; ++x) {
				Color color = Vector4.zero;
				for (int s = 0; s < aaQuality_; ++s) {
					float u = ((float)x + Util.UnitRandFloat()) / (float)width_;
					float v = ((float)y + Util.UnitRandFloat()) / (float)height_;
					color += scene.GetColor(cameraRay_.GetRay(u, v));
				}
				pixel_[x, y] = color / aaQuality_;
			}
		}
	}
}
