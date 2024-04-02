using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.MVP.Realization.Projectile.Contracts;
using SpaceTask.GameCore.MVP.Realization.ScoreCounter;
using SpaceTask.GameCore.Service;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.MVP.Realization.Projectile
{
	public class ShipProjectilePresenter : BaseProjectilePresenter
	{
		[Inject] 
		private ScoreCounterPresenter _counterPresenter;
		
		private const string GameField = "GameField";
		private const string Enemy = "Enemy";
		
		public override void OnSpawned(ProjectileSpawner __spawner, ProjectileSpawnInfo __info, ProjectileConfig __config, IMemoryPool __pool)
		{
			base.OnSpawned(__spawner, __info, __config, __pool);

			SetupNotifications(new IListener[2]{__spawner, _counterPresenter});
			
			base.SetupObserves();
		}

		public override void SetupNotifications<T>(params T[] __listeners)
		{
			ProjectileSpawner spawner = __listeners[0] as ProjectileSpawner;

			_observableTrigger2D.OnTriggerExit2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(GameField))
				.Subscribe(_ => spawner?.Despawn(this))
				.AddTo(_disposables);
			
			_observableTrigger2D.OnTriggerEnter2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(Enemy))
				.Subscribe(_ => spawner?.Despawn(this))
				.AddTo(_disposables);

			ScoreCounterPresenter counterPresenter = __listeners[1] as ScoreCounterPresenter;
			
			_observableTrigger2D.OnTriggerEnter2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(Enemy))
				.Subscribe(_ => counterPresenter?.UpdateCount(1))
				.AddTo(_disposables);
		}
		
	}
}
