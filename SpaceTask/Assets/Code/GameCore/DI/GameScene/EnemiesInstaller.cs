using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.GameLogic;
using SpaceTask.GameCore.MVP.Realization.Projectile;
using SpaceTask.GameCore.MVP.Realization.Projectile.Contracts;
using SpaceTask.GameCore.Service;
using SpaceTask.GameCore.Service.Contracts;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class EnemiesInstaller : MonoInstaller
	{
		[SerializeField] private GameObject[] _enemiesGameObjects;
		[SerializeField] private EnemyConfig _config;
		[SerializeField] private EnemiesProjectilesSpawnConfig _spawnConfig;
		
		[Space(5)]
		[Header("EASY PROJECTILES")]
		[SerializeField] private Transform _easyEnemiesProjectilesParent;
		[SerializeField] private int _easyPoolInitSize;
		[SerializeField] private EnemiesProjectileConfig _easyProjectilesConfig;
		
		[Space(10)]
		[Header("MEDIUM PROJECTILES")]
		[SerializeField] private Transform _mediumEnemiesProjectilesParent;
		[SerializeField] private int _mediumPoolInitSize;
		[SerializeField] private EnemiesProjectileConfig _mediumProjectilesConfig;
		
		[Space(10)]
		[Header("HARD PROJECTILES")]
		[SerializeField] private Transform _hardEnemiesProjectilesParent;
		[SerializeField] private int _hardPoolInitSize;
		[SerializeField] private EnemiesProjectileConfig _hardProjectilesConfig;
		public override void InstallBindings()
		{
			ProjectileSpawner easySpawner = InstallSpawner<EasyProjectilePresenter>(_easyProjectilesConfig, _easyPoolInitSize,
				_easyEnemiesProjectilesParent, FactoriesID.EnemiesEasy);
			
			ProjectileSpawner mediumSpawner = InstallSpawner<MediumProjectilePresenter>(_mediumProjectilesConfig, _mediumPoolInitSize, 
				_mediumEnemiesProjectilesParent, FactoriesID.EnemiesMedium);
			
			ProjectileSpawner hardSpawner = InstallSpawner<HardProjectilePresenter>(_hardProjectilesConfig, _hardPoolInitSize, 
				_hardEnemiesProjectilesParent, FactoriesID.EnemiesHard);

			Container.BindInterfacesAndSelfTo<EnemiesCollection>()
				.AsSingle()
				.WithArguments(_config, _enemiesGameObjects)
				.NonLazy();
			
			Container.BindInterfacesAndSelfTo<EnemiesProjectilesSpawnService>()
				.AsSingle()
				.WithArguments(_spawnConfig, easySpawner, mediumSpawner, hardSpawner)
				.NonLazy();
		}
		private ProjectileSpawner InstallSpawner<T>(EnemiesProjectileConfig __projectilesConfig, int __poolInitSize, 
			Transform __enemiesProjectilesParent, FactoriesID __id) where  T : BaseProjectilePresenter
		{
			Container
				.BindFactory<ProjectileSpawner, ProjectileSpawnInfo, ProjectileConfig, 
					IProjectile, BaseProjectilePresenter.Factory>()
				.WithId(__id)
				.To<T>()
				.FromMonoPoolableMemoryPool(__pool => __pool
					.WithInitialSize(__poolInitSize)
					.ExpandByOneAtATime()
					.To<T>()
					.FromComponentInNewPrefab(__projectilesConfig.GameObject)
					.UnderTransform(__enemiesProjectilesParent)
					.AsCached()
					.NonLazy());

			ProjectileSpawner spawner = new ProjectileSpawner(Container.ResolveId<BaseProjectilePresenter.Factory>(__id), __projectilesConfig);

			Container
				.BindInterfacesAndSelfTo<ProjectileSpawner>()
				.FromInstance(spawner)
				.AsCached()
				.WithArguments(__projectilesConfig)
				.NonLazy();

			return spawner;
		}
	}
}
