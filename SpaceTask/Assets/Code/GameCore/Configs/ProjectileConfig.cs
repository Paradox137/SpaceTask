using UnityEngine;

namespace SpaceTask.GameCore.Configs
{
	public abstract class ProjectileConfig : ScriptableObject
	{
		[SerializeField] private GameObject _gameObject;
		[SerializeField] private float _speed;
		[SerializeField] private int _damage;
		
		public GameObject GameObject => _gameObject;
		public float Speed => _speed;
		public int Damage => _damage;
	}
}
