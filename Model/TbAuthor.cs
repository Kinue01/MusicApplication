using System;
using System.Collections.Generic;

namespace MusicApplication;

public partial class TbAuthor
{
    public int AuthId { get; set; }

    public string? AuthName { get; set; }

    public virtual ICollection<TbMusic> TbMusics { get; set; } = new List<TbMusic>();
}
