﻿namespace TelerikMovieDatabase.Data.MongoDb
{
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization;
	using MongoDB.Driver;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TelerikMovieDatabase.Data.Json;

	public class MongoDbHandler
	{
		public static readonly string ConnectionString = Settings.Default.ConnectionString;

		public static readonly string DbName = Settings.Default.DbName;
		public static readonly string DbTableMoviesName = Settings.Default.DbTableMoviesName;
		public static readonly string DbTablePersonsName = Settings.Default.DbTablePersonsName;
		public static readonly string DbTableGenresName = Settings.Default.DbTableGenresName;
		public static readonly string DbTableCountriesName = Settings.Default.DbTableCountriesName;
		public static readonly string DbTableLanguagesName = Settings.Default.DbTableLanguagesName;
		public static readonly string DbTableJobPositionsName = Settings.Default.DbTableJobPositionsName;

		private MongoClient client;
		private MongoServer server;
		private MongoDatabase database;

		public void Connect()
		{
			this.client = new MongoClient(ConnectionString);
		}

		public MongoClient GetClient()
		{
			return this.client;
		}

		public MongoServer GetServer()
		{
			if (this.server == null)
			{
				this.server = this.GetClient().GetServer();
			}
			return this.server;
		}

		public MongoDatabase GetDatabase()
		{
			if (this.database == null)
			{
				this.database = this.GetServer().GetDatabase(DbName);
			}

			return this.database;
		}

		public bool DatabaseExists()
		{
			return this.GetServer().DatabaseExists(DbName);
		}

		public void GetMovies()
		{
			var movies = this.GetDatabase().GetCollection<BsonArray>(DbTableMoviesName);
			var persons = this.GetDatabase().GetCollection<BsonArray>(DbTablePersonsName);
			var genres = this.GetDatabase().GetCollection<BsonArray>(DbTableGenresName);
			var countries = this.GetDatabase().GetCollection<BsonArray>(DbTableCountriesName);
			var languages = this.GetDatabase().GetCollection<BsonArray>(DbTableLanguagesName);
			var jobPositions = this.GetDatabase().GetCollection<BsonArray>(DbTableJobPositionsName);
			// TODO
		}

		public void InsertMovies(IEnumerable<object> data)
		{
			this.InsertCollection(this.GetDatabase(), DbTableMoviesName, data);
		}

		public void InsertPersons(IEnumerable<object> data)
		{
			this.InsertCollection(this.GetDatabase(), DbTablePersonsName, data);
		}

		public void InsertGenres(IEnumerable<object> data)
		{
			this.InsertCollection(this.GetDatabase(), DbTableGenresName, data);
		}

		public void InsertCountries(IEnumerable<object> data)
		{
			this.InsertCollection(this.GetDatabase(), DbTableCountriesName, data);
		}

		public void InsertLanguages(IEnumerable<object> data)
		{
			this.InsertCollection(this.GetDatabase(), DbTableLanguagesName, data);
		}

		public void InsertJobPositions(IEnumerable<object> data)
		{
			this.InsertCollection(this.GetDatabase(), DbTableJobPositionsName, data);
		}

		private void InsertCollection(MongoDatabase database, string tableName, IEnumerable<object> data)
		{
			if (data.Any())
			{
				var jsonString = JsonHandler.Serialize(data);
				BsonArray bsonArray = BsonSerializer.Deserialize<BsonArray>(jsonString);
				var collection = database.GetCollection<BsonArray>(tableName);
				collection.InsertBatch(bsonArray);
			}
		}
	}
}