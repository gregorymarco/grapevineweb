using System;
namespace website.Models{

    public partial class MessageSubmissionModel {
        public string messageContent { get; set; }
        public bool? submissionSuccess {get;set;}
    }
}
