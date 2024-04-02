using UnityEngine;

namespace SpaceTask.GameCore.Configs
{
	[CreateAssetMenu(fileName = "PlayerShipConfig", menuName = "MyAssets/Game/Configs/PlayerShipConfig")]
	public class PlayerShipConfig : ScriptableObject
	{
		[Header("In seconds")]
		[SerializeField] private float _attackCooldown;

		[SerializeField] private int _hp;
		
		public float AttackCooldown => _attackCooldown;
		public int HP => _hp;
	}
}
