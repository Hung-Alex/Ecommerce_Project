using Application.Common.Exceptions;
using Application.Common.Interface;
using Domain.Entities.Brands;
using MediatR;

namespace Application.CQRS.Brands.Commands.DeleteBrand
{
    public class DeleteCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var repoBrand = _unitOfWork.GetRepository<Brand>();
            var brand = await repoBrand.GetByIdAsync(request.Id);
            if (brand == null) { throw new NotFoundException($"Brand with ID {request.Id} not found."); }
            repoBrand.Delete(brand);
            await _unitOfWork.Commit();
        }
    }
}
