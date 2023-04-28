using System;


namespace THEBADDEST.InteractSyetem
{


	public class ActionEffect : IEffect
	{

		Action task;

		public ActionEffect(Action task)
		{
			this.task = task;
		}
		
		public void Emit()
		{
			task?.Invoke();
		}

	}


}