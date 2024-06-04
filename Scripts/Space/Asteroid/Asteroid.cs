using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

namespace Cosmos_Six
{

    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float _minScale = 0.6f; //Минимальный размер астероида
        [SerializeField] private float _maxScale = 1.4f; //Максимальный размер астероида.
        [SerializeField] private float _rotationOffset = 100f; //Отклонение для случайной вращающей

        private Vector3 _randomRotation; //Вектор для хранения случайного вращения.

        private void Start() //Метод инициализирует начальный размер астероида и случайное вращение
        {
            Vector3 originalScale = transform.localScale;
            Vector3 newScale = new Vector3(Random.Range(_minScale, _maxScale), Random.Range(_minScale, _maxScale), Random.Range(_minScale, _maxScale)); 
            transform.localScale = new Vector3(originalScale.x * newScale.x, originalScale.y * newScale.y, originalScale.z * newScale.z); 

            _randomRotation = new Vector3(Random.Range(-_rotationOffset, _rotationOffset), Random.Range(-_rotationOffset, _rotationOffset), Random.Range(-_rotationOffset, _rotationOffset));
        }

        private void Update() //Метод отвечает за вращение астероида по заданным случайным осям.
        {
            transform.Rotate(_randomRotation * Time.deltaTime);
        }
    }
}