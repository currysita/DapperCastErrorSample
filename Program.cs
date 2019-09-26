using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Models.Dapper.Mapper;

namespace dapperSample
{


    class HogeEntity {
        public double? Column1 {get;set;}
        public double? Column2 {get;set;}
    }
    class Program
    {
        static void Main(string[] args)
        {
            // SqlMapper.AddTypeHandler(typeof(System.Double),new DoubleConverter());
            var sqlConnectionSb = new SqliteConnectionStringBuilder { DataSource = "sqlitedb.sqlite3" };

            using(var con = new SqliteConnection(sqlConnectionSb.ToString())){
                try{
                    con.Execute("drop table Hoge;");
                }catch{}
                var createSql = $@"create table if not exists Hoge (
                    column1 REAL,
                    column2 REAL
                    )";
                con.Execute(createSql);
                var insertSql = "insert into Hoge (column1,column2 ) values (1,36)";
                con.Execute(insertSql);
                insertSql = "insert into Hoge (column1,column2 ) values (2,NULL)";
                con.Execute(insertSql);
                insertSql = "insert into Hoge (column1,column2 ) values (3,36)";
                con.Execute(insertSql);
                var list = con.Query<HogeEntity>(
                    "select column1 Column1 ,column2 Column2 from Hoge where column1 in @Column1 order by column2 IS NULL DESC"
                    ,new {Column1=new List<int>{1,2,3}});
                foreach(var hoge in list){
                    Console.WriteLine(hoge.Column1);
                    Console.WriteLine(hoge.Column2);
                }

            }

        }
    }
}

namespace Models.Dapper.Mapper{  
    public class DoubleConverter : SqlMapper.TypeHandler<double>{  
        public override Double Parse (Object value)  
        {  
            return Convert.ToDouble(value);  
        }  
        public override void SetValue(IDbDataParameter parameter, double value){  
            parameter.Value = value;  
        }  
    }
}