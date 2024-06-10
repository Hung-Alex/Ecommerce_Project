using Domain.Entities.Brands;
using Domain.Entities.Products;
using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class HomeController:ControllerBase
    {


        private readonly IUnitOfWork<StoreDbContext> _unitOfWork;

        public HomeController(IUnitOfWork<StoreDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Get()
        {
            // Sử dụng UnitOfWork và IRepository ở đây
            try
            {
                
                var repository = _unitOfWork.GetRepository<Brand>();
                 repository.Add(new Brand() {Name="Apple" ,Description="đá",LogoImageUrl="asdas",UrlSlug="dasd"});
                await _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                return StatusCode(500, ex.Message);
            }
        }
    }
}
