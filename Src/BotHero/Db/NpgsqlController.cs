using BotHero.Logging;
using Npgsql;
using System;
using LiteX.DbHelper.Core;
using LiteX.DbHelper.Npgsql;
using System.Data;
using System.Data.Common;

namespace BotHero.Db
{
    

    public class NpgsqlController
    {

        static string serverName = "localhost";
        static string port = "5432";
        static string userName = "postgres";
        static string password = "123";
        static string databaseName = "bothero";

        private static IDbHelper _dbHelper;


        private static string _conn = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                                serverName, port, userName, password, databaseName);
        private static DbParameter[] paramIsActive;

        public static void Initialise()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conn))
                {
                    Logger.Sucess("Carregando Sistema Npgsql");
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public static void CreateUser(int ID)
        {
            try
            {
                
                _dbHelper = new NpgsqlHelper(_conn);
                var paramId = DbHelper.CreateParameter("@Id", ID);
                var count = _dbHelper.ExecuteScalar<int>("SELECT id FROM bothero_users where id = @Id", CommandType.Text, paramId);
                
                if(count == 0)
                {
                    Logger.Error("FUCK");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return;
            }
        }
    }
}