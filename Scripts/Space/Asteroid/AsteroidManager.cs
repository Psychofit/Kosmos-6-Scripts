using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    /* Класс `AsteroidManager` отвечает за инстанцирование сетки
    астероидов в 3D-пространстве в игре Unity. Он определяет такие параметры, как количество
    астероидов по каждой оси (X, Y, Z), расстояние между астероидами и префаб*/
    public class AsteroidManager : MonoBehaviour
    {
        public GameObject AsteroidPrefab;

        public int NumberOfAsteroidsOnAxisX = 10;
        public int NumberOfAsteroidsOnAxisY = 10;
        public int NumberOfAsteroidsOnAxisZ = 10;

        public int GridSpacing = 10;

        private void Start() //Метод создает сетку астероидов с заданными параметрами.
        {
            for (int i = 0; i < NumberOfAsteroidsOnAxisX; i++)
            {
                for (int j = 0; j < NumberOfAsteroidsOnAxisY; j++)
                {
                    for (int k = 0; k < NumberOfAsteroidsOnAxisZ; k++)
                    {
                        InstantiateAsteroid(i, j, k);
                    }
                }
            }
        }

        private void InstantiateAsteroid(int x, int y, int z) //Метод для создания отдельного астероида в заданной позиции с учётом смещения.
        {
            Vector3 position = new Vector3(transform.position.x + x * GridSpacing + OffsetAsteroid(), transform.position.y + y * GridSpacing + OffsetAsteroid(), transform.position.z + z * GridSpacing + OffsetAsteroid());

            Instantiate(AsteroidPrefab, position, Quaternion.identity, transform);
        }

        private float OffsetAsteroid() //Метод для генерации случайного смещения астероида относительно сетки.
        {
            return Random.Range(-GridSpacing / 4f, GridSpacing / 4f);
        }
    }
}