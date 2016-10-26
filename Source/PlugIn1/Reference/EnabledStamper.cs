using Contract;
using DynamicUICore;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace PlugIn1
{
    [Export(typeof(AssemblyLineControl))]
    public class EnabledStamper : Stamper
    {
        [UI("Change Text")]
        public bool EnableTextChange { get; set; }

        public override void Process(Product product)
        {
            if (EnableTextChange)
                product.Name = Text;
            product.TextColor = TextColor;
        }
    }
}
