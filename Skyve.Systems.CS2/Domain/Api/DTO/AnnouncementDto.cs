using Skyve.Domain;

using SkyveApi.Domain.CS2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Domain.Api.DTO;
internal class AnnouncementDto : IDTO<AnnouncementData, AnnouncementNotification>
{
	public AnnouncementNotification Convert(AnnouncementData? data)
	{
		if (data == null)
			return new AnnouncementNotification(string.Empty, string.Empty, default);

		return new AnnouncementNotification(data.Title!, data.Description!, data.Date);
	}
}
