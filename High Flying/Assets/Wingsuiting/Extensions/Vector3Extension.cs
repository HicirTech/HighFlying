using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
	public static Vector3 ToLocalPosition(this Vector3 wordPosition, Transform parent)
	{
		Vector3 localPosition = wordPosition - parent.position;
		return new Vector3( Vector3.Dot(localPosition, parent.right),
			Vector3.Dot(localPosition, parent.up),
			Vector3.Dot(localPosition, parent.forward)
		);
	}
	public static Vector3 ToLocalPosition(this Vector3 wordPosition, Vector3 parentPosition, Transform parent)
	{
		Vector3 localPosition = wordPosition - parentPosition;
		return new Vector3( Vector3.Dot(localPosition, parent.right),
			Vector3.Dot(localPosition,parent.up),
			Vector3.Dot(localPosition, parent.forward)
		);
	}

	public static Vector3 ToWordPosition(this Vector3 localPosition, Transform parent)
	{
		return parent.position + localPosition.x*parent.right + localPosition.y*parent.up + localPosition.z*parent.forward;
	}
	public static bool SphereInCube (this Vector3 point, float radius,  Transform cube)
	{
		Vector3 localPos = point.ToLocalPosition(cube);
		Vector3 cubeSqale = cube.lossyScale;
		return Mathf.Abs(localPos.x) + radius <= 0.5f*cubeSqale.x && Mathf.Abs(localPos.y) + radius <= 0.5f*cubeSqale.y && Mathf.Abs(localPos.z) + radius <= 0.5f*cubeSqale.z;
	}
	public static bool SphereHitCone(this Vector3 spherPosition, float sphereRadius, Vector3 conePosition, Vector3 coneForward, float coneAngleRadian, float coneH)
	{
		Vector3 r = spherPosition - conePosition;
		Vector3 p1 = Vector3.Project(r, coneForward);
		if (p1.magnitude - sphereRadius > coneH || (Vector3.Dot(p1, coneForward) <= 0.0f && p1.magnitude > sphereRadius))
		{
			return false;
		}
		float distance1 = p1.magnitude * Mathf.Tan(0.5f * coneAngleRadian);
		float distance2 = sphereRadius / Mathf.Cos(0.5f * coneAngleRadian);
		Vector3 p2 = Vector3.ProjectOnPlane(r, coneForward);
		return p2.magnitude <= distance1 + distance2;
	}
}
