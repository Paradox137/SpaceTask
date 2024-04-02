namespace SpaceTask.GameCore.Service.Contracts
{
	public interface IPausable
	{
		void HandlePause();

		void HandleResume();
	}
}
