using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;

namespace Logging.Web.Subscriber.Consumers
{
	/// <summary>
	/// Класс для получения сообщений о поисковых запросах из микросервиса Search
	/// </summary>
	public class SearchQueryLogConsumer : IConsumeAsync<SearchQueryLogMessage>
	{
		private readonly IMapper _mapper;
		private readonly ISearchLogService _searchLogService;

		public SearchQueryLogConsumer(IMapper mapper, ISearchLogService searchLogService)
		{
			_mapper = mapper;
			_searchLogService = searchLogService;
		}

		/// <summary>
		/// Метод для обработки сообщений о поисковых запросах
		/// </summary>
		/// <param name="message">Сообщение о поисковом запросе</param>
		/// <returns></returns>
		public async Task ConsumeAsync(SearchQueryLogMessage message)
		{
			await _searchLogService.AddLogAsync(_mapper.Map<SearchQueryLogEntry>(message));
		}
	}
}