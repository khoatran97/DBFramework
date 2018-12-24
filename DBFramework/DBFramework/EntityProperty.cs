using System;

namespace DBFramework
{
    public class EntityProperty
    {
        public EntityProperty(string name, Type type)
        {
            this.propertyName = name;
            this.propertyType = type;
        }

        public string propertyName;

        public Type propertyType;
    }
}