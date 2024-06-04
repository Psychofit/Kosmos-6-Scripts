using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public class GameAgent : MonoBehaviour
    {
        public enum Fraction
        {
            Player,
            Alians,
            CosmoDesants
        }

        public Fraction ShipFraction;
    }
}