﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Ploeh.AutoFixture.Kernel;
using Rhino.Mocks;

namespace Ploeh.AutoFixture.AutoRhinoMock.UnitTest
{
    public class AutoRhinoMockCustomizationTest
    {
        [Fact]
        public void SutImplementsICustomization()
        {
            // Fixture setup
            // Exercise system
            var sut = new AutoRhinoMockCustomization();
            // Verify outcome
            Assert.IsAssignableFrom<ICustomization>(sut);
            // Teardown
        }

        [Fact]
        public void CustomizeNullFixtureThrows()
        {
            // Fixture setup
            var sut = new AutoRhinoMockCustomization();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Customize(null));
            // Teardown
        }

        [Fact]
        public void CustomizeAddsAppropriateResidueCollectors()
        {
            // Fixture setup
            var residueCollectors = new List<ISpecimenBuilder>();
            var fixtureStub = MockRepository.GenerateMock<IFixture>();
            fixtureStub.Stub(c => c.ResidueCollectors).Return(residueCollectors);

            var sut = new AutoRhinoMockCustomization();
            // Exercise system
            sut.Customize(fixtureStub);
            // Verify outcome
            var postprocessor = residueCollectors.OfType<RhinoMockPostprocessor>().Single();
            var ctorInvoker = Assert.IsAssignableFrom<ConstructorInvoker>(postprocessor.Builder);
            Assert.IsAssignableFrom<RhinoMockConstructorQuery>(ctorInvoker.Query);
            // Teardown
        }
    }
}
