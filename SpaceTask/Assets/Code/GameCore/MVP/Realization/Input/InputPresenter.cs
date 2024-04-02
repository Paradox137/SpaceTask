using System;
using SpaceTask.GameCore.MVP.Contracts;
using SpaceTask.GameCore.MVP.Realization.PlayerShip;
using SpaceTask.GameCore.Service.Contracts;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SpaceTask.GameCore.MVP.Realization.Input
{
	public class InputPresenter : INotificator, IObservable, IResetable, IDisposable, IPausable
	{
		private readonly ObservableEventTrigger _dragTrigger;
		
		private readonly InputEntity _inputEntity;
		
		private readonly Camera _mainCamera;
		private readonly EventSystem _eventSystem;
		
		private CompositeDisposable _disposables;
		private CompositeDisposable _resetDisposables;
		
		[Inject]
		public InputPresenter(ObservableEventTrigger __dragTrigger, InputEntity __inputEntity, Camera __mainCamera, PlayerShipPresenter __playerShipPresenter)
		{
			_disposables = new CompositeDisposable();
			
			_dragTrigger = __dragTrigger;
			
			_inputEntity = __inputEntity;
			
			_mainCamera = __mainCamera;

			SetupNotifications(__playerShipPresenter);
			
			SetupObserves();
		}

		public void SetupNotifications<T>(params T[] __listeners) where T : IListener
		{
			PlayerShipPresenter playerShipPresenter = __listeners[0] as PlayerShipPresenter;

			NotificationToShipTransform(playerShipPresenter);
		}

		public void SetupObserves()
		{
			DragObserve();
		}

		private void NotificationToShipTransform(PlayerShipPresenter __playerShipPresenter)
		{
			_inputEntity.Position
				.Subscribe(__position => __playerShipPresenter?.UpdateShipPosition(__position));
		}
		
		private void DragObserve()
		{
			_dragTrigger.OnBeginDragAsObservable()
				.SelectMany(_ => _dragTrigger.OnDragAsObservable())
				.TakeUntil(_dragTrigger.OnEndDragAsObservable())
				.Select(_ => _.position)
				.RepeatUntilDestroy(_dragTrigger)
				.Subscribe(UpdateInputPosition)
				.AddTo(_disposables);

			_dragTrigger.OnPointerDownAsObservable()
				.Select(_ => _.position)
				.Subscribe(UpdateInputPosition)
				.AddTo(_disposables);
		}

		private void UpdateInputPosition(Vector2 __position)
		{
			_inputEntity.Position.Value = _mainCamera.ScreenToWorldPoint(__position);
		}
		
		private void ReInit()
		{
			_resetDisposables.Dispose();
			
			_disposables = new CompositeDisposable();
			
			DragObserve();
		}
		
		public void Reset()
		{
			Dispose();
			
			_resetDisposables = new CompositeDisposable();

			_dragTrigger
				.OnPointerUpAsObservable()
				.Subscribe(_ => ReInit())
				.AddTo(_resetDisposables);
		}
		
		public void Dispose()
		{
			_disposables.Dispose();
		}
		
		public void HandlePause()
		{
			_inputEntity.DragArea.raycastTarget = false;
		}
		
		public void HandleResume()
		{
			_inputEntity.DragArea.raycastTarget = true;

			if (_resetDisposables != null && _resetDisposables.Count > 0)
				ReInit();
		}
	}
}
