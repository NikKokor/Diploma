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
    public class Form2Tests
    {
        [TestMethod()]
        public void getNameTehTest_notExist()
        {
            Editor.Form2 form = new Form2("test", "new");
            bool exepted = false;
            bool actual = form.getNameTeh("-1");

            Assert.AreEqual(exepted, actual);
        }

        [TestMethod()]
        public void getNameTehTest_isExist()
        {
            Editor.Form2 form = new Form2("test", "new");
            bool exepted = true;
            bool actual = form.getNameTeh("39");

            Assert.AreEqual(exepted, actual);
        }

        [TestMethod()]
        public void getTehnologyTest_notExist()
        {
            Editor.Form2 form = new Form2("test", "new");
            bool exepted = false;
            bool actual = form.getNameTeh("-1");

            Assert.AreEqual(exepted, actual);
        }

        [TestMethod()]
        public void getTehnologyTest_isExist()
        {
            Editor.Form2 form = new Form2("test", "new");
            bool exepted = true;
            bool actual = form.getNameTeh("39");

            Assert.AreEqual(exepted, actual);
        }
    }
}