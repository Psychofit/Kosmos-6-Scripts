using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public class ShipMovement : MonoBehaviour
    {
        /* Атрибут `[Header(«MovementMult»)]` используется для визуальной группировки и маркировки следующих
        сериализованных полей в инспекторе Unity под заголовком с указанным именем. В данном
        случае он группирует поля, связанные с множителями движения для скоростей вращения по тангажу, рысканию, крену
        и общей скорости движения. */
        [Header("MovementMult")]
        [SerializeField] private float pitchrotationSpeed = 125000f;
        [SerializeField] private float yawrotationSpeed = 125000f;
        [SerializeField] private float rollrotationSpeed = 125000f;
        [SerializeField] private float moveSpeed = 125000f;


        [Header("DragMult")]
        [Range(0.5f, 50f)]
        [SerializeField] private float ProportionalAngularDrag = 5f; //Пропорциональное торможение вращения
        [Range(10f, 1000f)]
        [SerializeField] private float ProportionalDrag = 100f;

       
        [Header("Links")]
        public Spaceship Spaceship;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private List<Engine> Engines = new List<Engine>(); // Список двигателей корабля

        void Start()
        {

        }

        void Update()
        {
            /* Метод `Turn` отвечает за вращение корабля на основе входных значений для
            тангажа, рысканья и крена. Он вычисляет момент, необходимый для каждой оси вращения (тангаж,
            рысканье, крен) и прикладывает его к телу корабля. */
            Turn(Spaceship.IInputShipMovement.CurrentInputRotatePitch, Spaceship.IInputShipMovement.CurrentInputRotateYaw, Spaceship.IInputShipMovement.CurrentInputRotateRoll);
            Move(Spaceship.IInputShipMovement.CurrentInputMove);
        }

        private void Turn(float inputPitch, float inputYaw, float inputRoll)
        {
            if(!Mathf.Approximately(0f, inputPitch))
            {
                _rigidbody.AddTorque(-transform.right * inputPitch * pitchrotationSpeed * Time.fixedDeltaTime);
            }
            if (!Mathf.Approximately(0f, inputYaw))
            {
                _rigidbody.AddTorque(transform.up * inputYaw * yawrotationSpeed * Time.fixedDeltaTime);
            }
            if (!Mathf.Approximately(0f, inputRoll))
            {
                _rigidbody.AddTorque(-transform.forward * inputRoll * rollrotationSpeed * Time.fixedDeltaTime);
            }

            _rigidbody.AddForce(-_rigidbody.angularVelocity * ProportionalAngularDrag * Time.fixedDeltaTime);
        }

        private void Move(float inputMove)
        {
            Vector3 resultingThrust = new Vector3();
            /*`foreach (var Engine in Engines)` выполняет итерацию по каждому объекту `Engine` в
            списке `Engines`. Для каждого объекта `Engine` он вызывает метод `Thrust` этого
            двигателя, передавая в качестве параметра `inputMove`. Результат выполнения метода `Engine.Thrust(inputMove)`
            добавляется к вектору `resultingThrust`. Этот процесс повторяется для каждого двигателя в
            списка, накапливая суммарный вклад тяги всех двигателей в вектор
            вектор `resultingThrust`.*/
            foreach (var Engine in Engines)
            {
                resultingThrust = resultingThrust + Engine.Thrust(inputMove);
            }
            _rigidbody.AddForce(resultingThrust * moveSpeed * Time.fixedDeltaTime);
            _rigidbody.AddForce(-_rigidbody.velocity * ProportionalDrag * Time.fixedDeltaTime);
        }
    }
}