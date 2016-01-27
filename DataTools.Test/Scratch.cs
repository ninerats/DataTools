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
        [Test]
        public void CanInnerBeNullTest()
        {
            var x = new Exception("text", null);
            Assert.IsNotNull(x);
        }
    }
}
