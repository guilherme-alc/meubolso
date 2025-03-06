﻿using MeuBolso.Api.Data;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace MeuBolso.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            var query = context
                .Categories
                .AsNoTracking()
                .OrderBy(c => c.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query
                .CountAsync();

            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
        }

        public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                    return new Response<Category>(null, 404, "Categoria não encontrada.");

                return new Response<Category>(category, 200, "Categoria encontrada com sucesso!");
            }
            catch
            {
                return new Response<Category>(null, 500, "Não foi possível buscar a categoria.");
            }
        }
        public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category>(category, 201, "Categoria criada com sucesso!");
            } catch
            {
                return new Response<Category>(null, 500, "Não foi possível criar a categoria.");
            }
        }
        public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                    return new Response<Category>(null, 404, "Categoria não encontrada.");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category>(category, message: "Categoria atualizada com sucesso!");
            }
            catch
            {
                return new Response<Category>(null, 500, "Não foi possível alterar a categoria.");
            }
        }
        public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                    return new Response<Category>(null, 404, "Categoria não encontrada.");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category>(category, message: "Categoria excluida com sucesso!");
            }
            catch
            {
                return new Response<Category>(null, 500, "Não foi possível excluir a categoria.");
            }
        }

    }
}
