using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public class Engine : MonoBehaviour
    {
        [Serializable] 
        class EngineVisuals
        {
            /*`Атрибут [SerializeField]` используется
            для раскрытия приватных полей в инспекторе Unity для удобства настройки и конфигурирования. */
            [SerializeField] private ParticleSystem ParticleSystem;
            [Header("Settings")]
            [SerializeField] private float psEmittedMax = 50;
            [SerializeField] private float psEmittedMin = 0;
            [SerializeField] private float visualizationLerpRate = 0.25f;
            [Header("CurrVal")]
            [SerializeField] private float psEmittedCurr = 0;
            [SerializeField] private float psEmittedCurrNormalized = 0;


            public void VisualiseThrust(float inputMove)
            {
                var emission = ParticleSystem.emission;

                /* Строка использует функцию `Mathf.Lerp` в Unity для плавной
                интерполяции (или смешивания) между текущим значением `psEmittedCurrNormalized` и
                значением `inputMove` с течением времени. */
                psEmittedCurrNormalized = Mathf.Lerp(psEmittedCurrNormalized, inputMove, visualizationLerpRate);
                /* Строка вычисляет текущее значение излучаемых частиц (`psEmittedCurr`) на основе максимально возможного
                максимально возможного значения испускаемых частиц (`psEmittedMax`) и нормализованного текущего значения
                (`psEmittedCurrNormalized`). */
                psEmittedCurr = psEmittedMax * psEmittedCurrNormalized;

                /* Строка задает скорость выброса со временем для компонента ParticleSystem.
                Функция `Mathf.Max` используется для того, чтобы скорость выброса всегда была не меньше
                значения `psEmittedCurr`. Это означает, что ParticleSystem будет испускать частицы со скоростью
                скоростью, равной или большей, чем текущее значение `psEmittedCurr`. */
                emission.rateOverTime = Mathf.Max(psEmittedCurr, psEmittedCurr);
            }
        }
        [SerializeField] private float moveSpeed = 100f;
        [SerializeField] private List<EngineVisuals> engineVisuals = new List<EngineVisuals>();

        public Vector3 Thrust(float inputMove)
        {
            var calculatedThrust = inputMove * moveSpeed;
            VisualiseThrust(inputMove);
            return -transform.forward * calculatedThrust * Time.deltaTime;
        }

        private void VisualiseThrust(float inputMove)
        {
            foreach (var ev in engineVisuals) 
            {
                ev.VisualiseThrust(inputMove);
            }
        }
    }
}