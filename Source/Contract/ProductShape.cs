using System;
using System.Linq;

namespace Contract
{
	[Flags]
	public enum ProductShape
	{
		Rectangle = 1,
		RoundedRect = 2,
		Ellipse = 4
	}
}
