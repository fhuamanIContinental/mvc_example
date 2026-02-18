using DtoModel.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.General.Contratos
{
    public interface ICrudRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T request);
        Task Update(T request);
        Task Delete(int id);
    }
}
