using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		public partial class CreateMeasurementUnit_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.TheEnd();
			}

		}
}
