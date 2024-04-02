using Zenject;

namespace SpaceTask.GameCore.MVP.Contracts
{
	public interface ISignalBusFireable
	{
		public void SetupFire(SignalBus __signalBus);
	}
}
