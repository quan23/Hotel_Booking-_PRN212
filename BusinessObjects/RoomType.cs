using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
