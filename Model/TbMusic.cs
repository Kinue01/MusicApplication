using System;
using System.Collections.Generic;

namespace MusicApplication.Model;

public partial class TbMusic
{
    public int MusicId { get; set; }

    public string? MusicName { get; set; }

    public string? MusicPath { get; set; }

    public int? MusicAuthorId { get; set; }

    public virtual TbAuthor? MusicAuthor { get; set; }
}
