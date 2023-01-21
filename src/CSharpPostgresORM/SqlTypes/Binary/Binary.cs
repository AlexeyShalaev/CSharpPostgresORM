using CSharpPostgresORM.SqlTypes.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPostgresORM.SqlTypes.Binary
{
    public class Binary : ISqlType<byte[]>
    {
        public byte[] Value { get; set; }

        public static readonly string SqlTypeName = "BINARY";

        public static implicit operator Binary(byte[] value)
        {
            return new Binary { Value = value };
        }
    }
}
