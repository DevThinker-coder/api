using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject  queryObject);
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stockModel);
        Task<List<Stock>> CreateBulk(List<Stock> stockModel);
        Task<Stock> UpdateStockAsync(int id, CreateStockRequestDto stockDto);
        Task<Stock?> DeleteStockAsync(int id);
        Task<bool> StockExistsAsync(int id);
    }
}