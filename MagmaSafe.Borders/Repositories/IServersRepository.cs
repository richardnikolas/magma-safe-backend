using System;
using MagmaSafe.Borders.Entities;
using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories {
    public interface IServersRepository {

        Task<Server> GetById(string id);
    }
}
