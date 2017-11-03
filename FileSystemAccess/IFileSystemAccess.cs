using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAccess
{
	interface IFileSystemAccess
	{
		ICollection<FileSystemMetaData> GetFolderContents(string parentFolder,string searchParms,bool allDirectories,out string error);

	}



	public enum FileType
	{
		Folder=0,
		File=1
	}

	public class FileSystemMetaData
	{
		public string FileName { get; set; }
		public string FileType { get; set; }
		public string FileRelativePath { get; set; }
		public long FileSize { get; set; }
		public int ChildFileCount { get; set; }
		

	}
}
