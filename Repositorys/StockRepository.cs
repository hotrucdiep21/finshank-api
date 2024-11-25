using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositorys
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stock.Include(stk => stk.Comments).ToListAsync();
        }
        
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = updateDto.Symbol;    
            existingStock.CompanyName = updateDto.CompanyName;
            existingStock.Purchase = updateDto.Purchase;
            existingStock.LastDev = updateDto.LastDev;
            existingStock.Industry = updateDto.Industry;
            existingStock.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var existingStock = await _context.Stock.Include(stk => stk.Comments).FirstOrDefaultAsync(stk => stk.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            return existingStock;
        }

        public async Task<bool> StockExist(int id)
        {
            var isExist = await _context.Stock.FirstOrDefaultAsync(stk => stk.Id == id);
            if (isExist == null)
            {
                return false;
            }
            return true;
        }
    }
}