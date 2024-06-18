using Application.DTOs.Responses.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authen.Commands.Login
{
    public record LoginCommand(string UserName,string Password):IRequest<AuthencationResponse>;
}
