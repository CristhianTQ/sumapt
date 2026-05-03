using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SUMAPT.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato genérico para el acceso a datos.
/// Define las operaciones estándar para cualquier entidad del sistema.
/// </summary>
/// <typeparam name="T">Clase de entidad (ej. Usuario, Cita, Programa).</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>Busca una entidad por su identificador único (UUID o BIGSERIAL).</summary>
    Task<T?> GetByIdAsync(object id);
    
    /// <summary>Devuelve todos los registros de la tabla.</summary>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>Busca registros que cumplan con una condición específica.</summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>Prepara una entidad para ser insertada.</summary>
    Task AddAsync(T entity);
    
    /// <summary>Marca una entidad como modificada.</summary>
    void Update(T entity);
    
    /// <summary>Marca una entidad para eliminación física.</summary>
    void Delete(T entity);
    
    /// <summary>Ejecuta la transacción SQL en la base de datos.</summary>
    Task SaveChangesAsync();
}