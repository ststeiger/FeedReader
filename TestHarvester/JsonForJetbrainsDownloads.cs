

namespace TestHarvester
{

    using System.Collections.Generic;


    public class Distributions
    {
        public Zip zip { get; set; }
        public LinuxARM64 linuxARM64 { get; set; }
        public Linux linux { get; set; }
        public ThirdPartyLibrariesJson thirdPartyLibrariesJson { get; set; }
        public Windows windows { get; set; }
        public WindowsZip windowsZip { get; set; }
        public WindowsARM64 windowsARM64 { get; set; }
        public Mac mac { get; set; }
        public MacM1 macM1 { get; set; }
        public WindowsZipARM64 windowsZipARM64 { get; set; }
    }

    public class Downloads
    {
        public LinuxARM64 linuxARM64 { get; set; }
        public Linux linux { get; set; }
        public Windows windows { get; set; }
        public WindowsZip windowsZip { get; set; }
        public WindowsARM64 windowsARM64 { get; set; }
        public Mac mac { get; set; }
        public MacM1 macM1 { get; set; }
        public WindowsZipARM64 windowsZipARM64 { get; set; }
        public ThirdPartyLibrariesJson thirdPartyLibrariesJson { get; set; }
    }

    public class Linux
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class LinuxARM64
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class Mac
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string fromBuild { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class MacM1
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class Patches
    {
        public List<Win> win { get; set; }
        public List<Mac> mac { get; set; }
        public List<Unix> unix { get; set; }
    }

    public class Release
    {
        public string date { get; set; }
        public string type { get; set; }
        public Downloads downloads { get; set; }
        public Patches patches { get; set; }
        public string notesLink { get; set; }
        public bool licenseRequired { get; set; }
        public string version { get; set; }
        public string majorVersion { get; set; }
        public string build { get; set; }
        public string whatsnew { get; set; }
        public UninstallFeedbackLinks uninstallFeedbackLinks { get; set; }
        public string printableReleaseType { get; set; }
    }

    public class Root
    {
        public string name { get; set; }
        public string link { get; set; }
        public List<Release> releases { get; set; }
        public Distributions distributions { get; set; }
    }

    public class ThirdPartyLibrariesJson
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class UninstallFeedbackLinks
    {
        public string zip { get; set; }
        public string linuxARM64 { get; set; }
        public string linux { get; set; }
        public string thirdPartyLibrariesJson { get; set; }
        public string windows { get; set; }
        public string windowsZip { get; set; }
        public string windowsARM64 { get; set; }
        public string mac { get; set; }
        public string macM1 { get; set; }
        public string windowsZipARM64 { get; set; }
    }

    public class Unix
    {
        public string fromBuild { get; set; }
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
    }

    public class Win
    {
        public string fromBuild { get; set; }
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
    }

    public class Windows
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class WindowsARM64
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class WindowsZip
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class WindowsZipARM64
    {
        public string link { get; set; }
        public int size { get; set; }
        public string checksumLink { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
    }

    public class Zip
    {
        public string name { get; set; }
        public string extension { get; set; }
    }



}
