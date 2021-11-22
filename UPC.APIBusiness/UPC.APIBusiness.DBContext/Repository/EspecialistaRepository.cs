using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class EspecialistaRepository : BaseRepository, IEspecialistaRepository
    {
        public void delete(int id)
        {
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_especialista_delete";
                db.Query(sql: sql, commandType: CommandType.StoredProcedure, param: parameters);
            }
        }

        public List<EspecialistaExtend> findAll()
        {
            var res = new List<EspecialistaExtend>();
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_especialista_findAll";
                res = db.Query<EspecialistaExtend>(sql,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public EspecialistaExtend findById(int id)
        {
            var especialista = new EspecialistaExtend();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_especialista_findById";
                const string sql_02 = @"sp_horario_findByIdEspecialista";

                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@id", id);

                especialista = db.Query<EspecialistaExtend>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).FirstOrDefault<EspecialistaExtend>();

                if (especialista != null)
                {
                    DynamicParameters parameters_02 = new DynamicParameters();
                    parameters_02.Add("@id_especialista", especialista.id);
                    especialista.horarios = db.Query<EntityHorario>(sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).ToList();
                }
            }
            return especialista;
        }

        public EntityEspecialista save(EspecialistaExtend especialistaExtend)
        {
            var especialista = new EntityEspecialista();
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_especialista_save";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@id", especialistaExtend.id);
                parameters_01.Add("@nombres", especialistaExtend.nombres);
                parameters_01.Add("@apellidos", especialistaExtend.apellidos);
                parameters_01.Add("@dni", especialistaExtend.dni);
                parameters_01.Add("@id_especialidad", especialistaExtend.id_especialidad);
                parameters_01.Add("@id_sucursal", especialistaExtend.id_sucursal);
                especialista = db.Query<EntityEspecialista>(sql: sql, commandType: CommandType.StoredProcedure, param: parameters_01).FirstOrDefault<EntityEspecialista>();

                foreach (var item in especialistaExtend.horarios)
                {
                    const string sql_02 = @"sp_horario_save";
                    DynamicParameters parameters_02 = new DynamicParameters();
                    parameters_02.Add("@id", item.id);
                    parameters_02.Add("@id_especialista", especialista.id);
                    parameters_02.Add("@inicio", item.inicio);
                    parameters_02.Add("@fin", item.fin);
                    db.Query<EntityHorario>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02);
                }
            }
            return especialista;
        }

        public Pagination<EspecialistaExtend> pagination(string searchText = "", int page = 1, int numItems = 10)
        {
            var pagination = new Pagination<EspecialistaExtend>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_especialista_pagination";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@search_text", searchText);
                parameters_01.Add("@num_items", numItems);
                parameters_01.Add("@page", page - 1);
                pagination.content = db.Query<EspecialistaExtend>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).ToList();
                DynamicParameters parameters_02 = new DynamicParameters();
                parameters_02.Add("@search_text", searchText);
                parameters_02.Add("@num_items", numItems);
                const string sql_02 = @"sp_especialista_totalPages";
                pagination.totalPages = db.Query<int>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).FirstOrDefault<int>();
                pagination.currentPage = page;
            }
            return pagination;
        }

        public Pagination<EspecialistaExtend> filter(Filter filter)
        {
            var pagination = new Pagination<EspecialistaExtend>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_especialista_filter";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@id_sucursal", filter.id_sucursal);
                parameters_01.Add("@id_especialidad", filter.id_especialidad);
                parameters_01.Add("@num_items", filter.numItems);
                parameters_01.Add("@page", filter.page - 1);
                pagination.content = db.Query<EspecialistaExtend>(
                    sql: sql_01,
                    commandType: CommandType.StoredProcedure,
                    param: parameters_01).ToList();

                const string sql_02 = @"sp_horario_disponible";
                DynamicParameters parameters_02 = new DynamicParameters();
                foreach (var item in pagination.content)
                {
                    parameters_02.Add("@id_especialista", item.id);
                    parameters_02.Add("@fecha", filter.fecha);
                    item.horarios = db.Query<EntityHorario>(sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).ToList();
                }


                const string sql_03 = @"sp_especialista_filter_totalPages";
                DynamicParameters parameters_03 = new DynamicParameters();
                parameters_03.Add("@id_sucursal", filter.id_sucursal);
                parameters_03.Add("@id_especialidad", filter.id_especialidad);
                parameters_03.Add("@num_items", filter.numItems);
                pagination.totalPages = db.Query<int>(sql: sql_03, commandType: CommandType.StoredProcedure, param: parameters_03).FirstOrDefault<int>();

                pagination.currentPage = filter.page;

            }
            return pagination;
        }
        public Pagination<EspecialistaExtend> filter(int id_especialidad, int is_sucursal)
        {
            throw new NotImplementedException();
        }
    }
}
