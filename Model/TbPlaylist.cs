using System;
using System.Collections.Generic;

namespace MusicApplication.Model;

public partial class TbPlaylist
{
    public int PlstId { get; set; }

    public string? PlstName { get; set; }

    public int? PlstUserId { get; set; }

    public int[]? PlstMusicsId { get; set; }

    public virtual TbUser? PlstUser { get; set; }
}
