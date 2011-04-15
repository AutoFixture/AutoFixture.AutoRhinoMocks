﻿using System;
using System.Reflection;
using Ploeh.TestTypeFoundation;
using Xunit;
using Ploeh.AutoFixture.Kernel;
using Xunit.Extensions;
using Rhino.Mocks;

namespace Ploeh.AutoFixture.AutoRhinoMock.UnitTest
{
    public class RhinoRhinoMockBuilderTest
    {
        [Fact]
        public void SutImplementsISpecimenBuilder()
        {
            // Fixture setup
            // Exercise system
            var sut = new RhinoMockBuilder();
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void InitializeWithNullSpecificationThrows()
        {
            // Fixture setup
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new RhinoMockBuilder(null));
            // Teardown
        }

        [Fact]
        public void SpecificationIsCorrectWhenInitializedWithSpecification()
        {
            // Fixture setup
            Func<Type, bool> expectedSpec = t => true;
            var sut = new RhinoMockBuilder(expectedSpec);
            // Exercise system
            Func<Type, bool> result = sut.MockableSpecification;
            // Verify outcome
            Assert.Equal(expectedSpec, result);
            // Teardown
        }

        [Fact]
        public void SpecificationIsNotNullWhenInitializedWithDefaultConstructor()
        {
            // Fixture setup
            var sut = new RhinoMockBuilder();
            // Exercise system
            var result = sut.MockableSpecification;
            // Verify outcome
            Assert.NotNull(result);
            // Teardown
        }

        [Fact]
        public void CreateWithNullContextThrows()
        {
            // Fixture setup
            var sut = new RhinoMockBuilder();
            var dummyRequest = new object();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Create(dummyRequest, null));
            // Teardown
        }

        [Fact]
        public void CreateWithNonTypeIsCorrect()
        {
            // Fixture setup
            var sut = new RhinoMockBuilder(t => true);

            var dummyContext = MockRepository.GenerateMock<ISpecimenContext>();
            // Exercise system
            Assembly request = typeof(RhinoMockBuilder).Assembly;
            var result = sut.Create(request, dummyContext);

            // Verify outcome
            var expectedResult = new NoSpecimen(request);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(1)]
        [InlineData(typeof(object))]
        [InlineData(typeof(string))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(DateTimeOffset))]
        public void CreateWithNonAbstractRequestReturnsCorrectResult(object request)
        {
            // Fixture setup
            var sut = new RhinoMockBuilder();
            var dummyContext = MockRepository.GenerateMock<ISpecimenContext>();
            // Exercise system
            var result = sut.Create(request, dummyContext);
            // Verify outcome
            var expectedResult = new NoSpecimen(request);
            Assert.Equal(expectedResult, result);
            // Teardown
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(1)]
        [InlineData(typeof(object))]
        [InlineData(typeof(string))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(DateTimeOffset))]
        public void CreateWithNonAbstractRequestReturnsNonMockedResult(object request)
        {
            // Fixture setup
            var sut = new RhinoMockBuilder();
            var dummyContext = MockRepository.GenerateMock<ISpecimenContext>();
            // Exercise system
            var result = sut.Create(request, dummyContext);
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() => result.GetMockRepository());
            // Teardown
        }

        [Fact]
        public void CreateGenericTypeWithNonDefaultConstructorIsCorrect()
        {
            // Fixture setup
            var sut = new RhinoMockBuilder();
            var dummyContext = MockRepository.GenerateMock<ISpecimenContext>();
            // Exercise system
            var result = sut.Create(typeof(GenericType<ConcreteType>), dummyContext);

            // Verify outcome
            Assert.Throws<InvalidOperationException>(() => result.GetMockRepository());
        }
    }
}
