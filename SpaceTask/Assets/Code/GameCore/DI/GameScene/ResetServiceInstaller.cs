using SpaceTask.GameCore.Service;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class ResetServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ResetService>().AsSingle().NonLazy();
		}
	}
}
