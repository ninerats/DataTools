using System.Diagnostics;
using System.IO;
using Craftsmaneer.DataTools.IO;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test.IO
{
    [TestFixture]
    public class ImportingTablesTest
    {

        [Test]
        public void ImportEntireDatabase()
        {
            var datSer = new DataTableSerializer(DataSerTestHelper.DataDiffConnectionString);
            var paths = Directory.GetFiles("FolderDTC");
            var sw = new Stopwatch();
            sw.Start();
            var result = datSer.ImportTables(paths);
            DataSerTestHelper.AssertResult(result);
            sw.Stop();
            Debug.WriteLine(sw);
            
        }
    }
}
