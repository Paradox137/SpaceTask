using SpaceTask.GameCore.MVP.Realization.Input;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class InputInstaller : MonoInstaller
	{
		[SerializeField] private ObservableEventTrigger _dragTrigger; 
		[SerializeField] private Camera _mainCamera; 
		[SerializeField] private Image _dragArea;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<InputEntity>().AsSingle().WithArguments(_dragArea).NonLazy();
			
			Container.BindInterfacesAndSelfTo<InputPresenter>()
				.AsSingle()
				.WithArguments(_dragTrigger, _mainCamera)
				.NonLazy();
		}
	}
}
