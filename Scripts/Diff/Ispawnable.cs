using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public interface IWeaponSpawnable
    {
        void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic);
    }
}
