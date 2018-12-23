using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class SyntaxMapper
    {
        private SyntaxMapper()
        {
            throw new System.NotImplementedException();
        }

        public SyntaxMapper instance
        {
            get => default(SyntaxMapper);
            set
            {
            }
        }

        public DBSyntax getSyntaxFromConnection
        {
            get => default(DBSyntax);
            set
            {
            }
        }
    }
}