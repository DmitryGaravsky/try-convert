using System;
using System.Collections.Immutable;

namespace MSBuild.Conversion.Facts
{
    /// <summary>
    ///  A bunch of known values regarding desktop projects.
    /// </summary>
    public static class DesktopFacts
    {
        /// <summary>
        /// For use with conversion of WinForms and WPF projects only.
        /// </summary>
        public static ImmutableArray<string> ReferencesThatNeedRemoval => ImmutableArray.Create(
            "System.Deployment"
        );

        /// <summary>
        /// The core set of references all desktop WPF projects use.
        /// </summary>
        /// <remarks>
        /// Desktop projects will only convert to .NET Core, so any item includes that have .NET Core equivalents will be removed.
        /// Users will have to ensure those packages are also added if they cannot do so with a tool.
        /// References that are already present will also be removed.
        /// </remarks>
        public static ImmutableArray<string> KnownWPFReferences => ImmutableArray.Create(
            "System.Xaml",
            "PresentationCore",
            "PresentationFramework"
        );

        /// <summary>
        /// The core set of references all desktop WinForms projects use.
        /// </summary>
        /// <remarks>
        /// Desktop projects will only convert to .NET Core, so any item includes that have .NET Core equivalents will be removed.
        /// Users will have to ensure those packages are also added if they cannot do so with a tool.
        /// References that are already present will also be removed.
        /// </remarks>
        public static ImmutableArray<string> KnownWinFormsReferences => ImmutableArray.Create(
            "System.Windows.Forms",
            "System.Deployment"
        );

