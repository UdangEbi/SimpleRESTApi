using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CategoryEF : ICategory
    {
        //ef context
        private readonly ApplicationDbContext _context;
        public CategoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding category: ", ex);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category: ", ex);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            // var categories = _context.Categories.OrderByDescending(c => c.CategoryName).ToList(); //lamda expression
            var categories = from c in _context.Categories //ling
                           orderby c.CategoryName descending
                           select c;
                           
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            // var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId); //lamda expression
            var category = (from c in _context.Categories //ling
                           where c.CategoryId == categoryId
                           select c).FirstOrDefault();
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.Categories.Update(existingCategory);
                _context.SaveChanges();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category: ", ex);
            }
        }
    }
}