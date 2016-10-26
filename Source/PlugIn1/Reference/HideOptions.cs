using System;
using System.Linq;

namespace PlugIn1
{
    [Flags]
    public enum HideOptions
    {
        HideText = 1,
        HideFill = 2,
        HideOutline = 4
    }
}
