using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public class ShipHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;
        public float Health => health;

        private void Start()
        {

        }

        public void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender)
        {
            throw new System.NotImplementedException();
        }
    }
}