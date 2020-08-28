using System;
using System.Collections.Generic;
using System.Linq;

namespace StarMeApp.Application.Contracts.DTOs.Common
{
    public interface IDTO
    {
    }
    public interface IDTO<TIdTDto> : IDTO
    {
        TIdTDto Id { get; set; }
    }

    public interface IGetDTO<TIdTDto> : IDTO<TIdTDto>
    {
    }

    public interface IAddDTO<TIdTDto> : IDTO<TIdTDto>
    {
    }

    public class AuditInfoStructDTO
    {

        public AuditInfoStructDTO() { }

        public AuditInfoStructDTO(string user, string state, DateTime createdAt, DateTime? updatedAt)
        {
            this.User = user;
            this.State = state;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
        }
        public DateTime CreatedAt { get; set; }
        public string State { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string User { get; set; }
    }

    public interface IAuditableDTO
    {
        AuditInfoStructDTO AuditInfo { get; set; }
    }

    public class ResponseDTO
    {
        public ResponseDTO()
        {
            this.Messages = new List<ResponseMessage>();
        }
        public bool Succeeded
        {
            get
            {
                return !this.Messages.Any(x => x.Kind == (MessageKind.Error | MessageKind.Fatal));
            }
        }
        public IEnumerable<ResponseMessage> Messages { get; set; }
    }

    public class ResponseValueDTO<TValue> : ResponseDTO
    {
        public ResponseValueDTO() : base()
        {
        }
        public TValue Response { get; set; }
    }

    public class ResponseListDTO<TValue> : ResponseDTO
    {
        public ResponseListDTO() : base()
        { }

        public IEnumerable<TValue> Response { get; set; }
        public int TotalListCount { get; set; }
    }

    public class PagedResponseDTO<IDTO> : ResponseListDTO<IDTO>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResponseDTO(int pageNumber, int pageSize) : base()
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
