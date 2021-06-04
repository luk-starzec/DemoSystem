using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EmployerWebApp.Models
{
    public enum EnumPriority
    {
        [Description("")]
        None,
        [Description("Low")]
        Low,
        [Description("Normal")]
        Normal,
        [Description("High")]
        High,
    }
}