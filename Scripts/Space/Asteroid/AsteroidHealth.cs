using System.Collections;
using System.Collections.Generic;
using IG;
using UnityEngine;
namespace Cosmos_Six
{

    public class AsteroidHealth : MonoBehaviour, IDamageable
    {

        public float Health { get; set; }

        [SerializeField] private GameObject PrefabEffectDestr; //Префаб эффекта при уничтожении астероида.

        [SerializeField] private GameObject PrefabAsteroidDivision; //Префаб для разделения астероида.

        private bool Destroyed = false; // Флаг, указывающий, был ли астероид уничтожен

        public int DivisionCounter = 2; //Счетчик делений астероида

        public void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender) //Метод для получения урона. При уменьшении здоровья астероида проверяет, необходимо ли его разделить.
        {
            Health -= damageAmount;
            if (Health <= 0 && !Destroyed)
            {
                if (PrefabEffectDestr)
                {
                    Instantiate(PrefabEffectDestr, transform.position, Quaternion.identity);
                }

                if (PrefabAsteroidDivision)
                {
                    if (DivisionCounter > 0)
                    {
                        Vector3 shard1Pos = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                        transform.position.y + Random.Range(-1f, 1f),
                        transform.position.z + Random.Range(-1f, 1f));
                        Vector3 shard2Pos = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                        transform.position.y + Random.Range(-1f, 1f),
                        transform.position.z + Random.Range(-1f, 1f));
                        var s1 = Instantiate(PrefabAsteroidDivision, shard1Pos + PrefabAsteroidDivision.transform.localScale, Quaternion.identity);
                        var s2 = Instantiate(PrefabAsteroidDivision, shard2Pos - PrefabAsteroidDivision.transform.localScale, Quaternion.identity);

                        s1.GetComponent<AsteroidHealth>().DivisionCounter = DivisionCounter--;
                        s2.GetComponent<AsteroidHealth>().DivisionCounter = DivisionCounter--;
                    }
                }
                ManagerScore.Instance.AddScore(1);

                Destroyed = true;
                Destroy(gameObject);

            }
        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender) //Метод для получения исцеления
        {
            throw new System.NotImplementedException();
        }

        void Start()
        {

        }
    }
}