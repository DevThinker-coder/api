using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject  queryObject)
        {
            var stocks = await _stockRepository.GetAllStocksAsync(queryObject);
            var singleStock = stocks.Select(stock => stock.ToStockDto()).ToList();
            ;
            return Ok(singleStock);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dtos.Stock.CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockModel();
            await _stockRepository.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] List<Dtos.Stock.CreateStockRequestDto> stockDtos)
        {
            var stockModels = stockDtos.Select(dto => dto.ToStockModel()).ToList();
            await _stockRepository.CreateBulk(stockModels);
            var stockDtosResult = stockModels.Select(model => model.ToStockDto()).ToList();
            return Ok(stockDtosResult);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Dtos.Stock.CreateStockRequestDto stockDto)
        {
            var stockModel = await _stockRepository.UpdateStockAsync(id, stockDto);
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stockModel = await _stockRepository.DeleteStockAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}