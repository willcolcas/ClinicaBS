using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class HorarioRepository : BaseRepository, IHorarioRepository
    {
        public void delete(int id)
        {
            using (var db = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                const string sql = @"sp_horario_delete";
                db.Query(sql: sql, commandType: CommandType.StoredProcedure, param: parameters);
            }
        }
    }
}
