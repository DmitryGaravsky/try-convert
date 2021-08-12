using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using static MSBuild.Conversion.Facts.Constants;

namespace MSBuild.Conversion.Facts
{
    public static class DevExpressFacts
    {
        static Dictionary<string, Dictionary<string, string>> versionToReferences;
        public const string MajorVersion = ".v20.2";
        public const string PublicAndPrereleasePackageMaskSuffix = ".*-*";
        public static bool SkipNetwork(string packageName)
        {
            return IsDevExpress(packageName);
        }
        public static bool IsReferenceConvertibleToPackageReference(string includeString) {
            if (versionToReferences == null)
            {
                versionToReferences = new Dictionary<string, Dictionary<string, string>>();
            }
            var version = DependenciesGenerator.GetVersion(includeString);
            if (version == null)
                return false;
            var referenceName = DependenciesGenerator.GetReferenceName(includeString);
            string packageName;
            return TryGetPackageName(referenceName, version, out packageName);
        }
        public static bool TryGetPackageName(string referenceName, string version, out string packageName) {
            Dictionary<string, string> versionedReferenceToPackage;
            if (!versionToReferences.TryGetValue(version, out versionedReferenceToPackage))
            {
                versionedReferenceToPackage = CreateReferenceToPackage(version);
                versionToReferences[version] = versionedReferenceToPackage;
            }
            if (versionedReferenceToPackage.TryGetValue(referenceName, out packageName))
                return true;
            else
                return false;
        }
        public static string FindPackageNameFromReferenceName(string includeString)
        {
            if (!IsDevExpress(includeString))
                return null;
            var version = DependenciesGenerator.GetVersion(includeString);
            var referenceName = DependenciesGenerator.GetReferenceName(includeString);
            string packageName;
            if (TryGetPackageName(referenceName, version, out packageName))
                return packageName;
            return null;
        }
        public static string GetPackageVersion(string includeString)
        {
            if (!includeString.StartsWith(DevExpressPrefix, StringComparison.InvariantCultureIgnoreCase))
                return null;
            var version = DependenciesGenerator.GetVersion(includeString);
            if (version == null)
                return "21.1" + PublicAndPrereleasePackageMaskSuffix;
            //var referenceName = DependenciesGenerator.GetReferenceName(includeString);
            return version + PublicAndPrereleasePackageMaskSuffix;
        }
        public static bool IsDevExpress(string name) {
            return name.StartsWith(DevExpressPrefix, StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool IsDevExpressWpf(string name)
        {
            return name.StartsWith(DevExpressXpfPrefix, StringComparison.InvariantCultureIgnoreCase);
        }
        static Dictionary<string, string> CreateReferenceToPackage(string version)
        {
            var references = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var package in DependenciesGenerator.GetReferences(version)) {
                foreach (var reference in package.Item2) {
                    references.Add(reference, package.Item1.PackageName);
                }
            }
            references.Add("DevExpress.Xpf.Mvvm.v" + version, "DevExpress.Mvvm");
            return references;
        }
    }
    public class DependenciesGenerator
    {
        internal static IEnumerable<Tuple<PackageInfo, IEnumerable<string>>> GetReferences(string version, bool netCore = true)
        {
            return GetPackages(version)
                .Select(x => new Tuple<PackageInfo, IEnumerable<string>>(x, GetPackageFileReferences(ZipFile.OpenRead(x.PackageFile), netCore)));
        }
        static IEnumerable<PackageInfo> GetPackages(string packageMajorVersion)
        {
            return Directory
                        .GetFiles(string.Format(LocalPackagesStorage, packageMajorVersion))
                        .Where(x => Path.GetExtension(x) == NugetPackageExtension)
                        .GroupBy(x => GetPackageNameWithoutVersion(Path.GetFileNameWithoutExtension(x)))
                        .Select(x => new PackageInfo(x.OrderBy(y => GetPackageMinorVersion(Path.GetFileNameWithoutExtension(y))).Last(), x.Key));
        }
        //static IEnumerable<string> ExtractPackageReferences(PackageInfo[] packagesStorage, string referencedPackageName, HashSet<string> viewedPackages)
        //{
        //    var packageFile = GetPackageFile(packagesStorage, referencedPackageName);

        //    var packageDependencies = GetPackageFileDepencies(packageFile).Where(x => !viewedPackages.Contains(x)).ToArray();
        //    viewedPackages.UnionWith(packageDependencies);

        //    var references = new List<string>();
        //    references.AddRange(GetPackageFileReferences(packageFile));
        //    references.AddRange(packageDependencies.SelectMany(x => ExtractPackageReferences(packagesStorage, x, viewedPackages)));
        //    return references.ToArray();
        //}

