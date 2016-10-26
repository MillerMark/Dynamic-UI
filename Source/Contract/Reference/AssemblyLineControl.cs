using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
	public abstract class AssemblyLineControl
	{
		public string Name
		{
			get
			{
				return GetType().Name;
			}
		}
		

		public abstract void Process(Product product);
	}
}
