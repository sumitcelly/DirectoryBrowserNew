using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystemAccess
{
    public class WindowsFolderAccess:IFileSystemAccess
    {
		private string m_homeFolder = string.Empty;

		public WindowsFolderAccess(string homeFolder)
		{
			m_homeFolder = homeFolder;
		}

		public string HomeFolder { get { return m_homeFolder; } set { m_homeFolder = value; } }


		
		public ICollection<FileSystemMetaData> GetFolderContents(string parentFolder, string searchFiles,bool allDirectories,out string error)
		{
			error = string.Empty;			

			string folderToUse = "C:\\" + parentFolder;

			
			parentFolder = parentFolder.Replace("\\\\","\\").Trim();
			if (string.IsNullOrEmpty(parentFolder) || !Directory.Exists(folderToUse))
				return new List<FileSystemMetaData>();

			List<FileSystemMetaData> fileList = new List<FileSystemMetaData>();
			try
			{

				DirectoryInfo dirInfo = new DirectoryInfo(folderToUse);

				SearchOption option = allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
				List<DirectoryInfo> dirList = dirInfo.GetDirectories(searchFiles, option).ToList();
				dirList.ForEach(dir =>
				{										
					fileList.Add(new FileSystemMetaData
					{
						FileName = dir.Name,
						FileType = FileType.Folder.ToString(),
						FileSize = CalculateFolderSize(dir.FullName)/1000,
						ChildFileCount=dir.GetDirectories().Count()+dir.GetFiles().Count(),
						FileRelativePath = GetRelativePath(parentFolder,dir.FullName)
					});
				});

				List<FileInfo> systemFileList = dirInfo.GetFiles(searchFiles, option).ToList();
				systemFileList.ForEach(file =>
				{					
					fileList.Add(new FileSystemMetaData
					{
						FileName = file.Name,
						FileType = FileType.File.ToString(),
						FileSize = file.Length/1000,
						ChildFileCount = 0,
						FileRelativePath = GetRelativePath(parentFolder, file.FullName)
					});
				});
			}
			catch(Exception e){
				error = e.Message;
			}

			return fileList;
		}

		private static string GetRelativePath(string parentFolder,string FullPath)
		{		
			int startIndex = FullPath.IndexOf(parentFolder);
			if (startIndex >= 0)
				return FullPath.Substring(startIndex);
			else
				return FullPath;
		}

		private static long CalculateFolderSize(string folder)
		{
			long folderSize = 0;
			try
			{
				//Checks if the path is valid or not
				if (!Directory.Exists(folder))
					return folderSize;
				else
				{
					try
					{
						foreach (string file in Directory.GetFiles(folder))
						{
							if (File.Exists(file))
							{
								FileInfo finfo = new FileInfo(file);
								folderSize += finfo.Length;
							}
						}

						foreach (string dir in Directory.GetDirectories(folder))
							folderSize += CalculateFolderSize(dir);
					}
					catch (NotSupportedException e)
					{
						Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
					}
				}
			}
			catch (UnauthorizedAccessException e)
			{
				Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
			}
			return folderSize;
		}

	}
}
