using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StarMeApp.Application.Contracts.DTOs.Common
{
    public interface IDTO
    {
    }
    public interface IDTO<TIdTDto> : IDTO
    {
        [Required]
        [DataMember]
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
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public DateTime? UpdatedAt { get; set; }
        [DataMember]
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

        public ResponseDTO(ResponseMessage msg) : this()
        {
            this.Messages.Add(msg);
        }
        public bool Succeeded
        {
            get
            {
                return !this.Messages.Any(x => x.GetMessageKind() == (MessageKind.Error | MessageKind.Fatal));
            }
        }
        public ICollection<ResponseMessage> Messages { get; set; }

        public void AddMessage(MessageKind kind, string message)
        {
            this.Messages.Add(new ResponseMessage(message, kind));
        }

        public void ClearMessages()
        {
            this.Messages.Clear();
        }
    }

    public class ResponseValueDTO<TValue> : ResponseDTO
    {
        public ResponseValueDTO() : base()
        {
        }
        [DataMember]
        public TValue Response { get; set; }
    }

    public class ResponseListDTO<TValue> : ResponseDTO
    {
        public ResponseListDTO() : base()
        { }
        [DataMember]
        public IEnumerable<TValue> Response { get; set; }
        [DataMember]
        public int TotalListCount { get; set; }
    }

    public class PagedResponseDTO<IDTO> : ResponseListDTO<IDTO>
    {
        [DataMember]
        public int PageNumber { get; set; }
        [DataMember]
        public int PageSize { get; set; }

        public PagedResponseDTO(int pageNumber, int pageSize) : base()
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
