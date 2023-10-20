using System;
using System.Collections.Generic;

namespace MusicApplication;

public partial class TbAlbum
{
    public int AlbumId { get; set; }

    public string AlbumName { get; set; } = null!;

    public virtual ICollection<TbAuthor> TbAuthors { get; set; } = new List<TbAuthor>();
}
