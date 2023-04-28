using UnityEngine;


namespace THEBADDEST.InteractSyetem
{


	public class TriggerEffector : EffectorBase
	{

		protected bool triggered = false;

		void OnTriggerEnter(Collider other)
		{
			if (triggered) return;
			var dealer = other.GetComponent<IEffectContainer>() ?? other.GetComponentInParent<IEffectContainer>();
			if (dealer != null)
				OnEffect(other, dealer);
		}

	}


}