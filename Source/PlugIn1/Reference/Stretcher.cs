using Contract;
using DynamicUICore;
using System.ComponentModel.Composition;

namespace PlugIn1
{

    [Export(typeof(AssemblyLineControl))]
	public class Stretcher : AssemblyLineControl
	{
		[UI("Width Factor:")]
		public double WidthFactor { get; set; }

        [UI("Height Factor:")]
        public double HeightFactor { get; set; }

        public Stretcher()
        {
            WidthFactor = 1;
            HeightFactor = 1;
        }

        public override void Process(Product product)
		{
			product.Width *= WidthFactor;
            product.Height *= HeightFactor;
        }
	}
}
