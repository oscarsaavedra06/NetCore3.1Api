using CoreBuenasPracticas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    /*interfaz de repositorio generico, para repositorios que comparten los mismosmo métodos, solo cambiando la entidad */
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}
