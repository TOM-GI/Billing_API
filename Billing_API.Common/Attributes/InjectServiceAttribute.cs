using System;

namespace Billing_API.Common.Attributes
{
    public enum InjectAs { Self, ImplementingInterface }

    [AttributeUsage(AttributeTargets.Class)]
    public class InjectServiceAttribute : Attribute
    {
        public InjectServiceAttribute(InjectAs @as) { As = @as; }

        public InjectAs As { get; }
    }
}