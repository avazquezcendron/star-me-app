using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
        public ResponseMessage()
        {
            this.TimeStamp = DateTime.UtcNow;
        }

        public ResponseMessage(string message) : this()
        {
            this.Message = message;
        }

        public ResponseMessage(string message, MessageKind kind) : this(message)
        {
            this.Message = message;
            this._kind = kind;
        }

        private MessageKind _kind;
        [DataMember]
        public string Kind
        {
            get
            {
                return this.GetMessageDescription();
            }
        }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }

        public MessageKind GetMessageKind()
        {
            return this._kind;
        }
        
        private string GetMessageDescription()
        {
            var result = "(" + this._kind.ToString() + ") ";
            switch (this._kind)
            {
                case MessageKind.Fatal:
                    return result + "Fatal";
                case MessageKind.Error:
                    return result + "Error";
                case MessageKind.Info:
                    return result + "Info";
                case MessageKind.Warning:
                    return result + "Warning";
                case MessageKind.Debug:
                    return result + "Debug";
                default:
                    return result + "Other";
            }
        }

    }
}
