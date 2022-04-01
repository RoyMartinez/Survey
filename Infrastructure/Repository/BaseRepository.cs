using Infrastructure.Context;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IConfiguration _config;
        public BaseRepository
        (
            IConfiguration configuration
        )
        {
            _config = configuration;
        }

        /// <summary>
        /// Metodo que agrega una entidad a la base de datos
        /// </summary>
        /// <param name="entity">Entidad a agregar</param>
        public void Add(T entity)
        {
            using (var _context = new SurveyContext(_config))
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo que elimina una entidad de la base de datos
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        public void Delete(T entity)
        {
            using (var _context = new SurveyContext(_config))
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo que edita una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a editar</param>
        public void Edit(T entity)
        {
            using (var _context = new SurveyContext(_config))
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo que edita una lista de entidades en la base de datos
        /// </summary>
        /// <param name="entitie">Lista de entidades a editar</param>
        public void EditRange(T[] entitie)
        {
            using (var _context = new SurveyContext(_config))
            {
                _context.UpdateRange(entitie);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo que busca entidades en la base de datos
        /// </summary>
        /// <param name="predicate">Expresion para buscar las entidades</param>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query;
            using (var _context = new SurveyContext(_config))
            {
                query = _context.Set<T>().Where(predicate).ToList().AsQueryable();
            }
            return query;
        }

        /// <summary>
        /// Metodo que obtiene todas las entidades de una tabla de la base de datos
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query;
            using (var _context = new SurveyContext(_config))
            {
                query = (from p in _context.Set<T>() select p).ToList()
                        .AsQueryable();
            }
            return query;
        }
    }
}
