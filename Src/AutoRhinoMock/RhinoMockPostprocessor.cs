﻿using System;
using Ploeh.AutoFixture.Kernel;
using Rhino.Mocks.Interfaces;

namespace Ploeh.AutoFixture.AutoRhinoMock
{
    public class RhinoMockPostprocessor : ISpecimenBuilder
    {
        private readonly ISpecimenBuilder builder;

        public RhinoMockPostprocessor(ISpecimenBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            this.builder = builder;
        }

        public ISpecimenBuilder Builder
        {
            get
            {
                return this.builder;
            }
        }

        #region ISpecimenBuilder Members

        public object Create(object request, ISpecimenContext context)
        {
            if (!RhinoMockType.IsMockable(request)
                & !RhinoMockType.IsGeneric(request as Type))
            {
                return new NoSpecimen(request);
            }

            var built = this.builder.Create(request, context);
            var m = built as IMockedObject;
            if (m == null)
            {
                return new NoSpecimen(request);
            }

            return m;
        }

        #endregion
    }
}
