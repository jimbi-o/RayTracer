using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayTracer : MonoBehaviour {
	public int width, height;
	private Texture2D tex_;
	private Material material_;
	private RayTracingEngine rayTracingEngine_;
	private Scene scene_;
	void Start () {
		material_ = new Material(Shader.Find("Hidden/RayTracer"));
		tex_ = new Texture2D(width, height, TextureFormat.ARGB32, false);
		scene_ = new Scene();
		rayTracingEngine_ = new RayTracingEngine(width, height);
		rayTracingEngine_.Update(scene_);
	}
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		for (int x = 0; x < width; ++x) {
			for (int y = 0; y < height; ++y) {
				tex_.SetPixel(x, y, rayTracingEngine_.Pixel[x, y]);
			}
		}
		tex_.Apply();
		material_.SetTexture("_RTTex", tex_);
		Graphics.Blit(source, destination, material_);
	}
}
