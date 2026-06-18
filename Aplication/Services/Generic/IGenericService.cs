using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Generic
{
    public interface IGenericService<T> where T : class
    {
        Task<ResponseBase<T>> GetByIdAsync(int id);
        Task<ResponseBase<IEnumerable<T>>> GetAllAsync();
        Task<ResponseBase<int>> AddAsync(T entity);
        Task<ResponseBase<bool>> UpdateAsync(T entity);
        Task<ResponseBase<T>> DeleteAsync(int id);
    }
}
