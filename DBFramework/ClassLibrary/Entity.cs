using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ClassLibrary
{
    public class Entity: DynamicObject
    {
        public Entity(Connector connector)
        {
            throw new System.NotImplementedException();
        }

        private Dictionary<string, List<EntityProperty>> listEntityProperties
        {
            get => default(Dictionary<string, List<EntityProperty>>);
            set
            {
            }
        }

        private Connector connector
        {
            get => default(Connector);
            set
            {
            }
        }

        private Dictionary<string, Type> listType
        {
            get => default(Dictionary<string, Type>);
            set
            {
            }
        }

        public bool TryGetMember()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, Type> getListType()
        {
            throw new System.NotImplementedException();
        }
    }
}