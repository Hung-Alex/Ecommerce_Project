using Application.Common.Interface;
using AutoMapper;
using MediatR;
using FluentValidation;
using Domain.Constants;
using Domain.Shared;
using Application.DTOs.Responses.Rattings;
using Domain.Entities.Rattings;

namespace Application.Features.Rattings.Commands.UpdateRatting
{
    public class UpdateRattingCommandHandler : IRequestHandler<UpdateRattingCommand, Result<RattingDTO>>
    {
        internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateRattingCommand>
        {
            public UpdateCategoryCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRattingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<RattingDTO>> Handle(UpdateRattingCommand request, CancellationToken cancellationToken)
        {
            var repoRatting = _unitOfWork.GetRepository<Ratting>();
            var ratting = await repoRatting.GetByIdAsync(request.Id);
            if (ratting == null) return Result<RattingDTO>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(request.Id));
            ratting.Rate = request.Rate;
            ratting.ProductId = request.ProductId;
            ratting.Description = request.Description;
            await _unitOfWork.Commit();
            var rattingDTO = _mapper.Map<RattingDTO>(ratting);
            return Result<RattingDTO>.ResultSuccess(rattingDTO);
        }
    }
}

