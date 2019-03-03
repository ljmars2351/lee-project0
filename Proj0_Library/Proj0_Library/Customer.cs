using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library
{
    public class Customer
    {
        private string _fName;
        private string _lName;

        public int CustId { get; set; }

        public string FName
        {
            get => _fName;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Name cannot be empty", nameof(value));
                }

                _fName = value ?? throw new ArgumentNullException("Please enter a valid name", nameof(value));
            }
        }

        public string LName
        {
            get => _lName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                _lName = value ?? throw new ArgumentNullException("Please enter a valid name");
            }
        }

        public DateTime DateOfBirth
        {
            get; set;
        }

        public int Loc { get; set; } = 1;
    }
}
