using System.Collections.Generic;
using SpaceTask.GameCore.Logic.Signals;
using SpaceTask.GameCore.Service.Contracts;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.Service
{
	public class PauseService
	{
		private readonly List<IPausable> _pausables;

		public PauseService(List<IPausable> __pausables, SignalBus __signalBus)
		{
			_pausables = __pausables;
			
			__signalBus.Subscribe<PauseGame>(PauseCurrentGame);
			__signalBus.Subscribe<ResumeGame>(ResumeCurrentGame);
		}
		
		private void PauseCurrentGame()
		{
			Time.timeScale = 0f;
			
			foreach (IPausable resetable in _pausables)
			{
				resetable.HandlePause();
			}
		}
		
		private void ResumeCurrentGame()
		{
			Time.timeScale = 1f;
			
			foreach (IPausable resetable in _pausables)
			{
				resetable.HandleResume();
			}
		}
	}
}
