using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.IO.Compression
{
    internal sealed class SR
    {
        internal const string IO_DirectoryNameWithData = "IO_DirectoryNameWithData";

        internal const string IO_ExtractingResultsInOutside = "IO_ExtractingResultsInOutside";

        private static System.IO.Compression.SR loader;

        private ResourceManager resources;

        private static CultureInfo Culture
        {
            get
            {
                return null;
            }
        }

        public static ResourceManager Resources
        {
            get
            {
                return System.IO.Compression.SR.GetLoader().resources;
            }
        }

        static SR()
        {
        }

        internal SR()
        {
            this.resources = new ResourceManager("System.IO.Compression.FileSystem", this.GetType().Assembly);
        }

        private static System.IO.Compression.SR GetLoader()
        {
            if (System.IO.Compression.SR.loader == null)
            {
                System.IO.Compression.SR sR = new System.IO.Compression.SR();
                Interlocked.CompareExchange<System.IO.Compression.SR>(ref System.IO.Compression.SR.loader, sR, null);
            }
            return System.IO.Compression.SR.loader;
        }

        public static object GetObject(string name)
        {
            System.IO.Compression.SR loader = System.IO.Compression.SR.GetLoader();
            if (loader == null)
            {
                return null;
            }
            return loader.resources.GetObject(name, System.IO.Compression.SR.Culture);
        }

        public static string GetString(string name, params object[] args)
        {
            System.IO.Compression.SR loader = System.IO.Compression.SR.GetLoader();
            if (loader == null)
            {
                return null;
            }
            string str = loader.resources.GetString(name, System.IO.Compression.SR.Culture);
            if (args == null || args.Length == 0)
            {
                return str;
            }
            for (int i = 0; i < (int)args.Length; i++)
            {
                string str1 = args[i] as string;
                if (str1 != null && str1.Length > 1024)
                {
                    args[i] = string.Concat(str1.Substring(0, 1021), "...");
                }
            }
            return string.Format(CultureInfo.CurrentCulture, str, args);
        }

        public static string GetString(string name)
        {
            System.IO.Compression.SR loader = System.IO.Compression.SR.GetLoader();
            if (loader == null)
            {
                return null;
            }
            return loader.resources.GetString(name, System.IO.Compression.SR.Culture);
        }

        public static string GetString(string name, out bool usedFallback)
        {
            usedFallback = false;
            return System.IO.Compression.SR.GetString(name);
        }
    }
}