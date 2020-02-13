using application.interfaces;
using application.interfaces.paging;
using AutoMapper;
using lib.common.interfaces;
using System;
using System.Text;

namespace application.cqrs._base
{
    public class CommandResponseBase<TIdentity> : ICommandResponse<TIdentity> where TIdentity : struct, IEquatable<TIdentity>, IComparable<TIdentity>
    {
        public CommandResponseBase(string entity, TIdentity id)
        {
            Entity = entity;
            ID = id;
        }

        public TIdentity ID { get; }

        public string Entity { get; }


    }

    public abstract class RequestHandlerBase
    {
        protected readonly IApplicationDbContext dbContext;
        protected readonly IMapper mapper;

        protected RequestHandlerBase(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        protected static void Blame<T>(T entity, string createdBy, string modifiedBy = "") where T : ITakeCredit
        {
            entity.CreatedBy = createdBy;

            if (string.IsNullOrEmpty(modifiedBy))
            {
                entity.ModifiedBy = modifiedBy;
            }
            else
            {
                entity.ModifiedBy = createdBy;
            }

        }
    }
}
