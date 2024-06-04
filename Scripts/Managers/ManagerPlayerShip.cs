using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace IG
{
    namespace Cosmos_Six
    {
        public class ManagerPlayerShip : MonoBehaviour
        {
            public int CurrentShipID;
            public Transform ShipVisuals;
            public GameObject CurrentShip;

            [SerializeField] private List<GameObject> ShipsPrefabs = new List<GameObject>();
            void Update()
            {
                if (Input.GetButtonDown("ShipChange"))
                {
                    ChangeShipToNext();
                }
            }

            void ChangeShipToNext()
            {
                CurrentShipID++;
                if (CurrentShipID == ShipsPrefabs.Count)
                {
                    CurrentShipID = 0;
                }
                ChangeShip(CurrentShipID);
            }


            void ChangeShip(int id)
            {
                Destroy(CurrentShip);

                CurrentShip = Instantiate(ShipsPrefabs[CurrentShipID], ShipVisuals);
            }
        }
    }
}