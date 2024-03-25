using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Enums;

public class MediaType : Enumeration
{
    public static MediaType Mpeg = new MediaType(1, "MPEG audio file");
    public static MediaType Mpeg4 = new MediaType(2, "Protected MPEG-4 video file");
    public static MediaType ProtectedAac = new MediaType(3, "Protected AAC audio file");
    public static MediaType PurchasedAac = new MediaType(4, "Purchased AAC audio file");
    public static MediaType Aac = new MediaType(5, "AAC audio file");
    
    protected MediaType(int id, string name) : base(id, name)
    {
    }
}