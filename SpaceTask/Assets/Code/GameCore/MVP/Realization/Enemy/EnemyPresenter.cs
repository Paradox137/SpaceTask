using System;
using SpaceTask.GameCore.GameLogic;
using SpaceTask.GameCore.MVP.Contracts;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SpaceTask.GameCore.MVP.Realization.Enemy
{
	public class EnemyPresenter : IObservable, IDisposable, INotificator
	{
		private readonly EnemyEntity _entity;
		
		private readonly ObservableTrigger2DTrigger _observableTrigger2D;

		private CompositeDisposable _observeDisposables;
		private readonly CompositeDisposable _notificationDisposables;
		
		private const string PlayerProjectile = "PlayerProjectile";
		private const string FieldBlocks = "Block";
		
		public EnemyPresenter(EnemyEntity __entity, EnemiesCollection __collection)
		{
			_observeDisposables = new CompositeDisposable();
			_notificationDisposables = new CompositeDisposable();
			
			_entity = __entity;

			_observableTrigger2D = _entity.GameObject.GetComponent<ObservableTrigger2DTrigger>();
			
			SetupNotifications(__collection);
			
			SetupObserves();
		}

		public void SetupNotifications<T>(params T[] __listeners) where T : IListener
		{
			EnemiesCollection collection = __listeners[0] as EnemiesCollection;
			
			_observableTrigger2D.OnTriggerEnter2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(FieldBlocks))
				.Subscribe(_ => collection?.ChangeDirection())
				.AddTo(_notificationDisposables);
			
			_observableTrigger2D.OnTriggerEnter2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(PlayerProjectile))
				.Subscribe(_ => collection?.RemoveEntity(_entity))
				.AddTo(_notificationDisposables);
		}
		
		public void SetupObserves()
		{
			_observableTrigger2D.OnTriggerEnter2DAsObservable()
				.Where(__trigger => __trigger.gameObject.layer == LayerMask.NameToLayer(PlayerProjectile))
				.Subscribe(_ => DeactivateEnemy())
				.AddTo(_observeDisposables);

			Observable.EveryFixedUpdate()
				.Subscribe(_ => _entity.Transform.position =new Vector2(
					_entity.Transform.position.x + Time.deltaTime * _entity.Speed * _entity.DirectionMove.x, 
					_entity.Transform.position.y))
				.AddTo(_observeDisposables);
		}

		public void ChangeDirection()
		{
			_entity.DirectionMove = new Vector2(_entity.DirectionMove.x * -1f, 0f);
		}

		private void DeactivateEnemy()
		{
			_entity.GameObject.SetActive(false);

			Dispose();
		}
		
		public void Reset()
		{
			Dispose();
			
			_observeDisposables = new CompositeDisposable();
			
			_entity.Transform.position = _entity.InitPosition;
			
			_entity.GameObject.SetActive(true);

			SetupObserves();
		}

		public void Dispose()
		{
			_observeDisposables.Dispose();
		}
	}
}
