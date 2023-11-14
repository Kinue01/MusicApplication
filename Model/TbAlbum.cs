using System;
using System.Collections.Generic;

namespace MusicApplication.Model;

public partial class TbAlbum
{
    public int AlbumId { get; set; }

    public string AlbumName { get; set; } = null!;

    public int? AlbAuthorId { get; set; }

    public virtual TbAuthor? AlbAuthor { get; set; }
}
