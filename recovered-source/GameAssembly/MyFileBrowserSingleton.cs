using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleFileBrowser;
using UnityEngine;

public class MyFileBrowserSingleton : SingletonMonoBehaviour<MyFileBrowserSingleton>
{
	public class FileType
	{
		public string Name;

		public string Type;

		public FileType()
		{
		}

		public FileType(string name, string type)
		{
			Name = name;
			Type = type;
		}
	}

	public class UploadResult
	{
		public class FileResult
		{
			public string Path;

			public byte[] Data;

			public bool Success => Data != null;

			public string StringData => Encoding.UTF8.GetString(Data);
		}

		public List<FileResult> Results;

		public FileResult FirstFile
		{
			get
			{
				if (!GotFiles)
				{
					return null;
				}
				return Results[0];
			}
		}

		public bool GotFiles
		{
			get
			{
				if (Results != null)
				{
					return Results.Count != 0;
				}
				return false;
			}
		}
	}

	public class DownloadResult
	{
		public string Path;

		public string Info;

		public bool Success;

		public override string ToString()
		{
			return $"DownloadResult {{ Success: {Success}, Path: \"{Path}\", Info: \"{Info}\" }}";
		}
	}

	public static FileType TextFileType = new FileType
	{
		Name = "Text file",
		Type = ".txt"
	};

	public void GetFromFile(Action<UploadResult> finishFunc, List<FileType> filters = null, bool multifile = false, bool allowAllFiles = true)
	{
		filters = filters ?? new List<FileType> { TextFileType };
		FileBrowser.SetFilters(allowAllFiles, filters.Select((FileType t) => new FileBrowser.Filter(t.Name, t.Type)));
		FileBrowser.ShowLoadDialog((string[] t) =>
		{
			UploadResult obj = new UploadResult
			{
				Results = FileBrowser.Result.Select((string path) =>
				{
					try
					{
						byte[] data = FileBrowserHelpers.ReadBytesFromFile(path);
						return new UploadResult.FileResult
						{
							Path = path,
							Data = data
						};
					}
					catch (Exception exception)
					{
						Debug.Log("Fail reading file (read exception below): " + t);
						Debug.LogException(exception, this);
						return new UploadResult.FileResult
						{
							Path = path
						};
					}
				}).ToList()
			};
			finishFunc?.Invoke(obj);
		}, () =>
		{
			finishFunc?.Invoke(new UploadResult());
		}, FileBrowser.PickMode.Files, multifile, null, null, "Load file", "Load");
	}

	public void SaveToFile(Action<DownloadResult> finishFunc, string text, string file)
	{
		SaveToFileFileBrowser(finishFunc, (string path) =>
		{
			FileBrowserHelpers.WriteTextToFile(path, text);
		}, file);
	}

	public void SaveToFile(Action<DownloadResult> finishFunc, byte[] bytes, string file)
	{
		SaveToFileFileBrowser(finishFunc, (string path) =>
		{
			FileBrowserHelpers.WriteBytesToFile(path, bytes);
		}, file);
	}

	private static void SaveToFileFileBrowser(Action<DownloadResult> finishFunc, Action<string> onChosen, string file)
	{
		FileBrowser.SetFilters(showAllFilesFilter: true, Array.Empty<FileBrowser.Filter>());
		FileBrowser.ShowSaveDialog((string[] t) =>
		{
			if (FileBrowser.Result.Length != 0)
			{
				string text = FileBrowser.Result[0];
				try
				{
					onChosen?.Invoke(text);
					finishFunc?.Invoke(new DownloadResult
					{
						Path = text,
						Success = true
					});
					return;
				}
				catch (Exception arg)
				{
					string text2 = $"Fail writing to file (read exception below): {t}\n{arg}";
					Debug.LogError(text2);
					finishFunc?.Invoke(new DownloadResult
					{
						Path = text,
						Success = false,
						Info = text2
					});
					return;
				}
			}
			finishFunc?.Invoke(new DownloadResult
			{
				Success = false,
				Info = "Something went wrong while saving to file!"
			});
		}, () =>
		{
			finishFunc?.Invoke(new DownloadResult
			{
				Success = false
			});
		}, FileBrowser.PickMode.Files, allowMultiSelection: false, null, file, "Save file");
	}
}
