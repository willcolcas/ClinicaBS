using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class EspecialidadRepository : BaseRepository, IEspecialidadRepository
    {
        public void delete(int id)
        {
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_especialidad_delete";
                db.Query<EntityEspecialidad>(sql: sql, commandType: CommandType.StoredProcedure, param: parameters);
            }
        }

        public List<EntityEspecialidad> findAll()
        {
            var res = new List<EntityEspecialidad>();
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_especialidad_findAll";
                res = db.Query<EntityEspecialidad>(sql,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public EntityEspecialidad findById(int id)
        {
            var res = new EntityEspecialidad();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_especialidad_findById";
                res = db.Query<EntityEspecialidad>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityEspecialidad>();
            }
            return res;
        }

        public EntityEspecialidad save(EntityEspecialidad especialidad)
        {
            var res = new EntityEspecialidad();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", especialidad.id);
                parameters.Add("@nombre", especialidad.nombre);
                parameters.Add("@descripcion", especialidad.descripcion);

                const string sql = @"sp_especialidad_save";
                res = db.Query<EntityEspecialidad>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityEspecialidad>();
            }
            return res;
        }

        public Pagination<EntityEspecialidad> pagination(string searchText = "", int page = 1, int numItems = 10)
        {
            var pagination = new Pagination<EntityEspecialidad>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_especialidad_pagination";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@search_text", searchText);
                parameters_01.Add("@num_items", numItems);
                parameters_01.Add("@page", page - 1);
                pagination.content = db.Query<EntityEspecialidad>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).ToList();

                DynamicParameters parameters_02 = new DynamicParameters();
                parameters_02.Add("@search_text", searchText);
                parameters_02.Add("@num_items", numItems);
                const string sql_02 = @"sp_especialidad_totalPages";
                pagination.totalPages = db.Query<int>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).FirstOrDefault<int>();

                pagination.currentPage = page;

            }
            return pagination;
        }
    }
}
