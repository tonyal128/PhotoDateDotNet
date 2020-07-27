using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPhotoService
{
    public Task<List<IPhoto>> GetAlbumByIdAsync(int albumId);
}