using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TeaShopDemo.Models;

namespace TeaShopDemo.Services
{
    public class NewsletterService
    {
        private readonly TeaShopDemoContext _context;

        public NewsletterService(TeaShopDemoContext context)
        {
            _context = context;
        }              

        public async Task<List<Newsletter>> GetAllNewslettersAsync()
        {
            return await _context.Newsletters.ToListAsync();
        }

        public async Task CreateNewsletterAsync(Newsletter newsletter)
        {
            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();
        }
        public async Task AddSubscriptionAsync(NewsletterSubscription subscription)
        {
            _context.NewsletterSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }
        public async Task<List<NewsletterSubscription>> GetAllSubscriptionsAsync()
        {
            return await _context.NewsletterSubscriptions.ToListAsync();
        }
    }
}