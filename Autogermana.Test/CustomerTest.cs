using Autogermana.Application.Interfaces;
using Autogermana.Application.Services;
using Autogermana.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Autogermana.Test
{
    public class CustomerTest
    {
        [Fact]
        public async Task Test1CustomerEmpty()
        {
            var repository = new Mock<IPowerAutomateRepository>();
            ICustomerService customerService = new CustomerService(repository.Object);

            Func<Task> a = () => customerService.GetCustomerById("    ", CancellationToken.None);

            await a.Should().ThrowAsync<ArgumentException>();
        }


        [Fact]
        public async Task Test2CustomerExist()
        {
            var repository = new Mock<IPowerAutomateRepository>();


            repository.Setup(x => x.GetCustomerEntity("1000", It.IsAny<CancellationToken>())).
                ReturnsAsync(new CustomerEntity(
                    firstname: "Cristian",
                    lastName: "Gomez",
                    email: "cdgr16@gmail.com",
                    age: 25,
                    city: "Bogota"

                    ));

            ICustomerService customerService = new CustomerService(repository.Object);

            var result = await customerService.GetCustomerById("1000", CancellationToken.None);

            result.firstname.Should().Be("Cristian");
            result.city.Should().Be("Bogota");

        }
    }
}