using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.MVP.Realization.PlayerShip;
using SpaceTask.GameCore.MVP.Realization.Projectile;
using SpaceTask.GameCore.MVP.Realization.Projectile.Contracts;
using SpaceTask.GameCore.Service;
using SpaceTask.GameCore.Service.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class ShipInstaller : MonoInstaller
	{
		[SerializeField] private GameObject _playerShipGameObject;
		[SerializeField] private ProjectileSpawnInfo _spawnInfo;
		[SerializeField] private PlayerShipConfig _shipConfig;
		
		[FormerlySerializedAs("playerProjectilesParent")]
		[SerializeField] private Transform _playerProjectilesParent;
		[FormerlySerializedAs("poolInitSize")]
		[SerializeField] private int _poolInitSize;
		[SerializeField] private PlayerProjectileConfig _projectilesConfig;
		[SerializeField] private TextMeshProUGUI _hpText;
		public override void InstallBindings()
		{
			Container
				.BindFactory<ProjectileSpawner, ProjectileSpawnInfo, ProjectileConfig, 
					IProjectile, BaseProjectilePresenter.Factory>()
				.WithId(FactoriesID.ShipDefault)
				.To<ShipProjectilePresenter>()
				.FromMonoPoolableMemoryPool(__pool => __pool
					.WithInitialSize(_poolInitSize)
					.ExpandByOneAtATime()
					.To<ShipProjectilePresenter>()
					.FromComponentInNewPrefab(_projectilesConfig.GameObject)
					.UnderTransform(_playerProjectilesParent)
					.AsCached()
					.NonLazy());

			ProjectileSpawner spawner = new ProjectileSpawner(Container.ResolveId<BaseProjectilePresenter.Factory>(FactoriesID.ShipDefault), _projectilesConfig);
			
			Container
				.BindInterfacesAndSelfTo<ProjectileSpawner>()
				.FromInstance(spawner)
				.AsCached()
				.WithArguments(_projectilesConfig)
				.CopyIntoDirectSubContainers();
			
			Container.BindInterfacesAndSelfTo<PlayerShipPresenter>()
				.FromSubContainerResolve()
				.ByMethod(InstallFacade)
				.AsSingle()
				.NonLazy();
		}
		private void InstallFacade(DiContainer __subContainer)
		{
			__subContainer.BindInterfacesAndSelfTo<PlayerShipEntity>()
				.AsSingle()
				.WithArguments(_playerShipGameObject, _spawnInfo, _shipConfig)
				.NonLazy();

			__subContainer.BindInterfacesAndSelfTo<PlayerShipPresenter>().AsSingle().WithArguments(_hpText);
		}
	}
}
