﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayTracer : MonoBehaviour {
	private int width_, height_;
	private Texture2D tex_;
	private Material material_;
	private RayTracingEngine rayTracingEngine_;
	private Scene scene_;
	void Start () {
		width_ = Screen.width / 8;
		height_ = Screen.height / 8;
		material_ = new Material(Shader.Find("Hidden/RayTracer"));
		tex_ = new Texture2D(width_, height_, TextureFormat.ARGB32, false, true);
		scene_ = new Scene();
		rayTracingEngine_ = new RayTracingEngine(width_, height_);
		rayTracingEngine_.Update(scene_);
	}
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		for (int x = 0; x < width_; ++x) {
			for (int y = 0; y < height_; ++y) {
				tex_.SetPixel(x, y, rayTracingEngine_.Pixel[x, y]);
			}
		}
		tex_.Apply();
		material_.SetTexture("_RTTex", tex_);
		Graphics.Blit(source, destination, material_);
	}
}
