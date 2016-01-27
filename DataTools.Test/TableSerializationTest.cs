using System.Data;
using Craftsmaneer.DataTools.Compare;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class TableSerializationTest
    {
        [Test, Ignore("test fails due to possilbe Fx bug: https://social.msdn.microsoft.com/Forums/en-US/ef796a01-4a93-4f78-9708-cfd18c45a14c/datatablereadxml-ignores-the-xmlspacepreserve-attribute-created-by-datatablewritexml?forum=adodotnetdataset")]
        public void PreserveSpaceTest()
        {
            var dts = new FolderDataTableSet("FolderDTC")
            {
                Id = "Preserve space test.",
                TableList = new List<string>() {"Production.Culture"}
            };
            var loadResult = dts.Load();
            Assert.IsTrue(loadResult.Success);
            var dt = dts["Production.Culture"];
            var row = dt.Rows[0];
            Assert.AreEqual("      ",row["CultureId"]);
        }

        [Test, Ignore("test fails due to possilbe Fx bug: https://social.msdn.microsoft.com/Forums/en-US/ef796a01-4a93-4f78-9708-cfd18c45a14c/datatablereadxml-ignores-the-xmlspacepreserve-attribute-created-by-datatablewritexml?forum=adodotnetdataset")]
        public void PreserveSpaceRawTest()
        {
            var dt = new DataTable();
            
            dt.ReadXml(@"FolderDTC\Production.Culture.xml");
            var row = dt.Rows[0];
            Assert.AreEqual("      ", row["CultureId"]);
        }
    }
}
