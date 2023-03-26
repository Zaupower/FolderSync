using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSyncUnitTests.Scenarios
{
    [SetUpFixture]
    internal class SetUpFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {           
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
          
        }
    }
}