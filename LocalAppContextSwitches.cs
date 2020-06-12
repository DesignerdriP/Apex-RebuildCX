using System.Runtime.CompilerServices;

namespace System
{
    internal static class LocalAppContextSwitches
    {
        private static int _zipFileUseBackslash;

        private static int _doNotAddTrailingSeparator;

        internal const string ZipFileUseBackslashName = "Switch.System.IO.Compression.ZipFile.UseBackslash";

        internal const string DoNotAddTrailingSeparatorString = "Switch.System.IO.Compression.DoNotAddTrailingSeparator";

        public static bool DoNotAddTrailingSeparator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue("Switch.System.IO.Compression.DoNotAddTrailingSeparator", ref LocalAppContextSwitches._doNotAddTrailingSeparator);
            }
        }

        public static bool ZipFileUseBackslash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue("Switch.System.IO.Compression.ZipFile.UseBackslash", ref LocalAppContextSwitches._zipFileUseBackslash);
            }
        }
    }
}