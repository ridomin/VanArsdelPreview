﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VanArsdel.Data;
using VanArsdel.Inventory.Models;

namespace VanArsdel.Inventory.Providers
{
    partial class SQLBaseProvider
    {
        public async Task<PageResult<ProductModel>> GetProductsAsync(PageRequest<Product> request)
        {
            var models = new List<ProductModel>();
            var page = await DataService.GetProductsAsync(request);
            foreach (var item in page.Items)
            {
                models.Add(await CreateProductModelAsync(item, includeAllFields: false));
            }
            return new PageResult<ProductModel>(page.PageIndex, page.PageSize, page.Count, models);
        }

        public async Task<ProductModel> GetProductAsync(string id)
        {
            var item = await DataService.GetProductAsync(id);
            return await CreateProductModelAsync(item, includeAllFields: true);
        }

        public async Task<int> DeleteProductAsync(ProductModel model)
        {
            return await DataService.DeleteProductAsync(model.ProductID);
        }

        public async Task<int> UpdateProductAsync(ProductModel model)
        {
            string id = model.ProductID;
            var product = !String.IsNullOrEmpty(id) ? await DataService.GetProductAsync(model.ProductID) : new Product();
            if (product != null)
            {
                UpdateProductFromModel(product, model);
                await DataService.UpdateProductAsync(product);
                model.Merge(await GetProductAsync(product.ProductID));
            }
            return 0;
        }

        private async Task<ProductModel> CreateProductModelAsync(Product source, bool includeAllFields)
        {
            var model = new ProductModel()
            {
                ProductID = source.ProductID,
                CategoryID = source.CategoryID,
                SubCategoryID = source.SubCategoryID,
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                Color = source.Color,
                Gender = source.Gender,
                ListPrice = source.ListPrice,
                DealerPrice = source.DealerPrice,
                TaxType = source.TaxType,
                Discount = source.Discount,
                DiscountStartDate = source.DiscountStartDate,
                DiscountEndDate = source.DiscountEndDate,
                StockUnits = source.StockUnits,
                SafetyStockLevel = source.SafetyStockLevel,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                CreatedOn = source.CreatedOn,
                LastModifiedOn = source.LastModifiedOn,
                Thumbnail = source.Thumbnail,
                ThumbnailBitmap = await BitmapTools.LoadBitmapAsync(source.Thumbnail)
            };

            if (includeAllFields)
            {
                model.Picture = source.Picture;
                model.PictureBitmap = await BitmapTools.LoadBitmapAsync(source.Picture);
            }
            return model;
        }

        private void UpdateProductFromModel(Product target, ProductModel source)
        {
            target.CategoryID = source.CategoryID;
            target.SubCategoryID = source.SubCategoryID;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Size = source.Size;
            target.Color = source.Color;
            target.Gender = source.Gender;
            target.ListPrice = source.ListPrice;
            target.DealerPrice = source.DealerPrice;
            target.TaxType = source.TaxType;
            target.Discount = source.Discount;
            target.DiscountStartDate = source.DiscountStartDate;
            target.DiscountEndDate = source.DiscountEndDate;
            target.StockUnits = source.StockUnits;
            target.SafetyStockLevel = source.SafetyStockLevel;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.CreatedOn = source.CreatedOn;
            target.LastModifiedOn = source.LastModifiedOn;
            target.Picture = source.Picture;
            target.Thumbnail = source.Thumbnail;
        }
    }
}