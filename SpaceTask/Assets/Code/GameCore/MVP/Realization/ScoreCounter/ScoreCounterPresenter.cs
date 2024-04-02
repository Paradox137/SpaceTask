using System;
using System.Threading.Tasks;
using SpaceTask.GameCore.Logic.Signals;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.Service.Contracts;
using TMPro;
using UniRx;
using Zenject;

namespace SpaceTask.GameCore.MVP.Realization.ScoreCounter
{
	public class ScoreCounterPresenter : IListener, ISignalBusFireable, IResetable
	{
		private readonly ScoreCounterEntity _entity;
		private readonly TextMeshProUGUI _countText;

		public ScoreCounterPresenter(ScoreCounterEntity __entity, TextMeshProUGUI __countText, SignalBus __signalBus)
		{
			_entity = __entity;
			
			_countText = __countText;

			SetupFire(__signalBus);
		}

		public void SetupFire(SignalBus __signalBus)
		{
			_entity.Count.Where(__count => __count == _entity.Maximum)
				.Subscribe(_ => __signalBus.Fire<ResetGame>());
		}

		public async void UpdateCount(int __add)
		{
			await Task.Delay(TimeSpan.FromSeconds(0.1f));

			_entity.Count.Value += __add;
			
			if(_entity.Count.Value <= 9)
				_countText.text = "0" + _entity.Count.Value;
			else
				_countText.text = _entity.Count.Value.ToString();
		}

		public void Reset()
		{
			_entity.Count.Value = 0;
			
			_countText.text = "00";
		}
	}
}
