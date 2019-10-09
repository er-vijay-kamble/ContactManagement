namespace ContactManagement.Tests.Unit.Repository
{
    using ContactManagement.Domain.Models;
    using ContactManagement.Storage.DbConfiguration;
    using ContactManagement.Storage.Interfaces;
    using ContactManagement.Storage.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Test Cases for Contact Repository 
    /// </summary>
    public class ContactRepositoryTests
    {
        #region Private Fields
        private IContactRepository contactRepository;
        private readonly Mock<DbSet<Contact>> _mockSet;
        private readonly Mock<ManagementContext> _mockContext;
        #endregion

        #region Constructor
        public ContactRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Contact>>();
            _mockContext = new Mock<ManagementContext>();
        }
        #endregion

        #region  Test cases

        [Fact]
        public async Task GetAllActiveContactsTest()
        {
            //Arrange
            var source = GetContacts();
            var data = source.AsQueryable();
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockContext.Setup(m => m.Contacts).Returns(_mockSet.Object);
            var index = 0;
            var pageSize = 10;

            //Act
            contactRepository = new ContactRepository(_mockContext.Object);
            var result = await contactRepository.GetAllActiveContactsAsync(index, pageSize).ConfigureAwait(false);

            //Assert
            Assert.True(result.Count <= pageSize);
            Assert.True(result.All(contact => contact.IsActive));
        }

        [Fact]
        public async Task GetAllInActiveContactsTest()
        {
            //Arrange
            var source = GetContacts();
            var data = source.AsQueryable();
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockContext.Setup(m => m.Contacts).Returns(_mockSet.Object);
            var index = 0;
            var pageSize = 10;

            //Act
            contactRepository = new ContactRepository(_mockContext.Object);
            var result = await contactRepository.GetAllInActiveContactsAsync(index, pageSize).ConfigureAwait(false);

            //Assert
            Assert.True(result.Count <= pageSize);
            Assert.True(result.All(contact => !contact.IsActive));
        }

        #endregion

        #region Private Methods
        private static ICollection<Contact> GetContacts()
        {
            return new List<Contact>()
            {
                new Contact { ContactId=1, FirstName="Fake0", LastName="Test0", IsActive=false, Email="test0@fake.com" },
                new Contact { ContactId=2, FirstName="Fake1", LastName="Test1", IsActive=true, Email="test1@fake.com" },
                new Contact { ContactId=3, FirstName="Fake2", LastName="Test2", IsActive=true, Email="test2@fake.com" },
                new Contact { ContactId=3, FirstName="Fake3", LastName="Test3", IsActive=true, Email="test3@fake.com" },
                new Contact { ContactId=3, FirstName="Fake4", LastName="Test4", IsActive=true, Email="test4@fake.com" },
                new Contact { ContactId=3, FirstName="Fake5", LastName="Test5", IsActive=true, Email="test5@fake.com" },
                new Contact { ContactId=3, FirstName="Fake6", LastName="Test6", IsActive=true, Email="test6@fake.com" },
                new Contact { ContactId=3, FirstName="Fake7", LastName="Test7", IsActive=true, Email="test7@fake.com" },
                new Contact { ContactId=3, FirstName="Fake8", LastName="Test8", IsActive=true, Email="test8@fake.com" },
                new Contact { ContactId=3, FirstName="Fake9", LastName="Test9", IsActive=true, Email="test9@fake.com" },
                new Contact { ContactId=3, FirstName="Fake10", LastName="Test10", IsActive=true, Email="test10@fake.com" },
                new Contact { ContactId=3, FirstName="Fake11", LastName="Test11", IsActive=true, Email="test11@fake.com" },
                new Contact { ContactId=3, FirstName="Fake12", LastName="Test12", IsActive=true, Email="test12@fake.com" },
            };
        }
        #endregion
    }
}
