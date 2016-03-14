using Craftsmaneer.DataTools.Common.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class Scratch
    {
        [Test, Ignore("already determined")]
        public void CanInnerBeNullTest()
        {
            var x = new Exception("text", null);
            Assert.IsNotNull(x);
        }

        [Test]
        public void WhatsUpWithTypeOf()
        {
            var x = new Dictionary<Type, SqlDbTypeGroup>()
            {
                {typeof (Byte), SqlDbTypeGroup.Integer},
                {typeof (Int32), SqlDbTypeGroup.Integer},
                {typeof (Int16), SqlDbTypeGroup.Integer},
                {typeof (Int64), SqlDbTypeGroup.Integer},
                {typeof (Char), SqlDbTypeGroup.String},
                {typeof (String), SqlDbTypeGroup.String},
                {typeof (DateTime), SqlDbTypeGroup.Date},
                {typeof (DateTimeOffset), SqlDbTypeGroup.Date},
                {typeof (TimeSpan), SqlDbTypeGroup.Date},
             //   {typeof (float), SqlDbTypeGroup.Floating},
                {typeof (Decimal), SqlDbTypeGroup.Floating},
                {typeof (Single), SqlDbTypeGroup.Floating},
                {typeof (Double), SqlDbTypeGroup.Floating},
                {typeof (Boolean), SqlDbTypeGroup.Boolean},
                {typeof (Guid), SqlDbTypeGroup.String},
                {typeof (byte[]), SqlDbTypeGroup.Binary},

            };
        }
    }
}
