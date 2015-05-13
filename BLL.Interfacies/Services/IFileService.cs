using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IFileService
    {
        IEnumerable<FileEntity> GetAllFileEntities();
        void CreateFile(FileEntity file);
    }
}