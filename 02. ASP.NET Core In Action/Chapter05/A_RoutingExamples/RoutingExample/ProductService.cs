namespace RoutingExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService
    {
        private static readonly IDictionary<string, Product> _allProducts
            = new Dictionary<string, Product>
            {
                {"big-widget", new Product("Big Widget", 123) },
                {"super-fancy-widget", new Product("Super fancy widget", 456) },
            };

        public Product GetProduct(string name)
            => _allProducts.TryGetValue(name, out var product) ? product : null;

        public List<Product> Search(string term, StringComparison comparisonType)
            => _allProducts
                .Where(x => x.Value.Name.Contains(term, comparisonType))
                .Select(x => x.Value)
                .ToList();
    }
}
