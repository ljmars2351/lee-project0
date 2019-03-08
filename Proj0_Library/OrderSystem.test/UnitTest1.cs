using OrderSystem.Library;
using System;
using Xunit;

namespace OrderSystem.test
{
    public class UnitTest1
    {
        Customer cust = new Customer();
        [Fact]
        public void Add_Customer_First_Name()
        {
            string name = "Lunk";
            cust.FName = name;
            Assert.True(cust.FName == name);
        }

        [Fact]
        public void Add_Customer_Last_Name()
        {
            string name = "Mars";
            cust.LName = name;
            Assert.True(cust.LName == name);
        }

        [Fact]
        public void Add_Customer_Date_Of_Birth()
        {

            DateTime dob = new DateTime(1994,05,29);
            cust.DateOfBirth = dob;
            Assert.True(cust.DateOfBirth == dob);
        }

    }
}
