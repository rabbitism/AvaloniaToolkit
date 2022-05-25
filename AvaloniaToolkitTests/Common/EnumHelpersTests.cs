using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvaloniaToolkit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaToolkit.Options;

namespace AvaloniaToolkit.Common.Tests
{
    [TestClass()]
    public class EnumHelpersTests
    {
        [TestMethod()]
        public void GetDisplayNameTest()
        {
            string s = EnumHelpers.GetDisplayName(ViewModelFlavor.ReactiveUI);
            Assert.AreEqual("ReactiveUI (ReactiveObject)", s);
        }
    }
}