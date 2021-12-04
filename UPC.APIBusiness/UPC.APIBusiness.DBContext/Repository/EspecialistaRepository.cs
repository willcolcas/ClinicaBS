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

        public EntityEspecialista findById(int id)
        {
            var especialista = new EntityEspecialista();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_especialista_findById";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@id", id);
                especialista = db.Query<EntityEspecialista>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).FirstOrDefault<EntityEspecialista>();
            }
            return especialista;
        }

        public EntityEspecialista save(EntityEspecialista especialista)
        {
            using (var db = GetSqlConnection())
            {
                const string sql = @"sp_especialista_save";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@id", especialista.id);
                parameters_01.Add("@nombres", especialista.nombres);
                parameters_01.Add("@apellidos", especialista.apellidos);
                parameters_01.Add("@dni", especialista.dni);
                especialista = db.Query<EntityEspecialista>(sql: sql, commandType: CommandType.StoredProcedure, param: parameters_01).FirstOrDefault<EntityEspecialista>();
            }
            return especialista;
        }

        public void saveHorarios(List<EntityHorario> horarios)
        {
            var especialista = new EntityEspecialista();
            using (var db = GetSqlConnection())
            {
                foreach (var item in horarios)
                {
                    const string sql_02 = @"sp_horario_save";
                    DynamicParameters parameters_02 = new DynamicParameters();
                    parameters_02.Add("@id", item.id);
                    parameters_02.Add("@id_especialista", item.id_especialista);
                    parameters_02.Add("@id_sucursal", item.id_sucursal);
                    parameters_02.Add("@id_especilidad", item.id_especialidad);
                    parameters_02.Add("@tipo", item.tipo);
                    parameters_02.Add("@horario", item.horario);
                    parameters_02.Add("@dia", item.dia);
                    db.Query<EntityHorario>(sql: sql_02, commandType: CommandType.StoredProcedure, param: parameters_02);
                }
            }
        }

        public Pagination<EntityEspecialista> pagination(string searchText = "", int page = 1, int numItems = 10)
        {
            var pagination = new Pagination<EntityEspecialista>();
            using (var db = GetSqlConnection())
            {
                const string sql_01 = @"sp_especialista_pagination";
                DynamicParameters parameters_01 = new DynamicParameters();
                parameters_01.Add("@search_text", searchText);
                parameters_01.Add("@num_items", numItems);
                parameters_01.Add("@page", page - 1);
                pagination.content = db.Query<EntityEspecialista>(sql: sql_01, commandType: CommandType.StoredProcedure, param: parameters_01).ToList();
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
                parameters_01.Add("@dia", filter.dia);
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
                    parameters_02.Add("@id_sucursal", filter.id_sucursal);
                    parameters_02.Add("@id_especialidad", filter.id_especialidad);
                    parameters_02.Add("@dia", filter.dia);
                    parameters_02.Add("@fecha", filter.fecha);

                    item.horarios = db.Query<EntityHorario>(sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).ToList();
                }


                const string sql_03 = @"sp_especialista_filter_totalPages";
                DynamicParameters parameters_03 = new DynamicParameters();
                parameters_03.Add("@id_sucursal", filter.id_sucursal);
                parameters_03.Add("@id_especialidad", filter.id_especialidad);
                parameters_03.Add("@dia", filter.dia);
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

        public List<EntityHorario> loadHorarios(int id, int id_sucursal, int id_especialidad, int dia)
        {
            var horarios = new List<EntityHorario>();
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters_02 = new DynamicParameters();
                const string sql_02 = @"sp_horario_findByIdEspecialista";
                parameters_02.Add("@id_especialista", id);
                parameters_02.Add("@id_sucursal", id_sucursal);
                parameters_02.Add("@id_especialidad", id_especialidad);
                parameters_02.Add("@dia", dia);
                horarios = db.Query<EntityHorario>(sql_02, commandType: CommandType.StoredProcedure, param: parameters_02).ToList();
            }
            return horarios;
        }
    }
}

