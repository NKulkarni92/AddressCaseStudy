using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressCaseStudy.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;

namespace AddressCaseStudy.Controller.Tests
{
    [TestClass()]
    public class AddressControllerTests
    {
        [TestMethod()]
        public void untiTest()
        {
            // Arrange
            var controller = new AddressController();
            string[] resultset =new string[]{"26 Curtis Avenue, Taren Point NSW 2229","19 Allawah Street, Blacktown NSW 2148", "3 Woodgates Road, Buchan VIC 3885", "75 Old Waratah Road, Fish Creek VIC 3959"};
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            List<KeyValuePair<string, int>> distance = controller.topFiveAddresses("Victoria Australia", resultset);

            // Assert
            if (distance != null)
            {
                bool condition = true;
                Assert.IsTrue(condition);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}