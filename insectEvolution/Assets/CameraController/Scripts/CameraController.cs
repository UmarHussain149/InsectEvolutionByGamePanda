using System;
using System.Collections;
using GameDevUtils.MVVM;
using UnityEngine;


namespace GameDevUtils.CameraController
{


	public class CameraController : StateMachine
	{

		protected enum UpdateType
		{

			Update,
			FixedUpdate,
			LateUpdate

		}

		//public static event Action<int> changeCamera;
		public static Action<CameraState, Transform> anyCamera;
		public static Action                         resetToDefaultCamera;
		public        CameraData                     details;
		public        CameraState[]                  cameraStates;

		[SerializeField] protected UpdateType updateType;
		Transform                             defualtTarget;

		void Awake()
		{
			anyCamera            = AnyCamera;
			resetToDefaultCamera = DefaultCamera;
			//changeCamera = ChangeCamera;
			foreach (CameraState state in cameraStates)
			{
				cachedStates.Add(state.stateName, state);
			}

			foreach (CameraState cameraState in cameraStates)
			{
				cameraState.Init(this);
			}

			defualtTarget    = details.target;
			currentStateName = string.IsNullOrEmpty(currentStateName) ? cameraStates[0].stateName : currentStateName;
			LoadState(currentStateName, Entry);
		}

		private void Update()
		{
			if (updateType == UpdateType.Update)
			{
				details.deltaTime = Time.deltaTime;
				UpdateCamera();
			}
		}

		private void FixedUpdate()
		{
			if (updateType == UpdateType.FixedUpdate)
			{
				details.deltaTime = Time.fixedDeltaTime;
				UpdateCamera();
			}
		}

		private void LateUpdate()
		{
			if (updateType == UpdateType.LateUpdate)
			{
				details.deltaTime = Time.deltaTime;
				UpdateCamera();
			}
		}

		protected virtual void UpdateCamera()
		{
			if (currentAnyState != null)
			{
				((CameraState) currentAnyState).cameraDetails = details;
			}
			else
				((CameraState) currentState).cameraDetails = details;

			StatesExecution();
		}

		public void AnyCamera(CameraState cameraState, Transform target)
		{
			if (target)
				details.target = target;
			AnyTransition(cameraState);
		}

		public void DefaultCamera()
		{
			details.target = defualtTarget;
			ExitAnyStates();
		}

		public override void LoadState(string id, Action<IState> onStateLoad)
		{
			if (cachedStates.ContainsKey(id))
			{
				onStateLoad?.Invoke(cachedStates[id]);
			}
		}

		void OnDestroy()
		{
			anyCamera            = null;
			resetToDefaultCamera = null;
		}

	}


}