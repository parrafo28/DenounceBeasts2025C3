

using AutoMapper;
using DenounceBeasts.Infrasctructure;

namespace DenounceBeasts.Application.Core
{
    public class BaseService
    {
        protected readonly UnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        public BaseService(UnitOfWork unitOfWork, IMapper mapper)
        {

            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }


    }
}
