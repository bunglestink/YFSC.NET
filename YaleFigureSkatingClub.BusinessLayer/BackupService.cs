using System;
using System.Diagnostics;

namespace YaleFigureSkatingClub.BusinessLayer
{
	public class BackupService : IBackupService
	{
		readonly String backupScriptPath;
		readonly int scriptIgnoreLines;
		
		
		public BackupService(String backupScriptPath, int scriptIgnoreLines)
		{
			this.backupScriptPath = backupScriptPath;
			this.scriptIgnoreLines = scriptIgnoreLines;
		}
		
		
		public string getBackupScript()
		{
			var proc = new Process();
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.FileName = backupScriptPath;
			proc.Start();
			
			for (var i = 0; i < scriptIgnoreLines; i++) {
				proc.StandardOutput.ReadLine();
			}
			
			return proc.StandardOutput.ReadToEnd();
		}
	}
}

