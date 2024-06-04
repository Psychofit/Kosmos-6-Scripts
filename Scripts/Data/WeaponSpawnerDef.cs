using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Cosmos_Six
{
    [CreateAssetMenu(fileName = "Def WS Default", menuName = "Deffinitions/Battle/WeaponSpawnerDef")]
    public class WeaponSpawnerDef : ScriptableObject
    {
        [SerializeField] private string Id = "";
        [SerializeField] private string Name = "";

        [Range(0f, 5f)]
        public float CooldownTimeTotal = 0.25f;

        private void OnEnable() 
        {
            if(Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}
