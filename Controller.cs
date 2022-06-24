using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;
using System.Data.Common;

using TodoBackend;

namespace TodoAppBackend.Controller
{
    public class Controller : ControllerBase{

        protected static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public static String ConnectionString { get; set; }

        protected JsonResult Query<T>(string query)
        {
            return Query<T>(query, null);
        }

        protected JsonResult Query<T>(string query, object? obj)
        {
            log.Info($"Path: {Request.Path} Query: {Request.QueryString} SQL: \"{query}\"");

            using(var conn = this.conn())
                if (obj == null)
                    return new JsonResult(conn.Query<T>(query));
                else
                    return new JsonResult(conn.Query<T>(query, obj));
        }

        protected IActionResult Execute(string query)
        {
            return Execute(query, null);
        }

        protected IActionResult Execute(string query, object? obj)
        {
            log.Info($"Path: {Request.Path} Query: {Request.QueryString} SQL: \"{query}\"");

            using(var conn = this.conn())
                if (obj == null){
                    conn.Execute(query);
                }
                else
                {
                    conn.Execute(query, obj);
                }

            return StatusCode(204);
        }

        protected DbConnection conn()
        {
            DbConnection conn = new NpgsqlConnection(ConnectionString);

            conn.Open();

            return conn;
        }

    }
}