using System;
using System.Threading.Tasks;
using Collector.Common.Azure.Webjobs.Extensions.Exceptions;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;

namespace Collector.Common.Azure.Webjobs.Extensions.Tests
{
    internal class BrokeredMessageExtensionsTests
    {
        [Test]
        public void CreateBrokeredMessage_SetsProps()
        {
            var model = new TestClass();

            var actual = model.CreateBrokeredMessage();

            Assert.That(actual.Properties["FullNameType"], Is.EqualTo(typeof(TestClass).FullName));
            Assert.That(actual.Properties["NameType"], Is.EqualTo(typeof(TestClass).Name));
            Assert.That(actual.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void Parse_ValidModel_Parses()
        {
            var expected = new TestClass { Value = Guid.NewGuid().ToString() };

            var actual = expected.CreateBrokeredMessage().Parse<TestClass>();

            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        }

        [Test]
        public async Task ParseAsync_ValidModel_Parses()
        {
            var expected = new TestClass { Value = Guid.NewGuid().ToString() };

            var actual = await expected.CreateBrokeredMessage().ParseAsync<TestClass>();

            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        }

        [Test]
        public void Parse_AnotherModel_ThrowsException()
        {
            var sut = new TestClass { Value = Guid.NewGuid().ToString() }.CreateBrokeredMessage();

            Assert.That(() => sut.Parse<TestClass2>(), Throws.TypeOf<ParseException>());
        }

        [Test]
        public void ParseAsync_AnotherModel_ThrowsException()
        {
            var sut = new TestClass { Value = Guid.NewGuid().ToString() }.CreateBrokeredMessage();

            Assert.That(async () => await sut.ParseAsync<TestClass2>(), Throws.TypeOf<ParseException>());
        }

        [Test]
        public void Parse_EmptyMessage_ThrowsException()
        {
            var sut = new BrokeredMessage();

            Assert.That(() => sut.Parse<TestClass2>(), Throws.TypeOf<ParseException>());
        }

        [Test]
        public void ParseAsync_EmptyMessage_ThrowsException()
        {
            var sut = new BrokeredMessage();

            Assert.That(async () => await sut.ParseAsync<TestClass2>(), Throws.TypeOf<ParseException>());
        }

        private class TestClass
        {
            public string Value { get; set; }
        }

        private class TestClass2
        {
            public string Value { get; set; }
        }
    }
}