using System.Collections.Generic;
using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.MVP.Realization.Projectile.Contracts;
using SpaceTask.GameCore.Service.Contracts;

namespace SpaceTask.GameCore.Service
{
	public class ProjectileSpawner : IListener, IResetable
	{
		private readonly BaseProjectilePresenter.Factory _factory;
		private readonly ProjectileConfig _config;
		private readonly List<IProjectile> _projectiles;	
		
		public ProjectileSpawner(BaseProjectilePresenter.Factory __factory, ProjectileConfig __config)
		{
			_projectiles = new List<IProjectile>();

			_factory = __factory;
			
			_config = __config;
		}

		public void Spawn(ProjectileSpawnInfo __spawnInfo)
		{
			IProjectile presenter = _factory.Create(this, __spawnInfo, _config);

			_projectiles.Add(presenter);
		}
		
		public void Despawn(IProjectile __projectile)
		{
			__projectile.Dispose();
			
			_projectiles.Remove(__projectile);
		}
		
		private void DespawnnAll()
		{
			foreach (IProjectile projectilePresenter in _projectiles)
				projectilePresenter.Dispose();

			_projectiles.Clear();
		}
		
		public void Reset()
		{
			DespawnnAll();
		}
	}
}
