using System;
using SpaceTask.GameCore.Logic;
using SpaceTask.GameCore.Logic.Signals;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.MVP.Realization.Projectile;
using SpaceTask.GameCore.Service;
using SpaceTask.GameCore.Service.Contracts;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.MVP.Realization.PlayerShip
{
	public class PlayerShipPresenter : IListener, IObservable, IResetable, IDisposable, ISignalBusFireable
	{
		private readonly PlayerShipEntity _entity;

		private readonly TextMeshProUGUI _hpText;
		private readonly ObservableTrigger2DTrigger _observableTrigger2D;
		
		private const string EnemyProjectile = "EnemyProjectile";
		private readonly ProjectileSpawner _spawner;
		private CompositeDisposable _disposables;
		
		[Inject]
		public PlayerShipPresenter(PlayerShipEntity __playerShipEntity, ProjectileSpawner __spawner, TextMeshProUGUI __hpText, SignalBus __signalBus)
		{
			_hpText = __hpText;
			_entity = __playerShipEntity;

			_observableTrigger2D = _entity.ShipGameObject.GetComponent<ObservableTrigger2DTrigger>();
			_disposables = new CompositeDisposable();
			
			_spawner = __spawner;

			SetupFire(__signalBus);
			SetupObserves();
		}
		
		public void SetupFire(SignalBus __signalBus)
		{
			_entity.IsDead.Where(_ => _ == true).Subscribe(_ => __signalBus.Fire<ResetGame>());
		}
		
		public void SetupObserves()
		{
			_entity.HP
				.ObserveEveryValueChanged(_ => _.Value)
				.Subscribe(_ => _hpText.text = _.ToString());

			_observableTrigger2D.OnTriggerEnter2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(EnemyProjectile))
				.Subscribe(_ => _entity.HP.Value -= _.gameObject.GetComponent<BaseEnemyProjectilePresenter>().GetDamageValue())
				.AddTo(_disposables);
			
			Observable
				.Timer(TimeSpan.FromSeconds(_entity.AttackCooldown))
				.RepeatUntilDisable(_entity.ProjectileSpawnInfo)
				.Subscribe(_ => _spawner.Spawn(_entity.ProjectileSpawnInfo))
				.AddTo(_disposables);
		}

		public void UpdateShipPosition(Vector3 __value)
		{
			_entity.ShipTransform.position = __value;
		}

		public void Reset()
		{
			Dispose();
			
			_disposables = new CompositeDisposable();
			
			UpdateShipPosition(Constants.PlayerShipPosition);
			
			SetupObserves();
			
			_entity.Reset();
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
