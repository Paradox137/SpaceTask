using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using UniRx;
using UnityEngine;

namespace SpaceTask.GameCore.MVP.Realization.PlayerShip
{
	public class PlayerShipEntity
	{
		private readonly int _initialHp;
		public ProjectileSpawnInfo ProjectileSpawnInfo { get; }
		public ReadOnlyReactiveProperty<bool> IsDead { get; set; }
		public Transform ShipTransform { get; set; }
		public GameObject ShipGameObject { get; set; }
		public float AttackCooldown { get; }
		public ReactiveProperty<int> HP { get;  }

		public PlayerShipEntity(GameObject __gameObject, ProjectileSpawnInfo __projectileSpawnInfo, PlayerShipConfig __config)
		{
			HP = new ReactiveProperty<int>(__config.HP);
			
			_initialHp = __config.HP;
			
			IsDead = new ReadOnlyReactiveProperty<bool>(HP.Select(_ => _ <= 0));

			AttackCooldown = __config.AttackCooldown;
			
			ProjectileSpawnInfo = __projectileSpawnInfo;
			
			ShipTransform = __gameObject.transform;

			ShipGameObject = __gameObject;
		}
		
		public void Reset()
		{
			HP.Value = _initialHp;
		}
	}
}
