using SpaceTask.GameCore.Configs;
using UnityEngine;

namespace SpaceTask.GameCore.MVP.Realization.Projectile
{
	public class ProjectileEntity
	{
		public Transform Transform { get; }
		public float Speed { get; }
		public int Damage { get; }
		public Vector2 DirectionMove { get; }
		public BoxCollider2D BoxCollider2D { get; }
		
		public ProjectileEntity(ProjectileConfig __config, GameObject __gameObject, Vector2 __directionMove)
		{
			Transform = __gameObject.transform;

			Speed = __config.Speed;

			Damage = __config.Damage;

			DirectionMove = __directionMove;
			
			BoxCollider2D = __gameObject.GetComponent<BoxCollider2D>();
		}
		
	}
}
