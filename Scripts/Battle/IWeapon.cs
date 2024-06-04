using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cosmos_Six
{
    /*Этот интерфейс определяет три метода, которые любой класс реализующий его должен обеспечить: 
    1. `Vector3 FireWeapon(Vector3 targetPosition)` - Этот метод отвечает за выстрел из оружия в
    оружия в указанную позицию цели и возвращает результат.
    2. `void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic)` - Этот метод используется для
    инициализации оружия с помощью внешних данных.
    3. `void VisualiseFiring(Vector3 targetPosition)` - Этот метод предназначен для визуализации стрельбы
    действия оружия в целевой позиции. */
    public interface IWeapon
    {
        Vector3 FireWeapon(Vector3 targetPosition);

        void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic);

        void VisualiseFiring(Vector3 targetPosition);

    }
}
