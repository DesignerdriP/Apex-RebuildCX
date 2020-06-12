using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.IO.Compression
{
	public static class ZipFile
	{
		private readonly static char s_pathSeperator;

		static ZipFile()
		{
			ZipFile.s_pathSeperator = (LocalAppContextSwitches.ZipFileUseBackslash ? '\\' : '/');
		}

		public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName)
		{
			ZipFile.DoCreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, null, false, null);
		}

		public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory)
		{
			ZipFile.DoCreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, new CompressionLevel?(compressionLevel), includeBaseDirectory, null);
		}

		public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
		{
			ZipFile.DoCreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, new CompressionLevel?(compressionLevel), includeBaseDirectory, entryNameEncoding);
		}

		private static void DoCreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel? compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
		{
			string str;
			char sPathSeperator;
			sourceDirectoryName = Path.GetFullPath(sourceDirectoryName);
			destinationArchiveFileName = Path.GetFullPath(destinationArchiveFileName);
			using (ZipArchive zipArchive = ZipFile.Open(destinationArchiveFileName, ZipArchiveMode.Create, entryNameEncoding))
			{
				bool flag = true;
				DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectoryName);
				string fullName = directoryInfo.FullName;
				if (includeBaseDirectory && directoryInfo.Parent != null)
				{
					fullName = directoryInfo.Parent.FullName;
				}
				foreach (FileSystemInfo fileSystemInfo in directoryInfo.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
				{
					flag = false;
					int length = fileSystemInfo.FullName.Length - fullName.Length;
					if (!LocalAppContextSwitches.ZipFileUseBackslash)
					{
						str = ZipFile.EntryFromPath(fileSystemInfo.FullName, fullName.Length, length);
					}
					else
					{
						str = fileSystemInfo.FullName.Substring(fullName.Length, length);
						str = str.TrimStart(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
					}
					if (!(fileSystemInfo is FileInfo))
					{
						DirectoryInfo directoryInfo1 = fileSystemInfo as DirectoryInfo;
						if (directoryInfo1 == null || !ZipFile.IsDirEmpty(directoryInfo1))
						{
							continue;
						}
						sPathSeperator = ZipFile.s_pathSeperator;
						zipArchive.CreateEntry(string.Concat(str, sPathSeperator.ToString()));
					}
					else
					{
						ZipFileExtensions.DoCreateEntryFromFile(zipArchive, fileSystemInfo.FullName, str, compressionLevel);
					}
				}
				if (includeBaseDirectory & flag)
				{
					str = (LocalAppContextSwitches.ZipFileUseBackslash ? directoryInfo.Name : ZipFile.EntryFromPath(directoryInfo.Name, 0, directoryInfo.Name.Length));
					sPathSeperator = ZipFile.s_pathSeperator;
					zipArchive.CreateEntry(string.Concat(str, sPathSeperator.ToString()));
				}
			}
		}

		private static string EntryFromPath(string entry, int offset, int length)
		{
			while (length > 0 && (entry[offset] == Path.DirectorySeparatorChar || entry[offset] == Path.AltDirectorySeparatorChar))
			{
				offset++;
				length--;
			}
			if (length == 0)
			{
				return string.Empty;
			}
			char[] charArray = entry.ToCharArray(offset, length);
			for (int i = 0; i < (int)charArray.Length; i++)
			{
				if (charArray[i] == Path.DirectorySeparatorChar || charArray[i] == Path.AltDirectorySeparatorChar)
				{
					charArray[i] = ZipFile.s_pathSeperator;
				}
			}
			return new string(charArray);
		}

		public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
		{
			ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, null);
		}

		public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding entryNameEncoding)
		{
			if (sourceArchiveFileName == null)
			{
				throw new ArgumentNullException("sourceArchiveFileName");
			}
			using (ZipArchive zipArchive = ZipFile.Open(sourceArchiveFileName, ZipArchiveMode.Read, entryNameEncoding))
			{
				zipArchive.ExtractToDirectory(destinationDirectoryName);
			}
		}

		private static bool IsDirEmpty(DirectoryInfo possiblyEmptyDir)
		{
			bool flag;
			using (IEnumerator<FileSystemInfo> enumerator = possiblyEmptyDir.EnumerateFileSystemInfos("*", SearchOption.AllDirectories).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					FileSystemInfo current = enumerator.Current;
					flag = false;
				}
				else
				{
					return true;
				}
			}
			return flag;
		}

		public static ZipArchive Open(ZipArchiveMode mode, string archiveFileName)
		{
			return ZipFile.Open(archiveFileName, mode, null);
		}

		public static ZipArchive Open(string archiveFileName, ZipArchiveMode mode, Encoding entryNameEncoding)
		{
			FileMode fileMode;
			FileAccess fileAccess;
			FileShare fileShare;
			NewMethod1(mode, out fileMode, out fileAccess, out fileShare);
			FileStream fileStream = null;
			ZipArchive zipArchive;
			try
			{
				fileStream = File.Open(archiveFileName, fileMode, fileAccess, fileShare);
				zipArchive = new ZipArchive(fileStream, mode, false, entryNameEncoding);
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
				throw;
			}
			return zipArchive;
		}

		private static void NewMethod1(ZipArchiveMode mode, out FileMode fileMode, out FileAccess fileAccess, out FileShare fileShare)
		{
			NewMethod(mode,
					  out fileMode,
					  out fileAccess,
					  out fileShare);
		}

		private static void NewMethod(ZipArchiveMode mode, out FileMode fileMode, out FileAccess fileAccess, out FileShare fileShare)
		{
			switch (mode)
			{
				case ZipArchiveMode.Write:
					{
						fileMode = FileMode.Open;
						fileAccess = FileAccess.Read;
						fileShare = FileShare.Read;
						break;
					}
				case ZipArchiveMode.Create:
					{
						fileMode = FileMode.CreateNew;
						fileAccess = FileAccess.Write;
						fileShare = FileShare.None;
						break;
					}
				case ZipArchiveMode.Update:
					{
						fileMode = FileMode.OpenOrCreate;
						fileAccess = FileAccess.ReadWrite;
						fileShare = FileShare.None;
						break;
					}
				default:
					{
						throw new ArgumentOutOfRangeException("mode");
					}
			}
		}

		public static ZipArchive OpenRead(string archiveFileName)
		{
			return ZipFile.Open(ZipArchiveMode.Read, archiveFileName);
		}
	}
}