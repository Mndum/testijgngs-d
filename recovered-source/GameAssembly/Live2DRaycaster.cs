using System.Collections.Generic;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Raycasting;
using Live2D.Cubism.Rendering;
using UnityEngine;

public static class Live2DRaycaster
{
	public struct HitTriangleData(int vertexAIndex, int vertexBIndex, int vertexCIndex, float aWeight, float bWeight, float cWeight)
	{
		public readonly int VertexAIndex = vertexAIndex;

		public readonly int VertexBIndex = vertexBIndex;

		public readonly int VertexCIndex = vertexCIndex;

		public readonly float AWeight = aWeight;

		public readonly float BWeight = bWeight;

		public readonly float CWeight = cWeight;

		public Vector3 GetPosition(Vector3[] vertices)
		{
			return AWeight * vertices[VertexAIndex] + BWeight * vertices[VertexBIndex] + CWeight * vertices[VertexCIndex];
		}
	}

	public static void RaycastQuickBoundingBox(this CubismModel model, IEnumerable<CubismRenderer> targets, Ray ray, int countLimit, ref List<CubismRenderer> outputList)
	{
		CalculateIntersection(model, ray, out var intersectionInLocalSpace, out var _);
		outputList = outputList.ClearOrCreate();
		foreach (CubismRenderer target in targets)
		{
			if (target.MeshRenderer.enabled && !target.MeshRenderer.forceRenderingOff && target.Mesh.bounds.Contains(intersectionInLocalSpace))
			{
				outputList.Add(target);
				if (outputList.Count >= countLimit)
				{
					break;
				}
			}
		}
	}

	public static void RaycastNarrowDownTriangles(this CubismModel model, Ray ray, ref List<CubismRenderer> outputList)
	{
		CalculateIntersection(model, ray, out var intersectionInLocalSpace, out var _);
		outputList.RemoveAll((CubismRenderer renderer) => (!renderer.MeshRenderer.enabled || renderer.MeshRenderer.forceRenderingOff || !ContainsInTriangles(renderer.Mesh, intersectionInLocalSpace)) ? true : false);
	}

	public static void RaycastQuickTriangles(this CubismModel model, IEnumerable<CubismRenderer> targets, Ray ray, int countLimit, List<CubismRenderer> outputList)
	{
		CalculateIntersection(model, ray, out var intersectionInLocalSpace, out var _);
		outputList = outputList.ClearOrCreate();
		foreach (CubismRenderer target in targets)
		{
			if (target.MeshRenderer.enabled && !target.MeshRenderer.forceRenderingOff && ContainsInTriangles(target.Mesh, intersectionInLocalSpace))
			{
				outputList.Add(target);
				if (outputList.Count >= countLimit)
				{
					break;
				}
			}
		}
	}

	public static bool RaycastSingle(this CubismModel model, Ray ray, CubismRenderer renderer)
	{
		if (renderer == null)
		{
			return false;
		}
		if (!renderer.MeshRenderer.enabled || renderer.MeshRenderer.forceRenderingOff)
		{
			return false;
		}
		CalculateIntersection(model, ray, out var intersectionInLocalSpace, out var _);
		if (!renderer.Mesh.bounds.Contains(intersectionInLocalSpace))
		{
			return false;
		}
		return ContainsInTriangles(renderer.Mesh, intersectionInLocalSpace);
	}

	public static void Raycast(this CubismModel model, List<CubismRaycastable> targets, Ray ray, int countLimit, ref List<CubismRaycastHit> output, float maximumDistance = 1f / 0f)
	{
		CalculateIntersection(model, ray, out var intersectionInLocalSpace, out var intersectionInWorldSpace);
		float magnitude = intersectionInWorldSpace.magnitude;
		output = output.ClearOrCreate();
		if (magnitude > maximumDistance)
		{
			return;
		}
		int num = 0;
		foreach (CubismRaycastable target in targets)
		{
			CubismRenderer component = target.GetComponent<CubismRenderer>();
			CubismRaycastablePrecision precision = target.Precision;
			if (component.MeshRenderer.enabled && !component.MeshRenderer.forceRenderingOff && component.Mesh.bounds.Contains(intersectionInLocalSpace) && (precision != CubismRaycastablePrecision.Triangles || ContainsInTriangles(component.Mesh, intersectionInLocalSpace)))
			{
				output.Add(new CubismRaycastHit
				{
					Drawable = component.GetComponent<CubismDrawable>(),
					Distance = magnitude,
					LocalPosition = intersectionInLocalSpace,
					WorldPosition = intersectionInWorldSpace
				});
				num++;
				if (num >= countLimit)
				{
					break;
				}
			}
		}
	}

