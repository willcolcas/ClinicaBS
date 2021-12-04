using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class CitaRepository : BaseRepository, ICitaRepository
    {
        public void delete(int id)
        {
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_cita_cancel";
                db.Query<EntityCita>(sql: sql, commandType: CommandType.StoredProcedure, param: parameters);
            }
        }

        public List<EntityCita> findAll()
        {
            var res = new List<EntityCita>();
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_cita_findAll";
                res = db.Query<EntityCita>(sql,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public EntityCita findById(int id)
        {
            var res = new EntityCita();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_cita_findById";
                res = db.Query<EntityCita>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityCita>();
            }
            return res;
        }

        public EntityCita save(EntityCita cita)
        {
            var res = new EntityCita();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", cita.id);
                parameters.Add(@"id_usuario", cita.id_usuario);
                parameters.Add(@"id_sucursal", cita.id_sucursal);
                parameters.Add(@"id_especialidad", cita.id_especialidad);
                parameters.Add(@"id_especialista", cita.id_especialista);
                parameters.Add(@"id_horario", cita.id_horario);
                parameters.Add(@"fecha", cita.fecha);
                const string sql = @"sp_cita_save";
                res = db.Query<EntityCita>(
                    sql: sql,
                    commandType: CommandType.StoredProcedure,
                    param: parameters
                ).FirstOrDefault<EntityCita>();
            }
            return res;
        }

        public Pagination<CitaExtend> paginationByIdUsuario(int id_usuario, string searchText = "", int page = 1, int numItems = 10)
        {
            var pagination = new Pagination<CitaExtend>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_cita_pagination_byIdUsuario";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@id_usuario", id_usuario);
                parameters_01.Add("@search_text", searchText);
                parameters_01.Add("@num_items", numItems);
                parameters_01.Add("@page", page - 1);
                pagination.content = db.Query<CitaExtend>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).ToList();

                DynamicParameters parameters_02 = new DynamicParameters();
                parameters_02.Add("@id_usuario", id_usuario);
                parameters_02.Add("@search_text", searchText);
                parameters_02.Add("@num_items", numItems);
                const string sql_02 = @"sp_cita_totalPages_byIdUsuario";
                pagination.totalPages = db.Query<int>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).FirstOrDefault<int>();
                pagination.currentPage = page;
            }
            return pagination;
        }
    }
}
