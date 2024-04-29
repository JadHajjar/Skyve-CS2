using PDX.SDK.Contracts.Service.Mods.Models;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Paradox;
public class PdxForumThreadInfo : IModCommentsInfo
{
	public bool HasMore { get; set; }
	public bool CanPost { get; set; }
	public int Page { get; set; }
	public List<IModComment>? Posts { get; set; }
}
