using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Constants.Queries.GetConstantByEnum;

public sealed record GetConstantByEnumQuery(ConstantsEnum Constant) : IRequest<Constant>;