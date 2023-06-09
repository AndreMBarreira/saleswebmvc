﻿using Microsoft.EntityFrameworkCore;
using SaleWebMvc.Data;
using SaleWebMvc.Models;
using SaleWebMvc.Services.Exceptions;

namespace SaleWebMvc.Services
{
    public class SellerService
    {
        private readonly SaleWebMvcContext _context;

        public SellerService(SaleWebMvcContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Seller>> Find()
        {
            return from s in _context.Seller select s;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Seller obj) 
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) 
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
