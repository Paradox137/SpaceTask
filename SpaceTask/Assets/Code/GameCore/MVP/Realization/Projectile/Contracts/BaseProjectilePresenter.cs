using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.Service;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.MVP.Realization.Projectile.Contracts
{
	public abstract class BaseProjectilePresenter :  MonoBehaviour, 	
		IPoolable<ProjectileSpawner, ProjectileSpawnInfo, ProjectileConfig, IMemoryPool>, IProjectile
	{
		[SerializeField]
		protected ObservableTrigger2DTrigger _observableTrigger2D;

		private IMemoryPool _pool;
		
		protected CompositeDisposable _disposables;
		protected ProjectileEntity _entity;

		private void InitSpawn(ProjectileSpawnInfo __info, ProjectileConfig __config, IMemoryPool __pool)
		{
			_disposables = new CompositeDisposable();

			_entity = new ProjectileEntity(__config, this.gameObject, __info.DirectionMove);

			gameObject.transform.position = __info.Position;
			gameObject.transform.rotation = Quaternion.AngleAxis(__info.AngleRotation, Vector3.forward);

			_pool = __pool;
		}

		public virtual void OnSpawned(ProjectileSpawner __spawner, ProjectileSpawnInfo __info, ProjectileConfig __config, IMemoryPool __pool)
		{
			InitSpawn(__info, __config, __pool);
		}
		
		public virtual void SetupObserves()
		{
			Observable.EveryFixedUpdate()
				.Subscribe(_ => _entity.Transform.position =new Vector2(_entity.Transform.position.x, 
					_entity.Transform.position.y + Time.deltaTime * _entity.Speed * _entity.DirectionMove.y))
				.AddTo(_disposables);
		}

		public abstract void SetupNotifications<T>(params T[] __listeners) where T : IListener;

		public void Dispose()
		{
			_disposables.Dispose();
			
			_pool.Despawn(this);
		}

		public void OnDespawned()
		{
			_pool = null;
		}

		public class Factory : PlaceholderFactory<ProjectileSpawner, ProjectileSpawnInfo, ProjectileConfig, IProjectile>
		{
		}
	}
}
