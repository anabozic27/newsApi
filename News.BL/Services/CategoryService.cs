using AutoMapper;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Models.Response;
using News.DAL.Interfaces;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.BL.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();

            var categoryList = new List<CategoryViewModel>();

            categoryList = _mapper.Map<List<CategoryViewModel>>(categories);

            return categoryList;
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);

            if (category == null)
                throw new ValidationException("Category not found");

            var categoryModel = _mapper.Map<CategoryViewModel>(category);

            return categoryModel;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetByParentCategoryID(int parentCategoryId)
        {
            var categories = await _unitOfWork.CategoryRepository.GetByParentCategoryID(parentCategoryId);
            
            var categoryList = _mapper.Map<List<CategoryViewModel>>(categories);

            return categoryList;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetUserCategoriesForArticleId(int categoryId)
        {
            var categories = await _unitOfWork.CategoryRepository.GetUserCategoriesForArticleId(categoryId);
            
            var categoryList = _mapper.Map<List<CategoryViewModel>>(categories);

            return categoryList;
        }

        public async Task<int> Save(CategoryModel model)
        {
            return await _unitOfWork.CategoryRepository.Save(_mapper.Map<Category>(model));
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);

            if (category == null)
                throw new ValidationException("Category not found");

            return await _unitOfWork.CategoryRepository.Delete(id);
        }

    }
}
