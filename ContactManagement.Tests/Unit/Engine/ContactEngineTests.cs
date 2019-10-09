namespace ContactManagement.Tests.Unit.Engine
{
    using ContactManagement.Domain.Interfaces;
    using ContactManagement.Domain.Models;
    using ContactManagement.Engine.Engines;
    using ContactManagement.Storage.Interfaces;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Test Cases for Contact Repository 
    /// </summary>
    public class ContactEngineTests
    {
        #region Private Fields
        private IContactEngine _contactEngine;
        private readonly Mock<IContactRepository> _mockContactRepository;

        #endregion

        #region Constructor
        public ContactEngineTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
        }
        #endregion

        #region  Test cases


        [Fact]

        public async Task ContactEngine_ErrorIf_NullRepository()
        {
            //Act
            Exception ex = Assert.Throws<ArgumentNullException>(() => new ContactEngine(null));

            //Assert
            Assert.Contains("contactRepository", ex.Message);
        }

        [Fact]
        public async Task GetContactByIdAsyncTest_Success()
        {
            //Arrange
            var contact = GetContact();
            _mockContactRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(contact));
            _contactEngine = new ContactEngine(_mockContactRepository.Object);

            //Act
            var result = await _contactEngine.GetContactByIdAsync(1).ConfigureAwait(false);

            //Assert
            Assert.Equal(result, contact);
        }

        [Fact]
        public async Task GetAllActiveContactsAsyncTest_Success()
        {
            //Arrange
            var contacts = GetContacts();
            _mockContactRepository.Setup(r => r.GetAllActiveContactsAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(contacts));
            _contactEngine = new ContactEngine(_mockContactRepository.Object);
            var index = 0;
            var pageSize = 10;
            //Act
            var result = await _contactEngine.GetAllActiveContactsAsync(index, pageSize).ConfigureAwait(false);

            //Assert
            Assert.True(result.Count() <= contacts.Count());
        }

        [Fact]
        public async Task GetAllInActiveContactsAsyncTest_Success()
        {
            //Arrange
            var contacts = GetContacts();
            _mockContactRepository.Setup(r => r.GetAllInActiveContactsAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(contacts));
            _contactEngine = new ContactEngine(_mockContactRepository.Object);
            var index = 0;
            var pageSize = 10;
            //Act
            var result = await _contactEngine.GetAllInActiveContactsAsync(index, pageSize).ConfigureAwait(false);

            //Assert
            Assert.True(result.Count() <= contacts.Count());
        }

        [Fact]
        public async Task AddContactAsyncTest_Success()
        {
            //Arrange
            var contact = GetContact();
            _mockContactRepository.Setup(r => r.InsertAsync(It.IsAny<Contact>())).Returns(Task.FromResult(true));
            _contactEngine = new ContactEngine(_mockContactRepository.Object);

            //Act
            var result = await _contactEngine.AddContactAsync(contact).ConfigureAwait(false);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteContactAsyncTest_Success()
        {
            //Arrange
            var contact = GetContact();
            _mockContactRepository.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _contactEngine = new ContactEngine(_mockContactRepository.Object);

            //Act
            var result = await _contactEngine.DeleteContactAsync(contact.ContactId).ConfigureAwait(false);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateContactAsyncTest_Success()
        {
            //Arrange
            var contact = GetContact();
            _mockContactRepository.Setup(r => r.UpdateAsync(It.IsAny<Contact>())).Returns(Task.FromResult(true));
            _contactEngine = new ContactEngine(_mockContactRepository.Object);

            //Act
            var result = await _contactEngine.UpdateContactAsync(contact).ConfigureAwait(false);

            //Assert
            Assert.True(result);
        }

        #endregion


        #region Private Methods
        private static Contact GetContact()
        {
            return new Contact { ContactId = 1, FirstName = "Fake0", LastName = "Test0", IsActive = false, Email = "test0@fake.com" };
        }

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
