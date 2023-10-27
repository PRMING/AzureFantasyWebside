using System.Data.SQLite;

namespace prcoil_eu_org
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
        public void createNewDatabase()
        {
            SQLiteConnection.CreateFile(dbName);//可以不要此句
        }

        //创建一个连接到指定数据库
        public void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection($"Data Source={dbName};Version=3;");//没有数据库则自动创建
            m_dbConnection.Open();
        }

        //在指定数据库中创建一个table
        public void createTable()
        {
            string sql = "create table  if not exists main (Id integer PRIMARY KEY AUTOINCREMENT,Class text,Name text,Num text,Score text,AssignScore text,GradeRanking text,ClassRanking text,Chinese text,ChineseRanking text,Math text,MathRanking text,English text,EnglishRanking text,Physics text,PhysicsRanking text,History text,HistoryRanking text,Chemistry text,AssignChemistry text,ChemistryRanking text,Organism text,AssignOrganism text,OrganismRanking text,Politics text,AssignPolitics text,PoliticsRanking text,Geography text,AssignGeography text,GeographyRanking text)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //插入一些数据
        public void fillTable(string candidatenumber, string name, string classn, string chinese, string math, string english, string history, string physics, string chemistry, string organism, string politics, string geography, string totalscore)
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
            return "NotFind";
        }
    }
}
