using Extensions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Skyve.Domain.CS2.Utilities;
public class LogTrace : ILogTrace
{
	public LogTrace(string type, string title, DateTime timestamp, string sourceFile)
	{
		Type = type;
		Title = title;
		Timestamp = timestamp;
		SourceFile = sourceFile;
		Trace = [];
	}

	public string Title { get; }
	public DateTime Timestamp { get; }
	public List<string> Trace { get; }
	public string SourceFile { get; }
	public string Type { get; }

	public void AddTrace(string trace)
	{
		Trace.Add(trace.RegexReplace(@" \[0x\w+\] in", " in").RegexRemove(@" in \<\w+\>:\d+"));
	}

	public override string ToString()
	{
		return $"[{Type}] - [{Timestamp:HH:mm:ss,fff}] - ({Path.GetFileName(SourceFile)})\r\n" +
			$"{Title}\r\n" +
			$"{Trace.ListStrings(x => $"\t{x}", "\r\n")}";
	}
}