	public static void CalculateIntersection(CubismModel model, Ray ray, out Vector3 intersectionInLocalSpace, out Vector3 intersectionInWorldSpace)
	{
		intersectionInWorldSpace = ray.origin + ray.direction * (ray.direction.z / ray.origin.z);
		intersectionInLocalSpace = model.transform.InverseTransformPoint(intersectionInWorldSpace);
		intersectionInLocalSpace.z = 0f;
	}

	public static bool TryGetUVCoordinates(Mesh mesh, Vector3 intersectionInLocalSpace, out Vector2 uv)
	{
		HitTriangleData hitTriangleData;
		return TryGetUVCoordinates(mesh, intersectionInLocalSpace, out uv, out hitTriangleData);
	}

	public static bool TryGetUVCoordinates(Mesh mesh, Vector3 intersectionInLocalSpace, out Vector2 uv, out HitTriangleData hitTriangleData)
	{
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv2 = mesh.uv;
		uv = Vector2.zero;
		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 vector = vertices[triangles[i]];
			Vector3 vector2 = vertices[triangles[i + 1]];
			Vector3 vector3 = vertices[triangles[i + 2]];
			float num = (vector2.x - vector.x) * (intersectionInLocalSpace.y - vector2.y) - (vector2.y - vector.y) * (intersectionInLocalSpace.x - vector2.x);
			float num2 = (vector3.x - vector2.x) * (intersectionInLocalSpace.y - vector3.y) - (vector3.y - vector2.y) * (intersectionInLocalSpace.x - vector3.x);
			float num3 = (vector.x - vector3.x) * (intersectionInLocalSpace.y - vector.y) - (vector.y - vector3.y) * (intersectionInLocalSpace.x - vector.x);
			if ((num >= 0f && num2 >= 0f && num3 >= 0f) || (num <= 0f && num2 <= 0f && num3 <= 0f))
			{
				float magnitude = Vector3.Cross(vector2 - vector, vector3 - vector).magnitude;
				float magnitude2 = Vector3.Cross(vector2 - intersectionInLocalSpace, vector3 - intersectionInLocalSpace).magnitude;
				float magnitude3 = Vector3.Cross(vector3 - intersectionInLocalSpace, vector - intersectionInLocalSpace).magnitude;
				float num4 = magnitude2 / magnitude;
				float num5 = magnitude3 / magnitude;
				float num6 = 1f - num4 - num5;
				Vector2 vector4 = uv2[triangles[i]];
				Vector2 vector5 = uv2[triangles[i + 1]];
				Vector2 vector6 = uv2[triangles[i + 2]];
				uv = vector4 * num4 + vector5 * num5 + vector6 * num6;
				hitTriangleData = new HitTriangleData(triangles[i], triangles[i + 1], triangles[i + 2], num4, num5, num6);
				return true;
			}
		}
		hitTriangleData = default;
		return false;
	}

	private static bool ContainsInTriangles(Mesh mesh, Vector3 intersectionInLocalSpace)
	{
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 vector = vertices[triangles[i]];
			Vector3 vector2 = vertices[triangles[i + 1]];
			Vector3 vector3 = vertices[triangles[i + 2]];
			float num = (vector2.x - vector.x) * (intersectionInLocalSpace.y - vector2.y) - (vector2.y - vector.y) * (intersectionInLocalSpace.x - vector2.x);
			float num2 = (vector3.x - vector2.x) * (intersectionInLocalSpace.y - vector3.y) - (vector3.y - vector2.y) * (intersectionInLocalSpace.x - vector3.x);
			float num3 = (vector.x - vector3.x) * (intersectionInLocalSpace.y - vector.y) - (vector.y - vector3.y) * (intersectionInLocalSpace.x - vector.x);
			if ((num > 0f && num2 > 0f && num3 > 0f) || (num < 0f && num2 < 0f && num3 < 0f))
			{
				return true;
			}
		}
		return false;
	}
}
