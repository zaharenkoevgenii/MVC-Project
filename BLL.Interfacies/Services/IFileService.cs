using System;
using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IFileService
    {
        IEnumerable<FileEntity> GetAllFileEntities();
        IEnumerable<FileEntity> GetN(int n); 
        void CreateFile(FileEntity file);
        void DeleteFile(Guid id);
    }
}