        public static ImmutableArray<string> KnownSDKReferences => ImmutableArray.Create(
              "Microsoft.CSharp",
              "Microsoft.VisualBasic.Core",
              "Microsoft.VisualBasic",
              "Microsoft.Win32.Primitives",
              "System.AppContext",
              "System.Buffers",
              "System.Collections.Concurrent",
              "System.Collections.Immutable",
              "System.Collections.NonGeneric",
              "System.Collections.Specialized",
              "System.Collections",
              "System.ComponentModel.Annotations",
              "System.ComponentModel.DataAnnotations",
              "System.ComponentModel.EventBasedAsync",
              "System.ComponentModel.Primitives",
              "System.ComponentModel.TypeConverter",
              "System.ComponentModel",
              "System.Configuration",
              "System.Console",
              "System.Core",
              "System.Data.Common",
              "System.Data.DataSetExtensions",
              "System.Data",
              "System.Diagnostics.Contracts",
              "System.Diagnostics.Debug",
              "System.Diagnostics.DiagnosticSource",
              "System.Diagnostics.FileVersionInfo",
              "System.Diagnostics.Process",
              "System.Diagnostics.StackTrace",
              "System.Diagnostics.TextWriterTraceListener",
              "System.Diagnostics.Tools",
              "System.Diagnostics.TraceSource",
              "System.Diagnostics.Tracing",
              "System.Drawing.Primitives",
              "System.Drawing",
              "System.Dynamic.Runtime",
              "System.Formats.Asn1",
              "System.Globalization.Calendars",
              "System.Globalization.Extensions",
              "System.Globalization",
              "System.IO.Compression.Brotli",
              "System.IO.Compression.FileSystem",
              "System.IO.Compression.ZipFile",
              "System.IO.Compression",
              "System.IO.FileSystem.DriveInfo",
              "System.IO.FileSystem.Primitives",
              "System.IO.FileSystem.Watcher",
              "System.IO.FileSystem",
              "System.IO.IsolatedStorage",
              "System.IO.MemoryMappedFiles",
              "System.IO.Pipes",
              "System.IO.UnmanagedMemoryStream",
              "System.IO",
              "System.Linq.Expressions",
              "System.Linq.Parallel",
              "System.Linq.Queryable",
              "System.Linq",
              "System.Memory",
              "System.Net.Http.Json",
              "System.Net.Http",
              "System.Net.HttpListener",
              "System.Net.Mail",
              "System.Net.NameResolution",
              "System.Net.NetworkInformation",
              "System.Net.Ping",
              "System.Net.Primitives",
              "System.Net.Requests",
              "System.Net.Security",
              "System.Net.ServicePoint",
              "System.Net.Sockets",
              "System.Net.WebClient",
              "System.Net.WebHeaderCollection",
              "System.Net.WebProxy",
              "System.Net.WebSockets.Client",
              "System.Net.WebSockets",
              "System.Net",
              "System.Numerics.Vectors",
              "System.Numerics",
              "System.ObjectModel",
              "System.Reflection.DispatchProxy",
              "System.Reflection.Emit.ILGeneration",
              "System.Reflection.Emit.Lightweight",
              "System.Reflection.Emit",
              "System.Reflection.Extensions",
              "System.Reflection.Metadata",
              "System.Reflection.Primitives",
              "System.Reflection.TypeExtensions",
              "System.Reflection",
              "System.Resources.Reader",
              "System.Resources.ResourceManager",
              "System.Resources.Writer",
              "System.Runtime.CompilerServices.Unsafe",
              "System.Runtime.CompilerServices.VisualC",
              "System.Runtime.Extensions",
              "System.Runtime.Handles",
              "System.Runtime.InteropServices.RuntimeInformation",
              "System.Runtime.InteropServices",
              "System.Runtime.Intrinsics",
              "System.Runtime.Loader",
              "System.Runtime.Numerics",
              "System.Runtime.Serialization.Formatters",
              "System.Runtime.Serialization.Json",
              "System.Runtime.Serialization.Primitives",
              "System.Runtime.Serialization.Xml",
              "System.Runtime.Serialization",
              "System.Runtime",
              "System.Security.Claims",
              "System.Security.Cryptography.Algorithms",
              "System.Security.Cryptography.Csp",
              "System.Security.Cryptography.Encoding",
              "System.Security.Cryptography.Primitives",
              "System.Security.Cryptography.X509Certificates",
              "System.Security.Principal",
              "System.Security.SecureString",
              "System.Security",
              "System.ServiceModel.Web",
              "System.ServiceProcess",
              "System.Text.Encoding.CodePages",
              "System.Text.Encoding.Extensions",
              "System.Text.Encoding",
              "System.Text.Encodings.Web",
              "System.Text.Json",
              "System.Text.RegularExpressions",
              "System.Threading.Channels",
              "System.Threading.Overlapped",
              "System.Threading.Tasks.Dataflow",
              "System.Threading.Tasks.Extensions",
              "System.Threading.Tasks.Parallel",
              "System.Threading.Tasks",
              "System.Threading.Thread",
              "System.Threading.ThreadPool",
              "System.Threading.Timer",
              "System.Threading",
              "System.Transactions.Local",
              "System.Transactions",
              "System.ValueTuple",
              "System.Web.HttpUtility",
              "System.Web",
              "System.Windows",
              "System.Xml.Linq",
              "System.Xml.ReaderWriter",
              "System.Xml.Serialization",
              "System.Xml.XDocument",
              "System.Xml.XPath.XDocument",
              "System.Xml.XPath",
              "System.Xml.XmlDocument",
              "System.Xml.XmlSerializer",
              "System.Xml",
              "System",
              "WindowsBase",
              "mscorlib",
              "netstandard"
        );
        public static ImmutableArray<string> KnownDesktopReferences => ImmutableArray.Create(
            "Accessibility",//"WindowsForms;WPF"
            "Microsoft.Win32.Registry.AccessControl", //"WindowsForms;WPF"
            "Microsoft.Win32.Registry",   //"WindowsForms;WPF"
            "Microsoft.Win32.SystemEvents",    //"WindowsForms;WPF"
            "PresentationCore",   //"WPF"
            "PresentationFramework.Aero",    //"WPF"
            "PresentationFramework.Aero2",    //"WPF"
            "PresentationFramework.AeroLite",    //"WPF"
            "PresentationFramework.Classic",    //"WPF"
            "PresentationFramework.Luna",    //"WPF"
            "PresentationFramework.Royale",    //"WPF"
            "PresentationFramework",    //"WPF"
            "PresentationUI",    //"WPF"
            "ReachFramework",    //"WPF"
            "System.CodeDom",    //"WindowsForms;WPF"
            "System.Configuration.ConfigurationManager",    //"WindowsForms;WPF"
            "System.Diagnostics.EventLog",    //"WindowsForms;WPF"
            "System.Diagnostics.PerformanceCounter",    //"WindowsForms;WPF"
            "System.DirectoryServices",    //"WindowsForms;WPF"
            "System.IO.FileSystem.AccessControl",    //"WindowsForms;WPF"
            "System.IO.Packaging",    //"WindowsForms;WPF"
            "System.IO.Pipes.AccessControl",    //"WindowsForms;WPF"
            "System.Printing",    //"WPF"
            "System.Resources.Extensions",    //"WindowsForms;WPF"
            "System.Security.AccessControl",    //"WindowsForms;WPF"
            "System.Security.Cryptography.Cng",    //"WindowsForms;WPF"
            "System.Security.Cryptography.Pkcs",    //"WindowsForms;WPF"
            "System.Security.Cryptography.ProtectedData",    //"WindowsForms;WPF"
            "System.Security.Cryptography.Xml",    //"WindowsForms;WPF"
            "System.Security.Permissions",    //"WindowsForms;WPF"
            "System.Security.Principal.Windows",    //"WindowsForms;WPF"
            "System.Threading.AccessControl",    //"WindowsForms;WPF"
            "System.Windows.Controls.Ribbon",    //"WPF"
            "System.Windows.Extensions",    //"WindowsForms;WPF"
            "System.Windows.Input.Manipulations",    //"WPF"
            "System.Windows.Presentation",    //"WPF"
            "System.Xaml",    //"WPF"
            "UIAutomationClient",    //"WPF"
            "UIAutomationClientSideProviders",    //"WPF"
            "UIAutomationProvider",    //"WPF"
            "UIAutomationTypes",    //"WPF"
            "WindowsBase",    //"WPF"
            "WindowsFormsIntegration"//
        );

        public static ImmutableArray<Guid> KnownSupportedDesktopProjectTypeGuids => ImmutableArray.Create(
            Guid.Parse("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"), // C#
            Guid.Parse("{F184B08F-C81C-45F6-A57F-5ABD9991F28F}"), // VB.NET
            Guid.Parse("{60DC8134-EBA5-43B8-BCC9-BB4BC16C2548}") // WPF
        );

        public const string WinSDKAttribute = "Microsoft.NET.Sdk.WindowsDesktop";
        public const string UseWPFPropertyName = "UseWPF";
        public const string PlatformsPropertyName = "Platforms";
        public const string UseWinFormsPropertyName = "UseWindowsForms";
        public const string DesignerSuffix = ".Designer.cs";
        public const string XamlFileExtension = ".xaml";
        public const string SettingsDesignerFileName = "Settings.Designer.cs";
        public const string SettingsFileSuffix = ".settings";
        public const string ResourcesDesignerFileName = "Resources.Designer.cs";
        public const string ResourcesFileSuffix = ".resx";
        public const string FormSubTypeValue = "Form";
    }
}
