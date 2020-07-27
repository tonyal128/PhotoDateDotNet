using System;
public class Photo : IPhoto
{
    public int AlbumId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }

    public Photo(int albumId, int id, string title, string url, string thumbnailUrl)
    {
        AlbumId = albumId;
        Id = id;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Url = url ?? throw new ArgumentNullException(nameof(url));
        ThumbnailUrl = thumbnailUrl ?? throw new ArgumentNullException(nameof(thumbnailUrl));
    }
}