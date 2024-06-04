using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Cosmos_Six
{
    /* Публичный интерфейс IInputShipMovement определяет контракт для классов, реализующих функциональность ввода движения корабля
    функциональность ввода движения. Он включает свойства для получения текущих входных значений для
    перемещения корабля в различных направлениях и вращения корабля по
    различных осей (тангаж, рысканье, крен). */
    public interface IInputShipMovement
    {
        public float CurrentInputMove { get; }

        public float CurrentInputRotatePitch { get; }
        public float CurrentInputRotateYaw { get; }
        public float CurrentInputRotateRoll { get; } 
        
    }
    /* Публичный интерфейс IInputShipWeapons определяет контракт для классов, реализующих функциональность ввода корабельного
    функциональность ввода оружия. Он включает свойства для получения текущего значения ввода для
    атаки оружием корабля и событие для обработки ввода атаки.*/
    public interface IInputShipWeapons
    {
        public bool CurrentInputAttack { get; }
        public event Action OnAttackInput;
    }

}
