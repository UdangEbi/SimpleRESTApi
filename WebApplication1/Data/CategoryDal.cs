using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CategoryDal : ICategory
    {
        private List<Category>_categories = new List<Category>();

        public CategoryDal()
        {
            _categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "ASP.Net Core" },
                new Category { CategoryId = 2, CategoryName = "ASP.Net MVC" },
                new Category { CategoryId = 3, CategoryName = "ASP.Net Web API" },
                new Category { CategoryId = 4, CategoryName = "Blazor" },
                new Category { CategoryId = 5, CategoryName = "Xamarin" },
                new Category { CategoryId = 6, CategoryName = "Azure" }
            };
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }
        
        public Category GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                throw new Exception ("Category not found");
            }
            return category;
        }

        public Category AddCategory(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }       

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
            }
            return existingCategory;
        }
    }
}