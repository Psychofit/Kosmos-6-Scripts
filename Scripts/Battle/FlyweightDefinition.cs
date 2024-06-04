using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmos_Six
{
    public enum FlyweightDefType { 
        ShotGun, ShotPlasma
    };

    public abstract class FlyweightDefinition : ScriptableObject
    {
        [SerializeField] public string Id = "";
        [TextArea]
        [SerializeField] public string Name = "";

        public FlyweightDefType DefinitionType;
        public GameObject DefinitionPrefab;

        public abstract Flyweight Create();

        public virtual void OnGet(Flyweight flyweight)
        {
            flyweight.gameObject.SetActive(true);
        }
        public virtual void OnRelease(Flyweight flyweight)
        {
            flyweight.gameObject.SetActive(false);
        }
        public virtual void OnDestroyPooledObject(Flyweight flyweight)
        {
            Destroy(flyweight.gameObject);
        }

        protected void OnValidate()
        {
            if(Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }

    }
}
