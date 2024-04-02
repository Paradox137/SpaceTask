using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.MVP.Realization.Enemy;
using SpaceTask.GameCore.Service.Contracts;
using UnityEngine;
using Random = System.Random;

namespace SpaceTask.GameCore.GameLogic
{
	public class EnemiesCollection : IListener, IResetable
	{
		private readonly GameObject[] _enemiesGameObjects;
		private readonly EnemyConfig _config;

		private readonly EnemyEntity[] _entities;
		private readonly EnemyPresenter[] _presenters;
		
		private List<EnemyEntity> _activeEntities;
		private Random _random;
		private bool _canChangeDelay;
		
		public EnemiesCollection(EnemyConfig __config, GameObject[] __enemiesGameObjects)
		{
			_config = __config;

			_entities = new EnemyEntity[__enemiesGameObjects.Length];

			_presenters = new EnemyPresenter[__enemiesGameObjects.Length];

			_enemiesGameObjects = __enemiesGameObjects;

			Initialize();
		}

		private void Initialize()
		{
			_canChangeDelay = true;
			_random = new Random();
			_activeEntities = new List<EnemyEntity>();
			
			for (int i = 0; i < _enemiesGameObjects.Length; i++)
			{
				_entities[i] = new EnemyEntity(_config, _enemiesGameObjects[i]);

				_presenters[i] = new EnemyPresenter(_entities[i], this);
			}

			_activeEntities = _entities.ToList();
		}

		public async void ChangeDirection()
		{
			if (_canChangeDelay)
			{
				_canChangeDelay = false;
				
				foreach (EnemyPresenter enemyPresenter in _presenters)
					enemyPresenter.ChangeDirection();

				await StartDelay();
			}
		}
		private async Task StartDelay()
		{
			await Task.Delay(TimeSpan.FromSeconds(0.5f));

			_canChangeDelay = true;
		}
		
		public void Reset()
		{
			_activeEntities = _entities.ToList();

			foreach (EnemyPresenter enemyPresenter in _presenters)
				enemyPresenter.Reset();
		}
		
		public EnemyEntity GetRandomEntity()
		{
			int index = _random.Next(_activeEntities.Count);
			
			return _activeEntities?[index];
		}
		
		public void RemoveEntity(EnemyEntity __entity)
		{
			_activeEntities.Remove(__entity);
		}
	}
}
