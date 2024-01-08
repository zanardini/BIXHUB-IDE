using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BixHubWrapper.Model
{
    public class CreateSessionResponse
    {
        public Guid? SessionGuid { get; set; }

        public string SessionUrl { get; set; }
    }
}