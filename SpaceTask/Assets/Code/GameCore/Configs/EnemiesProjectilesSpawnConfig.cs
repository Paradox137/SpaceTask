using UnityEngine;

namespace SpaceTask.GameCore.Configs
{
	[CreateAssetMenu(fileName = "EnemiesProjectilesSpawnConfig", menuName = "MyAssets/Game/Configs/EnemiesProjectilesSpawnConfig")]
	public class EnemiesProjectilesSpawnConfig : ScriptableObject
	{
		[SerializeField] private float _easySpawnRate;
		[SerializeField] private float _mediumSpawnRate;
		[SerializeField] private float _hardSpawnRate;
		
		public float HardSpawnRate => _hardSpawnRate;
		public float MediumSpawnRate => _mediumSpawnRate;
		public float EasySpawnRate => _easySpawnRate;
	}
}
