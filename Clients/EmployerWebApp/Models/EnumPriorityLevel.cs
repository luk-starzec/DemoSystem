using System.ComponentModel;

namespace EmployerWebApp.Models
{
    public enum EnumPriorityLevel
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