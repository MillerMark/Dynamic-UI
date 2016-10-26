using Contract;
using System;
using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Linq;
using DynamicUICore;

namespace PlugIn1
{
    [Export(typeof(AssemblyLineControl))]
    public class ShapeChanger : AssemblyLineControl
    {
        [UI("Shape: ")]
        public ProductShape Shape { get; set; }

        [UI("Corner Radius: ", getVisible: nameof(ShowCornerRadius))]
        public double CornerRadius { get; set; }

        [UI("Corner Name: ", getVisible: nameof(ShowCornerRadius))]
        public string CornerName { get; set; }


        public ShapeChanger()
        {
            Shape = ProductShape.Rectangle;
            CornerRadius = 5;
        }

        bool ShowCornerRadius()
        {
            return Shape == ProductShape.RoundedRect;
        }
        public override void Process(Product product)
        {
            product.Shape = Shape;
            product.CornerRadius = CornerRadius;
        }
    }
}
