using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour, IWeapon
    {
        public float maxDist = 100f; //Максимальная дистанция, на которую может дотянуться лазерный луч.
        public float damageAmount = 5f; //Количество урона, наносимого цели лазером.
        private DataWeaponExtrinsic _dataWeaponExtrinsic; //Внешние данные для оружия.


        private Coroutine coroutineFiring; //Корутина для управления процессом стрельбы.
        private WaitForSeconds waitForFiring; //Задержка перед следующим выстрелом.
        [SerializeField] private float waitForFiringDelay = 0.1f;


        [Header("VFX")]
        [SerializeField] private float lineRenAnimSpeed = 1f;
        [SerializeField] private float lineRenAnimDelta = 0f;

        [Header("Inner working")]
        [SerializeField] private bool canFire;

        [Header("Links")]
        [SerializeField] private LineRenderer lineRenderer; // Ссылка на компонент отрисовки линии.
        private ShipWeapon shipWeapon; // Ссылка на компонент корабля с оружием

        public List<IDamageable> TargetsHit = new List<IDamageable>(); //Список целей, по которым прошел лазерный луч.

        private void Awake()
        {
            if(shipWeapon == null)
            {
                shipWeapon = GetComponentInParent<ShipWeapon>();
            }
            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }
        }

        private void Start() //Инициализирует параметры лазера.
        {
            waitForFiring = new WaitForSeconds(waitForFiringDelay);
            lineRenderer.enabled = false;
            canFire = true;
        }

        private void Update() // Обновление отображения лазерного луча
        {

            if (!lineRenderer.enabled) return;
            lineRenderer.SetPosition(0, transform.position);

            lineRenAnimDelta += Time.deltaTime;
            if (lineRenAnimDelta > 1f)
            {
                lineRenAnimDelta = 0f;
            }

            lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(lineRenAnimDelta * lineRenAnimSpeed, 0f));
        }

        public void Initialize(DataWeaponExtrinsic DataWeaponExtrinsic) //Инициализация данных для оружия.
        {
            _dataWeaponExtrinsic = DataWeaponExtrinsic;

        }

        public Vector3 FireWeapon(Vector3 targetPosition) //Выстрел лазером в указанную позицию.
        {
            /*Эта часть кода проверяет, может ли лазер стрелять. Если `canFire` равно false, то
            немедленно возвращает `Vector3.zero`, указывая на то, что лазер не может стрелять в данный момент.*/
            if (!canFire) return Vector3.zero;
            RaycastHit hitInfo;
            var direction = targetPosition - transform.position;
            /*Эта часть кода отвечает за проверку того, попадает ли лазерный луч в какую-либо цель
            в пределах максимального расстояния (`maxDist`).*/
            if(Physics.Raycast(transform.position, direction, out hitInfo, maxDist))
            {
                var targetHit = hitInfo.transform;
                /* Эта часть кода проверяет, попадает ли лазерный луч в цель на максимальном
                расстоянии (`maxDist`). Если луч попадает в цель, он извлекает объект цели
                и проверяет, реализует ли он интерфейс `IDamageable`. Если цель
                повреждаемой, он добавляет цель в список `TargetsHit`, наносит ей урон
                цели с помощью метода `Damage`, визуализирует стрельбу с указанием позиции цели,
                и возвращает позицию цели. */
                if(targetHit != null)
                {
                    var damageableHit = targetHit.GetComponent<IDamageable>();
                    if (damageableHit != null)
                    {
                        TargetsHit.Add(damageableHit);
                        Damage(damageAmount, targetHit.position, _dataWeaponExtrinsic.GameAgent);

                    }
                    VisualiseFiring(targetHit.position);
                    return targetHit.position;
                }
            }
            VisualiseFiring(transform.position + direction.normalized * maxDist);
            return targetPosition;
        }

        public void Damage(float damageAmount, Vector3 targetHitPosition, GameAgent sender) //Нанесение урона целям
        {
            /* Цикл `foreach` выполняет итерацию по каждому элементу списка `TargetsHit`, который
            содержит объекты, реализующие интерфейс `IDamageable`. Для каждого элемента в
            списка, он вызывает метод `ReceiveDamage` на целевом объекте, передавая ему значения
            `сумма урона`, `позиция попадания` и `отправитель` в качестве параметров. По сути, это
            наносит урон каждой цели в списке. */
            foreach(var targetHit in TargetsHit)
            {
                targetHit.ReceiveDamage(damageAmount, targetHitPosition, sender);

            }
            TargetsHit.Clear();
        }

        public void VisualiseFiring(Vector3 targetPosition) //Визуализация выстрела лазера.
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPosition);

            canFire = false;

            coroutineFiring = StartCoroutine(FiringCor());
        }

        /* Функция `FiringCor` - это корутин, который отвечает за задержку между выстрелами.*/
        private IEnumerator FiringCor()
        {
            yield return waitForFiring;

            canFire = true;
            yield return waitForFiring;
            if (canFire)
            {
                lineRenderer.enabled = false;
            }
        }
    }

}
