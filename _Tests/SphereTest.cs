using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assert = UnityEngine.Assertions.Assert;
public class SphereTest {
    [Test]
    public void SphereTestSimplePasses() {
		var ray = new Ray(new Vector3(0,0,-10), new Vector3(0,0,1));
		var hit = new HitRecord();
		var sphere = new Sphere(Vector3.zero, 1.0f);
		sphere.Hit(ray, hit);
		Assert.AreEqual(hit.t, 9.0f);
		Assert.AreEqual(hit.p, new Vector3(0,0,-1));
		Assert.AreEqual(hit.normal, new Vector3(0,0,-1));
    }
}
