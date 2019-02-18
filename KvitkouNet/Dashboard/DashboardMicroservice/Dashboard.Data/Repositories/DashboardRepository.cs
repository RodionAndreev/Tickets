using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dashboard.Data.Context;
using Dashboard.Data.DbModels;
using Dashboard.Data.DbModels.DbEnums;
using System;

namespace Dashboard.Data.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DashboardContext _context;

        public DashboardRepository(DashboardContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Добавляет новость в БД
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        public async Task<string> Add(NewsDb news)
        {
            try
            {
                news.CreatedDate = DateTime.UtcNow;

                _context.News.Add(news);
                await _context.SaveChangesAsync();
                return _context.News.Last().NewsId;
            }
            catch (Exception)
            {
                return ("Exception Data");
            }
        }

        /// <summary>
        ///     Удаление всех новостей в БД
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAll()
        {
            _context.News.RemoveRange(_context.News);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        ///     Удаление новости с определенным Id в БД
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task Delete(string newsId)
        {
            var origin = await _context.News.FindAsync(newsId);

            if (origin != null)
            {
                _context.News.Remove(origin);
                _context.SaveChanges();
            }

        }

        /// <summary>
        ///     Получение всех новостей имеющихся в системе в БД
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NewsDb>> GetAll()
        {
            var res = _context.News
               .Include(db => db.Ticket)
               .AsNoTracking();
            return await res.ToListAsync();

        }

        /// <summary>
        ///     Получение новости по Id в БД
        /// </summary>
        /// <param name="newsIdGuid">Id новости</param>
        /// <returns></returns>
        public Task<NewsDb> Get(string newsId)
        {
            return _context.News
                .Include(db => db.Ticket)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.NewsId == newsId);
        }

        /// <summary>
        ///     Автоматически добавляет новость в БД
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        public async Task AddAutoNews(NewsDb news)
        {
            news.CreatedDate = DateTime.UtcNow;
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }
    }
}
