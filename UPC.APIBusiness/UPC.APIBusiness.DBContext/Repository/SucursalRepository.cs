using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class SucursalRepository : BaseRepository, ISucursalRepository
    {
        public void delete(int id)
        {
           using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_sucursal_delete";
                db.Query(sql: sql, commandType: CommandType.StoredProcedure, param: parameters);
            }
        }

        public List<EntitySucursal> findAll()
        {
            var res = new List<EntitySucursal>();
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_sucursal_findAll";
                res = db.Query<EntitySucursal>(sql,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public EntitySucursal findById(int id)
        {
            var res = new EntitySucursal();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_sucursal_findById";
                res = db.Query<EntitySucursal>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntitySucursal>();
            }
            return res;
        }

        public EntitySucursal save(EntitySucursal sucursal)
        {
            var res = new EntitySucursal();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", sucursal.id);
                parameters.Add("@nombre", sucursal.nombre);
                parameters.Add("@direccion", sucursal.direccion);
                parameters.Add("@distrito", sucursal.distrito);

                const string sql = @"sp_sucursal_save";
                res = db.Query<EntitySucursal>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntitySucursal>();
            }
            return res;
        }

        public Pagination<EntitySucursal> pagination(string searchText = "", int page = 1, int numItems = 10)
        {
            var pagination = new Pagination<EntitySucursal>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_sucursal_pagination";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@search_text", searchText);
                parameters_01.Add("@num_items", numItems);
                parameters_01.Add("@page", page - 1);
                pagination.content = db.Query<EntitySucursal>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).ToList();

                DynamicParameters parameters_02 = new DynamicParameters();
                parameters_02.Add("@search_text", searchText);
                parameters_02.Add("@num_items", numItems);
                const string sql_02 = @"sp_sucursal_totalPages";
                pagination.totalPages = db.Query<int>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).FirstOrDefault<int>();
                
                pagination.currentPage = page;
                 
            }
            return pagination;
        }
    }
}
