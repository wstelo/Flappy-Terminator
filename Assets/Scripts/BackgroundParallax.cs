using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [Header("Layer Setting")]
    [SerializeField] private List<SpriteRenderer> _layerObjects;
    [SerializeField] private List<float> _layerSpeed;

    private List<float> _startPosition = new List<float>();
    private Transform _camera;
    private float _boundSizeX;
    private float _layerSpeedMaxValue = 1;

    void Start()
    {
        _camera = Camera.main.transform;
        _boundSizeX = _layerObjects[0].sprite.bounds.size.x;

        for (int i = 0; i < _layerObjects.Count; i++)
        {
            _startPosition.Add(_camera.position.x);
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < _layerObjects.Count; i++)
        {
            float moveDistanceX = (_camera.position.x * _layerSpeed[i]);
            float layerLocalOffset = (_camera.position.x * (_layerSpeedMaxValue - _layerSpeed[i]));

            _layerObjects[i].transform.position = new Vector2(_startPosition[i] + moveDistanceX, _camera.position.y);

            if (layerLocalOffset > _startPosition[i] + _boundSizeX)
            {
                _startPosition[i] += _boundSizeX;
            }
        }
    }
}
