using System.Collections.Generic;
using Bogus;
using Logging.Data.DbModels;

namespace Logging.Data.Fakers
{
	public static class InternalErrorLogEntryFaker
	{
		private static Faker<InternalErrorLogEntryDbModel> _faker;

		static InternalErrorLogEntryFaker()
		{
			_faker = new Faker<InternalErrorLogEntryDbModel>();
			_faker.RuleFor(_ => _.Id, f => f.IndexFaker.ToString());
			_faker.RuleFor(_ => _.ServiceName, f => f.Company.ToString());
			_faker.RuleFor(_ => _.ExceptionType, f => f.Lorem.Word().ToString());
			_faker.RuleFor(_ => _.InnerExceptionString, f => f.Lorem.Sentence(wordCount: 5));
			_faker.RuleFor(_ => _.Message, f => f.Lorem.Sentence(wordCount: 5));
			_faker.RuleFor(_ => _.Source, f => f.Lorem.Sentence(wordCount: 5));
			_faker.RuleFor(_ => _.StackTrace, f => f.Lorem.Sentence(wordCount: 10));
			_faker.RuleFor(_ => _.TargetSiteName, f => f.Lorem.Word());
			_faker.RuleFor(_ => _.ExceptionType, f => f.Lorem.Word());
			_faker.RuleFor(_ => _.HResult, f => f.Random.Int(-100, 100));
		}

		public static IEnumerable<InternalErrorLogEntryDbModel> Generate(int count = 100)
			=> _faker.Generate(count);
	}
}