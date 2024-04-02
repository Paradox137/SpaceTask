using SpaceTask.GameCore.MVP.Realization.ScoreCounter;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.DI.GameScene
{
	public class ScoreCounterInstaller : MonoInstaller
	{
		[SerializeField] private TextMeshProUGUI _textMeshProUGUI;
		[SerializeField] private GameObject[] _enemiesGameObjects;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ScoreCounterEntity>().AsSingle().WithArguments(_enemiesGameObjects.Length).NonLazy();
			
			Container.BindInterfacesAndSelfTo<ScoreCounterPresenter>().AsSingle().WithArguments(_textMeshProUGUI).NonLazy();
		}
	}
}
