using UniRx;

namespace SpaceTask.GameCore.MVP.Realization.ScoreCounter
{
	public class ScoreCounterEntity
	{
		public ReactiveProperty<int> Count{ get; set; }
		public int Maximum { get; set; }

		public ScoreCounterEntity(int __maximum)
		{
			Maximum = __maximum;
			
			Count = new ReactiveProperty<int>(0);
		}
	}
}
