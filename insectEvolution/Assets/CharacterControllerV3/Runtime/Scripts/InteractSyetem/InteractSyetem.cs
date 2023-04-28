using UnityEngine;


namespace THEBADDEST.InteractSyetem
{


	public interface IEffectContainer
	{

		void AddEffect(string id, IEffect effect);

		void EmitEffect(IEffector effect);

		void EmitEffect(string effectId);

	}

	public interface IEffector
	{

		string  Id          { get; }
		Vector3 effectPoint { get; }

	}

	public interface IEffect
	{

		void Emit();

	}


}