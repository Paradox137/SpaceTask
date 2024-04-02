using System.Collections.Generic;
using SpaceTask.GameCore.Logic.Signals;
using SpaceTask.GameCore.Service.Contracts;
using Zenject;

namespace SpaceTask.GameCore.Service
{
	public class ResetService
	{
		private readonly List<IResetable> _resetables;

		public ResetService(List<IResetable> __resetables, SignalBus __signalBus)
		{
			_resetables = __resetables;
			
			__signalBus.Subscribe<ResetGame>(ResetCurrentGame);
		}
		
		private void ResetCurrentGame()
		{
			foreach (IResetable resetable in _resetables)
			{
				resetable.Reset();
			}
		}
	}
}
