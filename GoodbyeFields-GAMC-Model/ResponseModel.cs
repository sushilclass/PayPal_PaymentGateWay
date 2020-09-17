using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_Model
{
   public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public object Data { get; set; }
    }
}
