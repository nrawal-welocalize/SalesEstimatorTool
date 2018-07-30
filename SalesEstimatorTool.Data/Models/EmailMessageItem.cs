using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;


namespace SalesEstimatorTool.Data.Models
{
    public class EmailMessageItem
    {
        public String MessageId { get; set; }
        public String Subject { get; set; }
        public IList<Attachment> Attachmants { get; set; }
    }
}
