using SpaceTask.GameCore.Configs;
using UnityEngine;
using Zenject;

namespace SpaceTask.GameCore.GameLogic
{
	public class PlayingField : IInitializable
	{
		private readonly float _offsetTop;

		private readonly float _offsetBot;

		private const float FieldMultiplier = 1f;

		private readonly Transform _fieldTransform;

		private readonly Camera _renderCamera;
		
		protected PlayingField(Transform __fieldTransform, Camera __renderCamera, FieldResolutionConfig __config)
		{
			_offsetBot = __config.OffsetBot;
			_offsetTop = __config.OffsetTop;
			
			_fieldTransform = __fieldTransform;

			_renderCamera = __renderCamera;
		}
		
		public void Initialize()
		{
			_fieldTransform.position = new Vector3(0, 0, _fieldTransform.position.z);
			
			MatchFieldToScreenSize();
		}

		private void MatchFieldToScreenSize()
		{
			float fieldHeightScale = (2.0f * _renderCamera.orthographicSize / FieldMultiplier);

			float fieldWidthScale = fieldHeightScale * _renderCamera.aspect;

			_fieldTransform.localScale = new Vector3(fieldWidthScale, fieldHeightScale - (_offsetTop + _offsetBot) * fieldHeightScale, 
				_fieldTransform.transform.localScale.z);
            
			_fieldTransform.position = new Vector3(_fieldTransform.position.x, 
				_fieldTransform.position.y + (_offsetBot - _offsetTop) * _renderCamera.orthographicSize,
				_fieldTransform.position.z);
		}
	}
}
