using UnityEngine;


namespace THEBADDEST.InteractSyetem
{


	public class CollideEffector : EffectorBase
	{

		protected bool triggered = false;

		void OnCollisionEnter(Collision collision)
		{
			if (triggered) return;
			var dealer = collision.collider.GetComponent<IEffectContainer>() ?? collision.collider.GetComponentInParent<IEffectContainer>();
			if (dealer != null)
				OnEffect(collision.collider, dealer);
		}

	}


}