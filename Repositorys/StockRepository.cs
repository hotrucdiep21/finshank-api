using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            Console.WriteLine($"SortBy: {queryObject.SortBy}, IsDescending: {queryObject.IsDescending}"); 
            var stocks = _context.Stock.Include(stk => stk.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol): stocks.OrderBy(s => s.Symbol); 
                }
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
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
            return await _context.Stock.AnyAsync(s => s.Id == id);
        }
    }
}