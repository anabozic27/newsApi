using AutoMapper;
using FluentValidation;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Models.Response;
using News.DAL.Interfaces;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.BL.Services
{
    public class ArticleService : IArticleService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetAll()
        {
            var articles = await _unitOfWork.ArticleRepository.GetAll();
            
            var articleList = _mapper.Map<List<ArticleViewModel>>(articles);

            return articleList;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetByCategoryId(int categoryId)
        {
            var articles = await _unitOfWork.ArticleRepository.GetByCategoryId(categoryId);
            
            var articleList = _mapper.Map<List<ArticleViewModel>>(articles);

            return articleList;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetByUserId(int userId)
        {
            var articles = await _unitOfWork.ArticleRepository.GetByUserId(userId);
            
            var articleList = _mapper.Map<List<ArticleViewModel>>(articles);

            return articleList;
        }

        public async Task<ArticleViewModel> GetById(int id)
        {
            var article = await _unitOfWork.ArticleRepository.GetById(id);

            if (article == null)
                throw new ValidationException("Article not found");

            var articleModel = _mapper.Map<ArticleViewModel>(article);

            return articleModel;
        }

        public async Task<int> Save(ArticleModel model)
        {
            return await _unitOfWork.ArticleRepository.Save(_mapper.Map<Article>(model));
        }

        public async Task<bool> Delete(int id)
        {
            var article = await _unitOfWork.ArticleRepository.GetById(id);

            if (article == null)
                throw new ValidationException("Article not found");

            return await _unitOfWork.ArticleRepository.Delete(id);
        }
    }
}
