using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public class Spaceship : MonoBehaviour
    {
        public GameAgent ShipAgent;
        public IInputShipMovement IInputShipMovement;
        public IInputShipWeapons IInputShipWeapons;


        private void OnEnable()
        {
            if (ShipAgent == null)
            {
                ShipAgent = GetComponent<GameAgent>();
            }
            if (IInputShipMovement == null)
            {
                IInputShipMovement = GetComponent<IInputShipMovement>();
            }
            if (IInputShipWeapons == null)
            {
                IInputShipWeapons = GetComponent<IInputShipWeapons>();
            }
        }
    }
}