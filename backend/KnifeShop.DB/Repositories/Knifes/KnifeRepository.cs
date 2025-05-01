using KnifeShop.Contracts.Category;
using KnifeShop.Contracts.Knife;
using KnifeShop.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnifeShop.DB.Repositories.Knifes
{
    public class KnifeRepository : IKnifeRepository
    {
        private readonly ApplicationDbContext _context;

        public KnifeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Create(string title, List<long> categoryIds, string? description, string? image, List<string>? images, double price, bool isOnSale,
            double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade)
        {
            var knifeInfo = new KnifeInfo(overallLength, bladeLength, buttThickness, weight, handleMaterial, country, manufacturer, steelGrade);
            await _context.KnifesInfo.AddAsync(knifeInfo);

            var knife = new Knife(title, description, image, images, price, isOnSale);
            knife.KnifeInfo = knifeInfo;

            var newKnifeCategories = categoryIds.Select(id => new KnifeCategory
            {
                KnifeId = knife.Id,
                CategoryId = id
            }).ToList();

            knife.KnifeCategories = newKnifeCategories;

            await _context.Knifes.AddAsync(knife);
            await _context.SaveChangesAsync();

            return knife.Id;
        }

        public async Task<long> Edit(long knifeId, string title, List<long> categoryIds, string description, string? image, List<string>? images, double price, bool isOnSale,
            double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade)
        {
            var knife = await _context.Knifes
                .Include(k => k.KnifeInfo)
                .Include(k => k.KnifeCategories)
                .FirstOrDefaultAsync(k => k.Id == knifeId);

            if (knife is not null)
            {
                knife.Title = title;
                knife.Description = description;
                knife.Price = price;
                knife.IsOnSale = isOnSale;

                if (image is not null) { knife.Image = image; }
                if (images is not null) { knife.Images = images; }

                if (knife.KnifeInfo is not null)
                {
                    knife.KnifeInfo.OverallLength = overallLength;
                    knife.KnifeInfo.BladeLength = bladeLength;
                    knife.KnifeInfo.ButtThickness = buttThickness;
                    knife.KnifeInfo.Weight = weight;
                    knife.KnifeInfo.HandleMaterial = handleMaterial;
                    knife.KnifeInfo.Country = country;
                    knife.KnifeInfo.Manufacturer = manufacturer;
                    knife.KnifeInfo.SteelGrade = steelGrade;
                }
                else
                {
                    knife.KnifeInfo = new KnifeInfo(overallLength, bladeLength, buttThickness, weight, handleMaterial, country, manufacturer, steelGrade);
                }

                var newKnifeCategories = categoryIds.Select(id => new KnifeCategory
                {
                    KnifeId = knifeId,
                    CategoryId = id
                }).ToList();

                knife.KnifeCategories = newKnifeCategories;

                await _context.SaveChangesAsync();
                return knifeId;
            }

            return 0;
        }

        public async Task<(List<GetKnifesResponse> Items, int TotalCount)> GetPaginated(string? search, string? sortItem, string? order, int page, int pageSize, Guid? userId, int? categoryId)
        {
            var query = _context.Knifes
                .Include(k => k.KnifeInfo)
                .Include(k => k.KnifeCategories)
                    .ThenInclude(kc => kc.Category)
                .Where(k =>
                    (string.IsNullOrWhiteSpace(search) || k.Title.ToLower().Contains(search.ToLower())) &&
                    (categoryId == null || k.KnifeCategories.Any(kc => categoryId == kc.CategoryId))
                    );

            Expression<Func<Knife, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => knife => knife.CreatedAt,
                "title" => knife => knife.Title,
                "price" => knife => knife.Price,
                _ => knife => knife.Id
            };

            if (order == "desc")
            {
                query = query
                    .OrderByDescending(k => k.IsOnSale)
                    .ThenByDescending(selectorKey);
            }
            else
            {
                query = query
                    .OrderByDescending(k => k.IsOnSale)
                    .ThenBy(selectorKey);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(k => new GetKnifesResponse
                {
                    Id = k.Id,
                    Title = k.Title,
                    Categories = k.KnifeCategories
                        .Where(kc => kc.Category != null)
                        .Select(kc => new CategoryDto { Id = kc.CategoryId, Name = kc.Category!.Name })
                        .ToList(),
                    Image = k.Image,
                    Price = k.Price,
                    IsOnSale = k.IsOnSale,
                    IsFavorite = userId.HasValue && k.FavoriteKnifes.Any(f => f.UserId == userId)
                })
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<List<GetKnifesResponse>> GetOnSale(string? search, string? sortItem, string? order)
        {
            var query = _context.Knifes
                .Include(k => k.KnifeInfo)
                .Include(k => k.KnifeCategories)
                    .ThenInclude(kc => kc.Category)
                .Where(k => k.IsOnSale)
                .Where(k => string.IsNullOrWhiteSpace(search) ||
                            k.Title.ToLower().Contains(search.ToLower()));

            Expression<Func<Knife, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => knife => knife.CreatedAt,
                "title" => knife => knife.Title,
                "price" => knife => knife.Price,
                _ => knife => knife.Id
            };

            query = order == "desc"
                ? query.OrderByDescending(selectorKey)
                : query.OrderBy(selectorKey);

            return await query
               .Select(k => new GetKnifesResponse
               {
                   Id = k.Id,
                   Title = k.Title,
                   Categories = k.KnifeCategories
                       .Where(kc => kc.Category != null)
                       .Select(kc => new CategoryDto { Id = kc.CategoryId, Name = kc.Category!.Name })
                       .ToList(),
                   Image = k.Image,
                   Price = k.Price,
                   IsOnSale = k.IsOnSale
               }).ToListAsync();
        }

        public async Task<GetKnifeResponse?> Get(long knifeId, Guid? userId)
        {
            var knife = await _context.Knifes
                .Include(k => k.KnifeInfo)
                .Include(k => k.KnifeCategories)
                    .ThenInclude(kc => kc.Category)
                .FirstOrDefaultAsync(k => k.Id == knifeId);

            if (knife == null)
                return null;

            var isFavorite = userId.HasValue &&
                await _context.FavoriteKnifes.AnyAsync(f => f.UserId == userId && f.KnifeId == knifeId);

            return new GetKnifeResponse
            {
                Id = knife.Id,
                Title = knife.Title,
                Categories = knife.KnifeCategories
                        .Where(kc => kc.Category != null)
                        .Select(kc => new CategoryDto { Id = kc.CategoryId, Name = kc.Category!.Name })
                        .ToList(),
                Description = knife.Description,
                Image = knife.Image,
                Images = knife.Images,
                Price = knife.Price,
                IsOnSale = knife.IsOnSale,
                CreatedAt = knife.CreatedAt,
                KnifeInfo = new KnifeInfoDto
                { 
                    BladeLength = knife?.KnifeInfo?.BladeLength,
                    ButtThickness = knife?.KnifeInfo?.ButtThickness,
                    Country = knife?.KnifeInfo?.Country,
                    HandleMaterial = knife?.KnifeInfo?.HandleMaterial,
                    Manufacturer = knife?.KnifeInfo?.Manufacturer,
                    OverallLength = knife?.KnifeInfo?.OverallLength,
                    SteelGrade = knife?.KnifeInfo?.SteelGrade,
                    Weight = knife?.KnifeInfo?.Weight
                },
                IsFavorite = isFavorite
            };
        }

        public async Task<long> Delete(long knifeId)
        {
            var knife = await _context.Knifes
                .FirstOrDefaultAsync(k => k.Id == knifeId);

            if (knife is null)
                return 0;

            _context.Knifes.Remove(knife);
            await _context.SaveChangesAsync();
            return knifeId;
        }
    }
}
