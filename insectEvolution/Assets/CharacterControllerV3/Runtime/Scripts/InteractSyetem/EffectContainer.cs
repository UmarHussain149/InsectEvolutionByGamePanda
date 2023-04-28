using System.Collections.Generic;


namespace THEBADDEST.InteractSyetem
{


	public class EffectContainer : IEffectContainer
	{

		Dictionary<string, IEffect> effectors;

		public EffectContainer()
		{
			effectors = new Dictionary<string, IEffect>();
		}

		public virtual void AddEffect(string id, IEffect effect)
		{
			effectors.Add(id, effect);
		}


		public virtual void EmitEffect(IEffector effect)
		{
			if (effectors.ContainsKey(effect.Id))
				effectors[effect.Id]?.Emit();
		}

		public virtual void EmitEffect(string effectId)
		{
			if (effectors.ContainsKey(effectId))
				effectors[effectId]?.Emit();
		}

	}


}