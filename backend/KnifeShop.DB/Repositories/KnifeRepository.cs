using KnifeShop.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnifeShop.DB.Repositories
{
    public class KnifeRepository : IKnifeRepository
    {
        private readonly ApplicationDbContext _context;

        public KnifeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Create(string title, string category, string? description, string? image, List<string>? images, double price, bool isOnSale,
            double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade)
        {
            var knifeInfo = new KnifeInfo(overallLength, bladeLength, buttThickness, weight, handleMaterial, country, manufacturer, steelGrade);
            await _context.KnifesInfo.AddAsync(knifeInfo);

            var dateNow = DateTime.UtcNow; // for Postgres
            var knife = new Knife(title, category, description, image, images, price, isOnSale, dateNow);
            knife.KnifesInfo = knifeInfo;

            await _context.Knifes.AddAsync(knife);
            await _context.SaveChangesAsync();

            return knife.Id;
        }

        public async Task<Knife?> Edit(long id, string title, string category, string description, string? image, List<string>? images, double price, bool isOnSale,
            double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade)
        {
            var knife = await _context.Knifes
                .Include(k => k.KnifesInfo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(knife is not null)
            {
                knife.Title = title;
                knife.Category = category;
                knife.Description = description;
                knife.Price = price;
                knife.IsOnSale = isOnSale;

                if (image is not null) { knife.Image = image; }
                if (images is not null) { knife.Images = images; }

                knife.KnifesInfo.OverallLength = overallLength;
                knife.KnifesInfo.BladeLength = bladeLength;
                knife.KnifesInfo.ButtThickness = buttThickness;
                knife.KnifesInfo.Weight = weight;
                knife.KnifesInfo.HandleMaterial = handleMaterial;
                knife.KnifesInfo.Country = country;
                knife.KnifesInfo.Manufacturer = manufacturer;
                knife.KnifesInfo.SteelGrade = steelGrade;

                await _context.SaveChangesAsync();
            }

            return knife;
        }

        public async Task<(List<Knife> Items, int TotalCount)> GetPaginated(string? search, string? sortItem, string? order, int page = 1, int pageSize = 10)
        {
            var notesQuery = _context.Knifes
                .Include(k => k.KnifesInfo)
                .Where(n => string.IsNullOrWhiteSpace(search) ||
                            n.Title.ToLower().Contains(search.ToLower()));

            Expression<Func<Knife, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => note => note.CreatedAt,
                "title" => note => note.Title,
                "price" => note => note.Price,
                _ => note => note.Id
            };

            notesQuery = order == "desc"
                ? notesQuery.OrderByDescending(selectorKey)
                : notesQuery.OrderBy(selectorKey);

            int totalCount = await notesQuery.CountAsync();

            var items = await notesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<List<Knife>> GetOnSale(string? search, string? sortItem, string? order)
        {
            var notesQuery = _context.Knifes
                .Include(k => k.KnifesInfo)
                .Where(n => n.IsOnSale)
                .Where(n => string.IsNullOrWhiteSpace(search) ||
                            n.Title.ToLower().Contains(search.ToLower()));

            Expression<Func<Knife, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => note => note.CreatedAt,
                "title" => note => note.Title,
                "price" => note => note.Price,
                _ => note => note.Id
            };

            notesQuery = order == "desc"
                ? notesQuery.OrderByDescending(selectorKey)
                : notesQuery.OrderBy(selectorKey);

            return await notesQuery.ToListAsync();
        }

        public async Task<Knife?> Get(long id)
        {
            return await _context.Knifes
                .Include(k => k.KnifesInfo)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<long> Delete(long id)
        {
            var knife = await _context.Knifes
                .Include(k => k.KnifesInfo) 
                .FirstOrDefaultAsync(k => k.Id == id);

            if (knife is null)
                return 0; 

            _context.Knifes.Remove(knife);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
