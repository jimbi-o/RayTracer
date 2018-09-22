using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayTracingEngine {
	public Color[,] Pixel {
		get { return pixel_; }
	}
	private int maxSampleNum_;
	private int width_, height_;
	private Color[,] pixel_;
	private CameraRay cameraRay_;
	private int sampleCount_;
	private int x_, y_;
	public RayTracingEngine(int quality, int width, int height) {
		maxSampleNum_ = quality;
		width_ = width;
		height_ = height;
		CameraParam cameraParam = new CameraParam();
		cameraParam.lookFrom.Set(13.0f, 2.0f, 3.0f);
		cameraParam.lookAt.Set(0.0f, 0.0f, 0.0f);
		cameraParam.aspect = (float)width_ / (float)height_;
		cameraParam.fstop = 1.0f / 16.0f;
		cameraParam.focusDistance = 10.0f;
		cameraParam.fov = 20.0f;
		cameraRay_ = new CameraRay(cameraParam);
		pixel_ = new Color[width_, height_];
		for (int y = height_ - 1; y >= 0; --y) {
			for (int x = 0; x < width_; ++x) {
				pixel_[x, y] = Color.black;
			}
		}
		x_ = width_;
		y_ = 0;
	}
	public void Update(Scene scene) {
		if (sampleCount_ >= maxSampleNum_) return;
		++x_;
		if (x_ >= width_) {
			x_ = 0;
			--y_;
			if (y_ < 0) {
				y_ = height_ - 1;
				++sampleCount_;
			}
		}
		var alpha = 1.0f / sampleCount_;
		float u = ((float)x_ + Util.UnitRandFloat()) / (float)width_;
		float v = ((float)y_ + Util.UnitRandFloat()) / (float)height_;
		var color = Util.ConvertToColor(scene.GetColor(cameraRay_.GetRay(u, v)));
		pixel_[x_, y_] = Color.Lerp(pixel_[x_, y_], color, alpha);
	}
}
