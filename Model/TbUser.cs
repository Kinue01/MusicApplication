using System;
using System.Collections.Generic;

namespace MusicApplication.Model;

public partial class TbUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public virtual ICollection<TbPlaylist> TbPlaylists { get; set; } = new List<TbPlaylist>();
}
