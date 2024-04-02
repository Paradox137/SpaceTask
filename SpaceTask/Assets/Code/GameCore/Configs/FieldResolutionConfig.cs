using UnityEngine;

namespace SpaceTask.GameCore.Configs
{
	[CreateAssetMenu(fileName = "FieldResolutionConfig", menuName = "MyAssets/Game/Configs/FieldResolutionConfig")]
	public class FieldResolutionConfig : ScriptableObject
	{
		[Range(0, 12)]
		public int _offsetTop;

		[Range(0, 12)]
		public int _offsetBot;
		
		public float _divider;
		
		public float OffsetBot => _offsetBot / _divider;
		public float OffsetTop => _offsetTop / _divider;
	}
}
