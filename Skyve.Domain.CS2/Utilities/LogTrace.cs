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
		Title = title.RegexReplace(@"(users[/\\]).+?([/\\])", x => $"{x.Groups[1].Value}%username%{x.Groups[2].Value}");
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
		Trace.Add(trace
			.RegexReplace(@"(users[/\\]).+?([/\\])", x => $"{x.Groups[1].Value}%username%{x.Groups[2].Value}")
			.RegexReplace(@" \[0x\w+\] in", " in")			
			.RegexRemove(@" in \<\w+\>:\d+"));
	}

	public override bool Equals(object? obj)
	{
		return obj is LogTrace trace &&
			   Title == trace.Title &&
			   Timestamp == trace.Timestamp &&
			   SourceFile == trace.SourceFile &&
			   Type == trace.Type;
	}

	public override int GetHashCode()
	{
		var hashCode = -1611215073;
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
		hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourceFile);
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
		return hashCode;
	}

	public override string ToString()
	{
		return $"[{Type}] - [{Timestamp:HH:mm:ss,fff}] - ({Path.GetFileName(SourceFile)})\r\n" +
			$"{Title}\r\n" +
			$"{Trace.ListStrings(x => $"\t{x}", "\r\n")}";
	}
}
