using SpaceTask.GameCore.Logic.Signals;
using SpaceTask.GameCore.MVP.Contracts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceTask.GameCore.Components
{
	public class PauseComponent : MonoBehaviour, ISignalBusFireable
	{
		[SerializeField] private Button _pauseButton;
		[SerializeField] private Button _resumeButton;
		[SerializeField] private Button _restartButton;

		[SerializeField] private GameObject _pausePanel;
		
		[Inject]
		public void Construct(SignalBus __signalBus)
		{
			SetupFire(__signalBus);
		}
		
		public void SetupFire(SignalBus __signalBus)
		{
			_pauseButton.onClick.AsObservable().Subscribe(_ => PauseInvoke(__signalBus));
			
			_restartButton.onClick.AsObservable().Subscribe(_ => RestartInvoke(__signalBus));
			
			_resumeButton.onClick.AsObservable().Subscribe(_ => ResumeInvoke(__signalBus));
		}
		
		private void PauseInvoke(SignalBus __signalBus) 
		{
			_pausePanel.SetActive(true);
			
			__signalBus.Fire<PauseGame>();
		}
		
		private void RestartInvoke(SignalBus __signalBus) 
		{
			_pausePanel.SetActive(false);
			
			__signalBus.Fire<ResetGame>();
			
			__signalBus.Fire<ResumeGame>();
		}
		
		private void ResumeInvoke(SignalBus __signalBus) 
		{
			_pausePanel.SetActive(false);
			
			__signalBus.Fire<ResumeGame>();
		}
	}
}
