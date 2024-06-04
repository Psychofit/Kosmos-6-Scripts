using System.Collections;
using System.Collections.Generic;
using IG;
using UnityEngine;
using UnityEngine.Pool;

namespace Cosmos_Six
{
    public class FactoryFlyweight : SingletonManager<FactoryFlyweight>
    {
        [SerializeField] private bool _coolectionCheck = true;
        [SerializeField] private int _defaultCapacity = 50;
        [SerializeField] private int _maxSize = 150;

        private Dictionary<FlyweightDefType, IObjectPool<Flyweight>> _pools = 
            new Dictionary<FlyweightDefType, IObjectPool<Flyweight>>();
        

        public Flyweight Spawn(FlyweightDefinition definition)
        {
            return GetPoolForDefinition(definition)?.Get();
        }

        public Flyweight Spawn(FlyweightDefinition definition, Vector3 position, Quaternion rotation)
        {
            var flyweight = GetPoolForDefinition(definition)?.Get();
            flyweight.transform.position = position;
            flyweight.transform.rotation = rotation;

            return flyweight;

        }


        public void ReturnToPool(Flyweight flyweight)
        {
            GetPoolForDefinition(flyweight.Definition)?.Release(flyweight);
        }

        private IObjectPool<Flyweight> GetPoolForDefinition(FlyweightDefinition definition)
        {
            IObjectPool<Flyweight> pool;

            if (_pools.TryGetValue(definition.DefinitionType, out pool))
                return pool;

            pool = new ObjectPool<Flyweight>(
                definition.Create,
                definition.OnGet,
                definition.OnRelease,
                definition.OnDestroyPooledObject,
                _coolectionCheck,
                _defaultCapacity,
                _maxSize
                );

            _pools.Add(definition.DefinitionType, pool);

            return pool;
        }
    }
}
