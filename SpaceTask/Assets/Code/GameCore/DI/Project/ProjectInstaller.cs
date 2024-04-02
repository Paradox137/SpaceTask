using SpaceTask.GameCore.Logic.Signals;
using Zenject;

namespace SpaceTask.GameCore.DI.Project
{
	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			SignalBusInstaller.Install(Container);

			Container.DeclareSignal<ResetGame>();
			Container.DeclareSignal<ResumeGame>();
			Container.DeclareSignal<PauseGame>();
		}
	}
}
