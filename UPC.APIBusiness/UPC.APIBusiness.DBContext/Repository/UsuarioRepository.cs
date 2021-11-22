using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public void delete(int id)
        {
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_usuario_delete";
                db.Query(sql: sql, commandType: CommandType.StoredProcedure, param: parameters);
            }
        }

        public List<EntityUsuario> findAll()
        {
            var res = new List<EntityUsuario>();
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_usuario_findAll";
                res = db.Query<EntityUsuario>(sql,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public EntityUsuario findById(int id)
        {
            var res = new EntityUsuario();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_usuario_findById";
                res = db.Query<EntityUsuario>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityUsuario>();
            }
            return res;
        }

        public EntityUsuario save(EntityUsuario usuario)
        {
            var res = new EntityUsuario();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", usuario.id);
                parameters.Add("@nombres", usuario.nombres);
                parameters.Add("@apellidos", usuario.apellidos);
                parameters.Add("@email", usuario.email);
                parameters.Add("@id_tipo_documento", usuario.id_tipo_documento);
                parameters.Add("@nro_documento", usuario.nro_documento);
                parameters.Add("@id_tipo_usuario", usuario.id_tipo_usuario);
                parameters.Add("@contrasena", usuario.contrasena);

                const string sql = @"sp_usuario_save";
                res = db.Query<EntityUsuario>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityUsuario>();
            }
            return res;
        }

        public Pagination<UsuarioExtend> pagination(string searchText = "", int page = 1, int numItems = 10)
        {
            var pagination = new Pagination<UsuarioExtend>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_usuario_pagination";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@search_text", searchText);
                parameters_01.Add("@num_items", numItems);
                parameters_01.Add("@page", page - 1);
                pagination.content = db.Query<UsuarioExtend>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).ToList();

                DynamicParameters parameters_02 = new DynamicParameters();
                parameters_02.Add("@search_text", searchText);
                parameters_02.Add("@num_items", numItems);
                const string sql_02 = @"sp_usuario_totalPages";
                pagination.totalPages = db.Query<int>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).FirstOrDefault<int>();

                pagination.currentPage = page;

            }
            return pagination;
        }

        public EntityUsuario login(Login login)
        {
            var res = new EntityUsuario();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@email", login.email);
                parameters.Add("@contrasena", login.contrasena);
                const string sql = @"sp_usuario_login";
                res = db.Query<EntityUsuario>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityUsuario>();
            }
            return res;
        }
    }
}
