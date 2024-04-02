using SpaceTask.GameCore.Logic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceTask.GameCore.MVP.Realization.Input
{
	public class InputEntity
	{
		public ReactiveProperty<Vector2> Position { get; set; }
		
		public Image DragArea { get; }
		
		public InputEntity(Image __dragArea)
		{
			DragArea = __dragArea;
			
			Position = new ReactiveProperty<Vector2>(Constants.PlayerShipPosition);
		}
	}
}
