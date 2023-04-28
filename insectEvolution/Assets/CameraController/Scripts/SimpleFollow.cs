using UnityEngine;


namespace GameDevUtils.CameraController
{


	[CreateAssetMenu(fileName = "SimpleFollow", menuName = "GameDevUtils/Camera/SimpleFollow")]
	public class SimpleFollow : CameraState
	{

		[SerializeField] protected     Vector3 posOffset, lookOffset;
		[SerializeField, Range(1, 120)] float   followSpeed = 5;
		[SerializeField, Range(1, 120)] float   lookSpeed   = 5;
		Vector3                                dir;

		protected override void UpdateCamera(Transform camera, Transform pivot, Transform target, float deltaTime)
		{
			pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
			dir                 = target.position - camera.position;
			dir.Normalize();
			camera.position = Vector3.Lerp(camera.position, target.position - dir, followSpeed * deltaTime);
			var rotation = target.rotation * Quaternion.Euler(lookOffset);
			camera.rotation = Quaternion.Slerp(camera.rotation, rotation, Time.deltaTime * lookSpeed);
		}

	}


}