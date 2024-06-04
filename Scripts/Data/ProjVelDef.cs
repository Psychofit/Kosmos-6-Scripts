using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Cosmos_Six
{
    [CreateAssetMenu(fileName = "Def Proj Vel Default", menuName = "Deffinitions/Battle/ProjVelDef")]
    public class ProjVelDef : ScriptableObject
    {
        [SerializeField] private string Id = "";
        [SerializeField] private string Name = "";

        [SerializeField] public float Velocity = 300f;
        
        private void OnEnable()
        {
            if (Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}

