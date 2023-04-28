using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace THEBADDEST.CharacterController3.EditorCore
{


	[System.Serializable]
	class BehaviourData
	{

		public CharacterBehaviour behaviour;
		public bool               folded;
		public Editor             editor;

	}

	[CustomEditor(typeof(CharacterController))]
	public class CharacterControllerEditor : Editor
	{

		CharacterController controller;
		BehaviourData[]     behaviourDatas;
		int                 oldLenghth;

		void OnEnable()
		{
			controller = (CharacterController) (target);
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			DrawBehavioursInspector();
		}

		void DrawBehavioursInspector()
		{
			using (var check = new EditorGUI.ChangeCheckScope())
			{
				EditorGUILayout.Space();
				var behaviours = controller.behaviours;
				if (behaviours.Length != oldLenghth)
				{
					oldLenghth     = behaviours.Length;
					behaviourDatas = new BehaviourData[behaviours.Length];
					for (int i = 0; i < behaviours.Length; i++)
					{
						behaviourDatas[i] = new BehaviourData() {behaviour = behaviours[i]};
					}
				}

				for (int i = 0; i < behaviourDatas.Length; i++)
				{
					if (behaviourDatas[i].behaviour)
					{
						behaviourDatas[i].folded = EditorGUILayout.InspectorTitlebar(behaviourDatas[i].folded, behaviourDatas[i].behaviour);
						if (behaviourDatas[i].folded)
						{
							CreateCachedEditor(behaviourDatas[i].behaviour, null, ref behaviourDatas[i].editor);
							behaviourDatas[i].editor.OnInspectorGUI();
						}
					}
				}
			}
		}

	}


}