using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

public static class MyZipUtility
{
	public class PreparedModZip : IDisposable
	{
		private ZipArchive _zipFile;

		public PreparedModZip(Stream inputStream)
		{
			_zipFile = new ZipArchive(inputStream, ZipArchiveMode.Read);
		}

		public IEnumerable<ModFileBase> GetModFiles()
		{
			foreach (ZipArchiveEntry entry in _zipFile.Entries)
			{
				if (entry.IsValidFile())
				{
					yield return new RawModFileInZip(entry);
				}
			}
		}

		public void Dispose()
		{
			((IDisposable)_zipFile)?.Dispose();
		}
	}

	public class ZipEntryStub
	{
		public readonly string FileName;

		public readonly string Data;

		public ZipEntryStub(string name, string data)
		{
			FileName = name;
			Data = data;
		}
	}

	public static bool IsValidFile(this ZipArchiveEntry zipEntry)
	{
		FileAttributes fileAttributes = (FileAttributes)(byte)(zipEntry.ExternalAttributes & 0xFF);
		if (fileAttributes.HasFlag(FileAttributes.Directory))
		{
			return false;
		}
		string fullName = zipEntry.FullName;
		if (fullName.Length == 0)
		{
			return false;
		}
		if (fullName.EndsWith('/') || fullName.EndsWith('\\'))
		{
			return false;
		}
		return true;
	}

	public static void ExtractZipFile(Stream zipFileStream, string outFolder)
	{
		Directory.CreateDirectory(outFolder);
		using ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Read);
		foreach (ZipArchiveEntry entry in zipArchive.Entries)
		{
			if (entry.IsValidFile())
			{
				string fullName = entry.FullName;
				string path = Path.Combine(outFolder, fullName);
				string directoryName = Path.GetDirectoryName(path);
				if (directoryName != null && directoryName.Length > 0)
				{
					Directory.CreateDirectory(directoryName);
				}
				using Stream stream = entry.Open();
				stream.WriteToFile(path);
			}
		}
	}

	public static byte[] CreateZipFile(string folderName)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
		{
			CompressFolder(folderName, archive, folderName.Length + ((!folderName.EndsWith("\\")) ? 1 : 0));
		}
		return memoryStream.ToArray();
	}

	public static byte[] CreateZipFile(List<ZipEntryStub> entries)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
		{
			foreach (ZipEntryStub entry in entries)
			{
				using Stream stream = zipArchive.CreateEntry(entry.FileName).Open();
				using StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
				streamWriter.Write(entry.Data);
			}
		}
		return memoryStream.ToArray();
	}

	private static void CompressFolder(string path, ZipArchive archive, int folderOffset)
	{
		string[] files = Directory.GetFiles(path);
		foreach (string obj in files)
		{
			FileInfo fileInfo = new FileInfo(obj);
			string entryName = obj.Substring(folderOffset).Replace("\\", "/");
			ZipArchiveEntry zipArchiveEntry = archive.CreateEntry(entryName);
			zipArchiveEntry.LastWriteTime = fileInfo.LastWriteTime;
			using FileStream fileStream = File.OpenRead(obj);
			using Stream destination = zipArchiveEntry.Open();
			fileStream.CopyTo(destination);
		}
		files = Directory.GetDirectories(path);
		for (int i = 0; i < files.Length; i++)
		{
			CompressFolder(files[i], archive, folderOffset);
		}
	}
}
