using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams productSpecParams) : base(p =>
        (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search)) &&
            (!productSpecParams.Brands.Any() || productSpecParams.Brands.Contains(p.Brand)) &&
            (!productSpecParams.Types.Any() || productSpecParams.Types.Contains(p.Type)))
        {
            ApplyPaging((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);
            switch (productSpecParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
        // Define properties and methods for product specifications here
    }
}