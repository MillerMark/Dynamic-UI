using System;
using System.Linq;

namespace Contract
{

	[Flags]
	public enum ProductShape
	{
		Rectangle = 1,
		Ellipse = 2,
		RoundedRect = 4
	}
}
