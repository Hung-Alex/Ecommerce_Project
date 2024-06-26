using Application.Common.Interface;
using Domain.Entities.Slides;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Slides.Commands.CreateSlide
{
    public class CreateSlideCommandHandler : IRequestHandler<CreateSlideCommand, Result<bool>>
    {
        internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommandValidator>
        {

        }
        private readonly IUnitOfWork _unitOfWork;

        public CreateSlideCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            repoSlide.Add(new Slide(request.Title, request.Description, request.Status, request.Order));
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
