using UnityEngine;

namespace SpaceTask.GameCore.Components
{
	public class ProjectileSpawnInfo : MonoBehaviour
	{
		[SerializeField] private float _angleRotation;
		[SerializeField] private Vector2 _directionMove;
		[SerializeField] private Transform _spawnPosition;
		public float AngleRotation => _angleRotation;
		
		public Vector2 Position => _spawnPosition.position;
		
		public Vector2 DirectionMove => _directionMove;
	}
}
