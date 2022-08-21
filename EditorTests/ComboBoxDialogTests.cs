using Microsoft.VisualStudio.TestTools.UnitTesting;
using Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Tests
{
    [TestClass()]
    public class ComboBoxDialogTests
    {
        [TestMethod()]
        public void getOsobTest_empty()
        {
            Editor.ComboBoxDialog combo = new ComboBoxDialog("");
            bool exepted = false;
            bool actual = combo.getOsob("");

            Assert.AreEqual(exepted, actual);
        }
    }
}