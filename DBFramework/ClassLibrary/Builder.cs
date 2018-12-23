using System;
using System.Reflection.Emit;

namespace ClassLibrary
{
    public class Builder
    {
        public Builder(string className)
        {
            throw new System.NotImplementedException();
        }

        public object CreateObject(string[] propertyName, Type[] propertyType)
        {
            throw new System.NotImplementedException();
        }

        private TypeBuilder CreateClass()
        {
            throw new System.NotImplementedException();
        }

        private void CreateConstructor(TypeBuilder typeBuilder)
        {
            throw new System.NotImplementedException();
        }

        private void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            throw new System.NotImplementedException();
        }
    }
}