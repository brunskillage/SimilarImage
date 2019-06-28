using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using SimilarImage.Models;

namespace SimilarImage.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _dbConnection;

        public SQLiteConnection GetOpenConnection()
        {
            if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
            {
                CloseConnection();
                var dbFullPath = AppDomain.CurrentDomain.BaseDirectory + "images.db";
                _dbConnection = new SQLiteConnection("Data Source=" + dbFullPath + ";Version=3;");
                _dbConnection.Open();
            }

            return _dbConnection;
        }

        public void CloseConnection()
        {
            if (_dbConnection == null)
                return;

            _dbConnection.Close();
            _dbConnection.Dispose();
            _dbConnection = null;
        }

        public int GetImageRecordCount()
        {
            return _dbConnection.ExecuteScalar<int>("select count(id) from ImageStat");
        }

        public void InsertImageStat(ImageStat imageStat)
        {
            _dbConnection.ExecuteAsync(
                "insert into ImageStat (rectangles, luminance, blue, red, green, cb,cr,y, pixels, imagePath, BlueMax,BlueMin,GreenMax,GreenMin,RedMax,RedMin, length) values (@rectangles, @luminance, @blue, @red, @green, @cb, @cr, @y, @pixels, @imagePath,@BlueMax,@BlueMin,@GreenMax,@GreenMin,@RedMax,@RedMin,@length)",
                imageStat);
        }

        public void DeleteImageStatById(int id)
        {
            _dbConnection.Execute("delete from ImageStat where Id = @id", new {id});
        }

        public void DeleteImageStats()
        {
            _dbConnection.Execute("delete from ImageStat where Id > 0");
        }

        public ImageStat[] GetImageStatsNotMoved(FormOptions options)
        {
            var stats = GetAllImageStats(options).Where(s => s.IsMoved != 1);
            return stats.ToArray();
        }

        public ImageStat[] GetAllImageStats(FormOptions options)
        {
            var stats = _dbConnection.Query<ImageStat>("select * from ImageStat;");
            foreach (var stat in stats)
                stat.ImagePath = options.ImagesSoureDirectory + stat.ImagePath;

            return stats.ToArray();
        }

        public ImageStat[] GetDuplicates(FormOptions options)
        {
            var stats = GetImageStatsNotMoved(options).Where(i => i.IsDuplicate == 1).ToArray();

            return stats;
        }

        public void SetDuplicateValue(int id, bool val)
        {
            var isDuplicate = val ? 1 : 0;
            _dbConnection.Execute("update ImageStat set IsDuplicate = @isDuplicate where id=@id", new {id, isDuplicate});
        }

        public void TestConnection()
        {
            try
            {
                _dbConnection = GetOpenConnection();
                var command = _dbConnection.CreateCommand();
                command.CommandText = "vacuum";
                command.ExecuteNonQueryAsync();
            }
            catch (SQLiteException)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SetMoved(int id, bool val)
        {
            var isMoved = val ? 1 : 0;
            _dbConnection.Execute("update ImageStat set IsMoved = @isMoved where id=@id", new {id, isMoved});
        }

        public List<string> GetTables()
        {
            return _dbConnection.Query<string>("SELECT name, sql FROM sqlite_master WHERE type = 'table'").ToList();
        }

        public void CreateTables()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS ImageStat (
	                        id	INTEGER PRIMARY KEY AUTOINCREMENT,
	                        rectangles	INTEGER,
	                        luminance	NUMERIC,
	                        blue	INTEGER,
	                        red	INTEGER,
	                        green	INTEGER,
	                        cb	INTEGER,
	                        cr	INTEGER,
	                        y	INTEGER,
	                        pixels	INTEGER,
	                        imagePath	TEXT,
	                        IsDuplicate	INTEGER DEFAULT 0,
	                        IsMoved	INTEGER DEFAULT 0,
	                        BlueMax	INTEGER,
	                        BlueMin	INTEGER,
	                        RedMax	INTEGER,
	                        RedMin	INTEGER,
	                        GreenMax	INTEGER,
	                        GreenMin	INTEGER,
	                        length	INTEGER
                        );";
            _dbConnection.Execute(sql);
        }
    }
}