using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cosmos_Six
{
    public class ShipWeapon : MonoBehaviour
    {
        public Spaceship spacsShip;
        [SerializeField] private Rigidbody ShipRigidbody;

        public List<IWeapon> Weapons = new List<IWeapon>();

        public float maxDistanceToTarget = 250f;

        private void Awake()
        {
            if (spacsShip == null)
            {
                spacsShip = GetComponentInParent<Spaceship>();
            }
            if (ShipRigidbody == null)
            {
                ShipRigidbody = GetComponentInParent<Rigidbody>();
            }
        }

        [ContextMenu("InitWeapons")]
        public void InitWeapons()
        {
            Weapons = GetComponentsInChildren<IWeapon>().ToList();
            foreach (var weapon in Weapons)
            {
                weapon.Initialize(new DataWeaponExtrinsic() { ShipRigidbody = ShipRigidbody, GameAgent = spacsShip.ShipAgent});
            }
        }

        private void OnEnable()
        {
            InitWeapons();
            spacsShip.IInputShipWeapons.OnAttackInput += FireWeapons;
        }

        private void OnDisable()
        {
            spacsShip.IInputShipWeapons.OnAttackInput -= FireWeapons;
        }

        public void FireWeapons()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out hit, maxDistanceToTarget))
            {
                foreach (var weapon in Weapons)
                {
                    weapon.FireWeapon(hit.point);
                }
            }
            else
            {
                foreach (var weapon in Weapons)
                {
                    weapon.FireWeapon(ray.origin + ray.direction * maxDistanceToTarget);
                }
            }
        }
    }
}