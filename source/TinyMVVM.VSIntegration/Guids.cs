// Guids.cs
// MUST match guids.h
using System;

namespace TinyMVVM.TinyMVVM_VSIntegration
{
    static class GuidList
    {
        public const string guidTinyMVVM_VSIntegrationPkgString = "dcf9bfb0-0daa-49a5-b05a-778cdddca3a5";
        public const string guidTinyMVVM_VSIntegrationCmdSetString = "a058ad6d-0509-494d-b32d-758cebe77ef9";

        public static readonly Guid guidTinyMVVM_VSIntegrationCmdSet = new Guid(guidTinyMVVM_VSIntegrationCmdSetString);
    };
}