        //net5.0
        //netstandard1.6
        //netcoreapp3.0
        //net472
        static Regex NETCoreReferencedDllRegex = new Regex(@"lib\\(netcoreapp|netstandard|net)\d\.\d\z");
        static Regex ReferencedDllRegex = new Regex(@"lib\\net\d{1,3}\z");
        static Regex VersionRegex = new Regex(@"v\d\d.\d");
        static IEnumerable<string> GetPackageFileReferences(ZipArchive packageFile, bool netCore)
        {
            return packageFile.Entries
                .Where(x => PathMatchesDLL(x.FullName, netCore))
                .Select(x => Path.GetFileNameWithoutExtension(x.Name));
            //var nuspecEntry = packageFile.Entries.First(x => StringEquals(Path.GetExtension(x.Name), NuspecExtension));
            //using (var nuspec = nuspecEntry.Open())
            //{
            //    var nuspecDocument = new XmlDocument();
            //    nuspecDocument.Load(nuspec);
            //    var namespaceManager = GetNuspecManager(nuspecDocument);
            //    return nuspecDocument
            //                        .SelectNodes($"//{NuspecXsdNamespace}:{NuspecReferencesNode}", namespaceManager)
            //                        .OfType<XmlNode>()
            //                        .Select(x => GetAttribute(x, NuspecReferencesFileAttribute))
            //                        .Where(x => DevExpressFacts.IsDevExpress(x));
            //}
        }
        public static string GetReferenceName(string includeString)
        {
            var delimiterIndex = includeString.IndexOf(ReferenceNameDelimiter);
            return delimiterIndex > 0 ? includeString.Substring(0, delimiterIndex) : includeString;
        }
        public static string GetVersion(string includeString)
        {
            var referenceName = GetReferenceName(includeString);
            var matches = VersionRegex.Match(referenceName);
            if (matches.Success)
                return matches.Value.Substring("v".Length);
            return null;
        }
        static bool PathMatchesDLL(string path, bool netCore) {
            return (netCore ? NETCoreReferencedDllRegex : ReferencedDllRegex).Match(Path.GetDirectoryName(path)).Success;
        }
        static XmlNamespaceManager GetNuspecManager(XmlDocument nuspecDocument)
        {
            var namespaceManager = new XmlNamespaceManager(nuspecDocument.NameTable);
            namespaceManager.AddNamespace(NuspecXsdNamespace, NuspecXsdUri);
            return namespaceManager;
        }
        static string GetAttribute(XmlNode node, string attr)
        {
            return node.Attributes.Cast<XmlAttribute>().FirstOrDefault(x => StringEquals(x.Name, attr))?.Value;
        }
        static string GetPackageNameWithoutVersion(string packageFullName)
        {
            var wordPattern = @"\D+";
            return string.Join(PackageVersionSeparator, packageFullName.Split(PackageVersionSeparator.ToCharArray()).TakeWhile(x => Regex.IsMatch(x, wordPattern)));
        }
        static string GetPackageMinorVersion(string packageName)
        {
            return packageName.Split(PackageVersionSeparator.ToCharArray()).Last();
        }
        static bool StringEquals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }
        public static void Generate(decimal value)
        {
            //throw new NotImplementedException();
        }
    }
    static class Constants
    {
        public const char ReferenceNameDelimiter = ',';

        public const string DevExpressPrefix = "DevExpress";
        public const string DevExpressXpfPrefix = DevExpressPrefix +".Xpf";

        public const string ProjectGroupNode = "ItemGroup";
        public const string ProjectElementNameAttribute = "Include";

        public const string PackageNode = "PackageReference";
        public const string PackageVersionAttribute = "Version";
        public const string PackageVersionSeparator = ".";
        public const string LocalPackagesStorage = @"\\corp\builds\testbuilds\TestBuildNuGet.v{0}";

        public const string ReferenceNode = "Reference";
        public const string ReferencePathAttribute = "HintPath";
        public const string LocalReferenceStorage = @"C:\Work\20{0}\Bin\NETCoreDesktop";

        
        public const string NugetPackageExtension = ".nupkg";
        public const string NuspecExtension = ".nuspec";
        public const string NuspecXsdNamespace = "n";
        public const string NuspecXsdUri = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd";
        public const string NuspecDependencyNode = "dependency";
        public const string NuspecDependencyNameAttribute = "Id";
        public const string NuspecReferencesNode = "reference";
        public const string NuspecReferencesFileAttribute = "file";
        public const string NuspecDesignEntry = "Design";

        public const string ReferenceExtension = ".dll";
    }
    [DebuggerDisplay("{PackageName}")]
    internal class PackageInfo
    {
        public string PackageFile { get; }
        public string PackageName { get; }

        public PackageInfo(string packageFile, string packageName)
        {
            PackageFile = packageFile;
            PackageName = packageName;
        }
    }
}
