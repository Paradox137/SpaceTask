namespace SpaceTask.GameCore.MVP.Contracts
{
	public interface INotificator
	{
		void SetupNotifications<T>(params T[] __listeners) where T : IListener;
	}
}
