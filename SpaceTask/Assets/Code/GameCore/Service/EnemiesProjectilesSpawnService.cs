using System;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.GameLogic;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.Service.Contracts;
using UniRx;
using Random = UnityEngine.Random;

namespace SpaceTask.GameCore.Service
{
	public class EnemiesProjectilesSpawnService : IObservable, IDisposable, IResetable
	{
		private CompositeDisposable _disposables;
		
		private readonly EnemiesCollection _collection;
		private readonly EnemiesProjectilesSpawnConfig _config;
		
		private readonly ProjectileSpawner _easySpawner;
		private readonly ProjectileSpawner _mediumSpawner;
		private readonly ProjectileSpawner _hardSpawner;
		
		public EnemiesProjectilesSpawnService(EnemiesCollection __collection, EnemiesProjectilesSpawnConfig __config,
			ProjectileSpawner __easySpawner, ProjectileSpawner __mediumSpawner, ProjectileSpawner __hardSpawner)
		{
			_disposables = new CompositeDisposable();
			
			_collection = __collection;
			_config = __config;
			_easySpawner = __easySpawner;
			_mediumSpawner = __mediumSpawner;
			_hardSpawner = __hardSpawner;

			SetupObserves();
		}
		
		public void SetupObserves()
		{
			Observable
				.Timer(TimeSpan.FromSeconds(_config.EasySpawnRate))
				.Delay(TimeSpan.FromSeconds(GetRandomDelay()))
				.RepeatSafe()
				.Subscribe(_ => _easySpawner.Spawn(_collection.GetRandomEntity().ProjectileSpawnInfo))
				.AddTo(_disposables);
			
			Observable
				.Timer(TimeSpan.FromSeconds(_config.MediumSpawnRate))
				.Delay(TimeSpan.FromSeconds(GetRandomDelay()))
				.RepeatSafe()
				.Subscribe(_ => _mediumSpawner.Spawn(_collection.GetRandomEntity().ProjectileSpawnInfo))
				.AddTo(_disposables);
			
			Observable
				.Timer(TimeSpan.FromSeconds(_config.HardSpawnRate))
				.Delay(TimeSpan.FromSeconds(GetRandomDelay()))
				.RepeatSafe()
				.Subscribe(_ => _hardSpawner.Spawn(_collection.GetRandomEntity().ProjectileSpawnInfo))
				.AddTo(_disposables);
		}

		private float GetRandomDelay()
		{
			return Random.Range(0f, 2f);
		}
		
		public void Dispose()
		{
			_disposables?.Dispose();
		}
		
		public void Reset()
		{
			Dispose();
			
			_disposables = new CompositeDisposable();
			
			SetupObserves();
		}
	}
}
