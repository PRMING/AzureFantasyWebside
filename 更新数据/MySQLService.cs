using MySql.Data.MySqlClient;

namespace 更新数据
{
    public class MySqlService
    {
        string? _serverip;
        string? _database;
        string? _uid;
        string? _password;

        //构造函数
        public MySqlService(string serverip, string database, string uid, string password)
        {
            _serverip = serverip;
            _database = database;
            _uid = uid;
            _password = password;
        }
        //函数重载
        public MySqlService()
        {
            _serverip = "127.0.0.1";
            _database = "serverdata";
            _uid = "root";
            _password = "248655";
        }



        public void MySqlInsertDaily(string into1, string into2, string into3, string into4, string into5,
                                      string table,
                                      string? value1, string? value2, string? value3, string? value4, string? value5)
        {
            string connectionString = $"Server={_serverip};Database={_database};User Id={_uid};Password={_password};SslMode = none;allowPublicKeyRetrieval=true;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();//打开数据库连接

                string sql = $"INSERT INTO {table} ({into1},{into2},{into3},{into4},{into5}) VALUES (@值1,@值2,@值3,@值4,@值5)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@值1", $"{value1}");
                    cmd.Parameters.AddWithValue("@值2", $"{value2}");
                    cmd.Parameters.AddWithValue("@值3", $"{value3}");
                    cmd.Parameters.AddWithValue("@值4", $"{value4}");
                    cmd.Parameters.AddWithValue("@值5", $"{value5}");
                    cmd.ExecuteNonQuery();

                    connection.Close();//关闭连接
                }

            }
        }
        public void MySqlInsertOne(string into1, string table,string? value1)
        {
            string connectionString = $"Server={_serverip};Database={_database};User Id={_uid};Password={_password};SslMode = none;allowPublicKeyRetrieval=true;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();//打开数据库连接

                string sql = $"INSERT INTO {table} ({into1}) VALUES (@值1)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@值1", $"{value1}");
                    cmd.ExecuteNonQuery();

                    connection.Close();//关闭连接
                }

            }
        }
        

        public string? MySqlSelect(string outputType, string searchType, string? searchData, string table)
        {
            string connectionString = $"Server={_serverip};Database={_database};User Id={_uid};Password={_password};SslMode = none;allowPublicKeyRetrieval=true;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                connection.Open();//打开数据库连接

                string selectQuery = $"SELECT {outputType} FROM {table} WHERE {searchType} = '{searchData}' ORDER BY Id DESC";
                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string? result = reader[$"{outputType}"].ToString();
                            // 使用文件流进行读写操作

                            connection.Close();//关闭数据库
                            return result;
                        }// 在这里，文件流会被自动关闭和释放
                        connection.Close();
                        return "DataNotFound";
                    }
                }
            }
        }

    }
}
