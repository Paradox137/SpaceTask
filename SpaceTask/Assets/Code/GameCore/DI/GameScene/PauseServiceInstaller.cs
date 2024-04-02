using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Service;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class PauseServiceInstaller : MonoInstaller
	{
		[SerializeField] private PauseComponent _pauseComponent;
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<PauseComponent>().FromInstance(_pauseComponent).AsSingle().NonLazy();
			
			Container.BindInterfacesAndSelfTo<PauseService>().AsSingle().NonLazy();
		}
	}
}
