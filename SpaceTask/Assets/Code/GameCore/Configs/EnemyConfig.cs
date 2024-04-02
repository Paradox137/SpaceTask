using UnityEngine;

namespace SpaceTask.GameCore.Configs
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = "MyAssets/Game/Configs/EnemyConfig")]
	public class EnemyConfig : ScriptableObject
	{
		[SerializeField] private float _speed;

		public float Speed => _speed;
	}
}
