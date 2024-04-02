using SpaceTask.GameCore.Components;
using SpaceTask.GameCore.Configs;
using UnityEngine;

namespace SpaceTask.GameCore.MVP.Realization.Enemy
{
	public class EnemyEntity
	{
		public Vector3 InitPosition { get; }
		public float Speed { get; }
		public Transform Transform { get; }
		public GameObject GameObject { get; }
		public Vector2 DirectionMove { get; set; }
		public ProjectileSpawnInfo ProjectileSpawnInfo { get; }
		
		public EnemyEntity(EnemyConfig __config, GameObject __gameObject)
		{
			Speed = __config.Speed;

			Transform = __gameObject.transform;

			GameObject = __gameObject;

			DirectionMove = new Vector2(1, 0);

			InitPosition = __gameObject.transform.position;

			ProjectileSpawnInfo = __gameObject.GetComponent<ProjectileSpawnInfo>();
		}
	}
}
