﻿using OnlineShop.Api.Entities;
using OnlineShop.Shared.Dtos;

namespace OnlineShop.Api.Extensions
{
    public static class DtoConversion
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products.ToArray()
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = product.CategoryId,
                        CategoryName = productCategory.Name
                    }).ToArray();

        }

        public static ProductDto ConvertDto(this Product product, ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return from cartItem in cartItems.ToArray()
                   join product in products
                   on cartItem.ProductId equals product.Id
                   select new CartItemDto
                   {
                       Id = cartItem.Id,
                       ProductId = cartItem.ProductId,
                       ProductName = product.Name,
                       ProductDescription = product.Description,
                       ProductImageUrl = product.ImageUrl,
                       Price = product.Price,
                       CardId = cartItem.CartId,
                       Quantity = cartItem.Quantity,
                       TotalPrice = product.Price * cartItem.Quantity
                   };
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageUrl = product.ImageUrl,
                Price = product.Price,
                CardId = cartItem.CartId,
                Quantity = cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity
            };
        }
    }
}
