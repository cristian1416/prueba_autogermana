using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Domain.Entities
{
    public class CustomerEntity
    {
        public string Firstname { get; }
        public string LastName { get; }
        public string Email { get; }
        public int Age { get; }
        public string City { get; }

        public CustomerEntity(string firstname, string lastName, string email, int age, string city)
        {
            Firstname = firstname;
            LastName = lastName;
            Email = email;
            Age = age;
            City = city;
        }
    }
}
