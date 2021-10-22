﻿using System;
using Producks.Undercutters;
using Producks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Producks.Repo.ViewModels;

namespace Producks.Repo
{
    public class RepoApi
    {
        private readonly StoreDb _context;
        private List<Producks.Undercutters.Models.Category> UndercuttersCategories;
        private List<Producks.Undercutters.Models.Brand> UndercuttersBrands;
        private List<Producks.Undercutters.Models.Product> UndercuttersProducts;

        public RepoApi(StoreDb context)
        {
            _context = context;
            UndercuttersCategories = UndercuttersAPI.GetCategories().Result;
            UndercuttersBrands = UndercuttersAPI.GetBrands().Result;
            UndercuttersProducts = UndercuttersAPI.GetProducts().Result;
        }

        public IEnumerable<string> GetCategoriesName() {
            var catName = (_context.Categories.AsEnumerable().Where(c => c.Active).Select(c => c.Name)).Union(UndercuttersCategories.AsEnumerable().Select(c => c.Name)).Distinct();
            return catName;        
        }

        public IEnumerable<string> GetBrandsName()
        {
            var catName = (_context.Brands.AsEnumerable().Where(c => c.Active).Select(c => c.Name)).Union(UndercuttersBrands.AsEnumerable().Select(c => c.Name)).Distinct();
            return catName;
        }
        public IEnumerable<ProductRepoVM> GetAllProducts()
        {
            return( _context.Products.AsEnumerable().Where(p => p.Active)
                    .Select(x => new ProductRepoVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        StockLevel = x.StockLevel,
                        Brand = x.Brand.Name,
                        Category = x.Category.Name,
                        Undercutters = false
                    })
                    .Union(UndercuttersProducts.AsEnumerable().Select(x => new ProductRepoVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        inStock = x.InStock,
                        Brand = x.BrandName,
                        Category = x.CategoryName,
                        Undercutters = true
                    })));

            
        }
    }
}
