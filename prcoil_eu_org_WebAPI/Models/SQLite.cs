using System.Data.SQLite;

namespace prcoil_eu_org_WebAPI.Models
{
    internal class SQLite
    {
        //构造函数传入数据库名称
        public SQLite(string inputDbName)
        {
            dbName = inputDbName;
        }

        public string dbName;


        //数据库连接
        SQLiteConnection m_dbConnection;

        //创建一个空的数据库
        public void CreateNewDatabase()
        {
            SQLiteConnection.CreateFile(dbName);//可以不要此句
        }

        //创建一个连接到指定数据库
        public void ConnectToDatabase()
        {
            m_dbConnection = new SQLiteConnection($"Data Source={dbName};Version=3;");//没有数据库则自动创建
            m_dbConnection.Open();
        }

        //在指定数据库中创建一个table
        public void CreateTable()
        {
            string sql = "create table  if not exists main (Id integer PRIMARY KEY AUTOINCREMENT,Class text,Name text,Num text,Score text,AssignScore text,GradeRanking text,ClassRanking text,Chinese text,ChineseRanking text,Math text,MathRanking text,English text,EnglishRanking text,Physics text,PhysicsRanking text,History text,HistoryRanking text,Chemistry text,AssignChemistry text,ChemistryRanking text,Organism text,AssignOrganism text,OrganismRanking text,Politics text,AssignPolitics text,PoliticsRanking text,Geography text,AssignGeography text,GeographyRanking text)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //插入一些数据
        public void FillTable(string candidatenumber, string name, string classn, string chinese, string math, string english, string history, string physics, string chemistry, string organism, string politics, string geography, string totalscore)
        {
            string sql = $"insert into main (candidatenumber,name,classn,chinese,math,english,history,physics,chemistry,organism,politics,geography,totalscore) values ({candidatenumber}, '{name}','{classn}','{chinese}','{math}','{english}','{history}','{physics}','{chemistry}','{organism}','{politics}','{geography}','{totalscore}')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //使用sql查询语句，并显示结果
        public string Select(string name, string input)
        {
            //string sql = "select * from highscores order by score desc";
            string sql = $"SELECT {input} FROM SeniorTwo1 WHERE Name = '{name}' ORDER BY ID DESC";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                //将SQLiteCommand对象转换为String类型
                string Class = reader.GetString(reader.GetOrdinal($"{input}"));
                return Class;
            }
            return "DataNotFound";
        }






        //prcoil.eu.org---------------------------------------------------------------------------------------------------------------------
        //插入数据
        public void FillTableRegister(string Email, string CellPhone, string Passworld, string UserName)
        {
            string sql = $"insert into UsersDataMain (Email,CellPhone,Passworld,UserName,RegistrationTime) values ('{Email}', '{CellPhone}','{Passworld}','{UserName}',strftime('%s','now'))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //查询
        public string WebDataSelect(string OutputType, string SearchType, string SearchData)
        {
            //string sql = "select * from highscores order by score desc";
            string sql = $"SELECT {OutputType} FROM UsersDataMain WHERE {SearchType} = '{SearchData}' ORDER BY Id DESC";
            //             (搜索输出的类型，搜索哪一列)                       (哪一列)  <==  (那一列的那个值)

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                //将SQLiteCommand对象转换为String类型
                string Class = reader.GetString(reader.GetOrdinal($"{OutputType}"));
                return Class;
            }
            //未查询到:
            return "DataNotFound";
        }
    }
}
