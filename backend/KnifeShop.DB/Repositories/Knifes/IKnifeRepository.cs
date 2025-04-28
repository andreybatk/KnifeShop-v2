using KnifeShop.Contracts.Knife;
using KnifeShop.DB.Models;

namespace KnifeShop.DB.Repositories.Knifes
{
    public interface IKnifeRepository
    {
        /// <summary>
        /// Create Knife
        /// </summary>
        /// <returns>Id</returns>
        Task<long> Create(string title, List<long> categoryIds, string? description, string? image, List<string>? images, double price, bool isOnSale,
            double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade);
        /// <summary>
        /// Edit Knife
        /// </summary>
        /// <returns>Edited Knife</returns>
        Task<long> Edit(long id, string title, List<long> categoryIds, string description, string? image, List<string>? images, double price, bool isOnSale,
            double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade);
        /// <summary>
        /// Get Knife
        /// </summary>
        /// <returns>Knife</returns>
        Task<GetKnifeResponse?> Get(long knifeId, Guid? userId);
        /// <summary>
        /// Get Knifes On Sale
        /// </summary>
        /// <returns>List<Knife> where IsOnSale is true</returns>
        Task<List<GetKnifesResponse>> GetOnSale(string? search, string? sortItem, string? order);
        /// <summary>
        /// Get Knifes with Pagination
        /// </summary>
        /// <returns>List<Knife>; Total Count elements</returns>
        Task<(List<GetKnifesResponse> Items, int TotalCount)> GetPaginated(string? search, string? sortItem, string? order, int page, int pageSize, Guid? userId, List<long>? CategoryIds);
        /// <summary>
        /// Delete
        /// </summary>
        /// <returns>Id deleted knife</returns>
        Task<long> Delete(long id);
    }
}