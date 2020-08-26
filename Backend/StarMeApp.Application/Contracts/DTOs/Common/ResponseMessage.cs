using System;
using System.Collections.Generic;
using System.Text;

namespace StarMeApp.Application.Contracts.DTOs.Common
{
    public enum MessageKind
    {
        Fatal = 0,
        Error = 1,
        Info = 2,
        Warning = 3,
        Debug = 4,
        Other = 5,
    }
    public class ResponseMessage
    {
        public ResponseMessage() {
            this.TimeStamp = new DateTime();
        }

        public ResponseMessage(string message) : this()
        {
            this.Message = message;
        }

        public ResponseMessage(string message, MessageKind kind): this(message)
        {
            this.Message = message;
            this.Kind = kind;
        }

        public MessageKind Kind { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
