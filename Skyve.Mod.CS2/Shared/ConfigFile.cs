using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
	public class ConfigFile
	{
	}

	public class SaveNameAttribute : Attribute
	{
		public string FileName { get; }
		public string AppName { get; }
		public bool NoBackup { get; }
		public bool Local { get; }
		public bool SuppressErrors { get; }

		public SaveNameAttribute(string fileName, string appName = null, bool noBackup = false, bool local = false, bool suppressErrors = false)
		{
			FileName = fileName is null or "" ? throw new MissingFieldException("FileName must be provided") : fileName;
			AppName = appName;
			NoBackup = noBackup;
			Local = local;
			SuppressErrors = suppressErrors;
		}
	}
}
