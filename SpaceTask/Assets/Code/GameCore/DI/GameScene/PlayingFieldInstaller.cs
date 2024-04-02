using SpaceTask.GameCore.Configs;
using SpaceTask.GameCore.GameLogic;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class PlayingFieldInstaller : MonoInstaller
	{
		[SerializeField] private FieldResolutionConfig _config;
		[SerializeField] private Camera _rendererCamera;
		[SerializeField] private Transform _fieldTransform;
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<PlayingField>()
				.AsSingle()
				.WithArguments(_fieldTransform, _rendererCamera, _config)
				.NonLazy();
		}
	}
}
