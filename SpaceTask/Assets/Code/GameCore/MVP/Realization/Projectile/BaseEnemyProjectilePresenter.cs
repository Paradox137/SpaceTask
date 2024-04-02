using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.MVP.Realization.Projectile.Contracts;
using SpaceTask.GameCore.Service;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.MVP.Realization.Projectile
{
	public class BaseEnemyProjectilePresenter : BaseProjectilePresenter
	{
		private const string GameField = "GameField";
		private const string Ship = "Ship";
		
		public override void OnSpawned(ProjectileSpawner __spawner, ProjectileSpawnInfo __info, ProjectileConfig __config, IMemoryPool __pool)
		{
			base.OnSpawned(__spawner, __info, __config, __pool);

			SetupNotifications(__spawner);
			
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
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(Ship))
				.Subscribe(_ => spawner?.Despawn(this))
				.AddTo(_disposables);
		}
		public int GetDamageValue()
		{
			return _entity.Damage;
		}
	}
}
