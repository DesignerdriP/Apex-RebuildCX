using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.IO.Compression
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ZipFileExtensions
    {
        public static ZipArchiveEntry CreateEntryFromFile(this ZipArchive destination, string sourceFileName, string entryName)
        {
            return ZipFileExtensions.DoCreateEntryFromFile(destination, sourceFileName, entryName, null);
        }

        public static ZipArchiveEntry CreateEntryFromFile(this ZipArchive destination, string sourceFileName, string entryName, CompressionLevel compressionLevel)
        {
            return ZipFileExtensions.DoCreateEntryFromFile(destination, sourceFileName, entryName, new CompressionLevel?(compressionLevel));
        }

        internal static ZipArchiveEntry DoCreateEntryFromFile(ZipArchive destination, string sourceFileName, string entryName, CompressionLevel? compressionLevel)
        {
            ZipArchiveEntry zipArchiveEntry;
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (sourceFileName == null)
            {
                throw new ArgumentNullException("sourceFileName");
            }
            if (entryName == null)
            {
                throw new ArgumentNullException("entryName");
            }
            using (Stream stream = File.Open(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ZipArchiveEntry zipArchiveEntry1 = (compressionLevel.HasValue ? destination.CreateEntry(entryName, compressionLevel.Value) : destination.CreateEntry(entryName));
                DateTime lastWriteTime = File.GetLastWriteTime(sourceFileName);
                if (lastWriteTime.Year < 1980 || lastWriteTime.Year > 2107)
                {
                    lastWriteTime = new DateTime(1980, 1, 1, 0, 0, 0);
                }
                zipArchiveEntry1.LastWriteTime = lastWriteTime;
                using (Stream stream1 = zipArchiveEntry1.Open())
                {
                    stream.CopyTo(stream1);
                }
                zipArchiveEntry = zipArchiveEntry1;
            }
            return zipArchiveEntry;
        }

        public static void ExtractToDirectory(this ZipArchive source, string destinationDirectoryName)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destinationDirectoryName == null)
            {
                throw new ArgumentNullException("destinationDirectoryName");
            }
            string fullName = Directory.CreateDirectory(destinationDirectoryName).FullName;
            if (!LocalAppContextSwitches.DoNotAddTrailingSeparator)
            {
                int length = fullName.Length;
                if (length != 0 && fullName[length - 1] != Path.DirectorySeparatorChar)
                {
                    char directorySeparatorChar = Path.DirectorySeparatorChar;
                    fullName = string.Concat(fullName, directorySeparatorChar.ToString());
                }
            }
            foreach (ZipArchiveEntry entry in source.Entries)
            {
                string fullPath = Path.GetFullPath(Path.Combine(fullName, entry.FullName));
                if (!fullPath.StartsWith(fullName, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException(System.IO.Compression.SR.GetString("IO_ExtractingResultsInOutside"));
                }
                if (Path.GetFileName(fullPath).Length != 0)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                    entry.ExtractToFile(fullPath, false);
                }
                else
                {
                    if (entry.Length != 0)
                    {
                        throw new IOException(System.IO.Compression.SR.GetString("IO_DirectoryNameWithData"));
                    }
                    Directory.CreateDirectory(fullPath);
                }
            }
        }

        public static void ExtractToFile(this ZipArchiveEntry source, string destinationFileName)
        {
            source.ExtractToFile(destinationFileName, false);
        }

        public static void ExtractToFile(this ZipArchiveEntry source, string destinationFileName, bool overwrite)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destinationFileName == null)
            {
                throw new ArgumentNullException("destinationFileName");
            }
            using (Stream stream = File.Open(destinationFileName, (overwrite ? FileMode.Create : FileMode.CreateNew), FileAccess.Write, FileShare.None))
            {
                using (Stream stream1 = source.Open())
                {
                    stream1.CopyTo(stream);
                }
            }
            File.SetLastWriteTime(destinationFileName, source.LastWriteTime.DateTime);
        }
    }
}