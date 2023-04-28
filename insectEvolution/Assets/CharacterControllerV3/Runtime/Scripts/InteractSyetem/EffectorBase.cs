using UnityEngine;


namespace THEBADDEST.InteractSyetem
{


	public abstract class EffectorBase : MonoBehaviour, IEffector
	{

		[SerializeField] protected string  id;
		public                     string  Id          => id;
		public                     Vector3 effectPoint { get; protected set; }

		protected virtual void OnEffect(Collider other, IEffectContainer container)
		{
			effectPoint = other.transform.position;
			container.EmitEffect(this);
		}

	}


}