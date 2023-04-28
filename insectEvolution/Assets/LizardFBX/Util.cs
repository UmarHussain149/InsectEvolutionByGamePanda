using System.Collections;
using UnityEngine;


public static class Util
{
	//Vector3
	public static Vector3 SetX(this Vector3 vec, float x)
	{
		return new Vector3(x, vec.y, vec.z);
	}

	public static Vector3 SetY(this Vector3 vec, float y)
	{
		return new Vector3(vec.x, y, vec.z);
	}

	public static Vector3 SetZ(this Vector3 vec, float z)
	{
		return new Vector3(vec.x, vec.y, z);
	}

	public static Vector3 Multiply(this Vector3 vec, float x, float y, float z)
	{
		return new Vector3(vec.x * x, vec.y * y, vec.z * z);
	}

	public static Vector3 Multiply(this Vector3 vec, Vector3 other)
	{
		return Multiply(vec, other.x, other.y, other.z);
	}

	public static Vector3 ClampX(this Vector3 vec, float min, float max)
	{
		vec = new Vector3(Mathf.Clamp(vec.x, min, max), vec.y, vec.z);
		return vec;
	}
	
	public static Vector3 ClampY(this Vector3 vec, float min, float max)
	{
		vec = new Vector3(vec.x, Mathf.Clamp(vec.y, min, max), vec.z);
		return vec;
	}
	public static Vector3 ClampZ(this Vector3 vec, float min, float max)
	{
		vec = new Vector3(vec.x, vec.y, Mathf.Clamp(vec.z, min, max));
		return vec;
	}
	public static Vector3 Clamp(this Vector3 vec, Vector3 min, Vector3 max)
	{
		vec.x = Mathf.Clamp(vec.x, min.x, max.x);
		vec.y = Mathf.Clamp(vec.y, min.y, max.y);
		vec.z = Mathf.Clamp(vec.z, min.z, max.z);
		return vec;
	}
	
	
	//Quaternion
	public static Quaternion SetX(this Quaternion rot, float x)
	{
		return new Quaternion(x, rot.y, rot.z, rot.w);
	}
	
	public static Quaternion SetY(this Quaternion rot, float y)
	{
		return new Quaternion(rot.x, y, rot.z, rot.w);
	}
	
	public static Quaternion SetZ(this Quaternion rot, float z)
	{
		return new Quaternion(rot.x, rot.y, z, rot.w);
	}
	
	public static Quaternion ClampX(this Quaternion rot, float minRot, float maxRot)
	{
		return new Quaternion(Mathf.Clamp(rot.x, minRot, maxRot), rot.y, rot.z, rot.w);
	}

	public static Quaternion ClampY(this Quaternion rot, float minRot, float maxRot)
	{
		return new Quaternion(rot.x, rot.y + Mathf.Clamp(rot.y, minRot, maxRot), rot.z, rot.w);
	}

	public static Quaternion ClampZ(this Quaternion rot, float minRot, float maxRot)
	{
		return new Quaternion(rot.x, rot.y, Mathf.Clamp(rot.z, minRot, maxRot), rot.w);
	}

	public static float Remap(this float f, float fromMin, float fromMax, float toMin, float toMax)
	{
		float t = (f - fromMin) / (fromMax - fromMin);
		return Mathf.LerpUnclamped(toMin, toMax, t);
	}


	public static IEnumerator RotateAngleOverTime(this Transform transformToRotate, float angle, Vector3 axis, float inTime)
	{
		// calculate rotation speed
		float rotationSpeed = angle / inTime;

		while (true)
		{
			// save starting rotation position
			Quaternion startRotation = transformToRotate.rotation;

			float deltaAngle = 0;

			// rotate until reaching angle
			while (deltaAngle < angle)
			{
				deltaAngle += rotationSpeed * Time.deltaTime;
				deltaAngle =  Mathf.Min(deltaAngle, angle);

				transformToRotate.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

				yield return null;
			}

			yield break;
		}
	}

}