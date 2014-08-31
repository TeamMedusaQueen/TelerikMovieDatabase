namespace TMDB.Data.Provider.MongoDatabase
{
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization;
	using MongoDB.Driver;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TMDB.Data.Provider.IMDB;

	public class TmdbMongoDbContext
	{
		private const string ConnectionString = "mongodb://localhost";
		private const string DbName = "tmdb";

		private const string DbTableMoviesName = "movies";
		private const string DbTablePersonsName = "persons";
		private const string DbTableGenresName = "genres";
		private const string DbTableProductionCompaniesName = "productionCompanies";
		private const string DbTableCountriesName = "countries";
		private const string DbTableLanguagesName = "languages";
		private const string DbTableJobPositionsName = "jobPositions";

		public void InitialCreate()
		{
			var client = new MongoClient(ConnectionString);
			var server = client.GetServer();
			var database = server.GetDatabase(DbName);

			var modelMapper = new JsonMovieJsonModelMapper();
			var movieJSONModels = OpenMovieDatabase.GetTop250();

			var movieProjections = modelMapper.Map(movieJSONModels);
			var personProjections = modelMapper.GetPersonProjections();
			var genreProjections = modelMapper.GetGenreProjections();
			var productionCompanyProjections = modelMapper.GetProductionCompanyProjections();
			var countryProjections = modelMapper.GetCountryProjections();
			var languageProjections = modelMapper.GetLanguageProjections();
			var jobProjections = modelMapper.GetJobProjections();

			InsertCollection(database, DbTableMoviesName, movieProjections);
			InsertCollection(database, DbTablePersonsName, personProjections);
			InsertCollection(database, DbTableGenresName, genreProjections);
			InsertCollection(database, DbTableProductionCompaniesName, productionCompanyProjections);
			InsertCollection(database, DbTableCountriesName, countryProjections);
			InsertCollection(database, DbTableLanguagesName, languageProjections);
			InsertCollection(database, DbTableJobPositionsName, jobProjections);
		}

		private void InsertCollection(MongoDatabase database, string tableName, IEnumerable<object> data)
		{
			if (data.Any())
			{
				var jsonString = JsonConvert.SerializeObject(data);
				BsonArray bsonArray = BsonSerializer.Deserialize<BsonArray>(jsonString);
				var collection = database.GetCollection<BsonArray>(tableName);
				collection.InsertBatch(bsonArray);
			}
		}
	}
}