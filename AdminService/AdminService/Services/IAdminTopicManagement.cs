using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Services {
    public interface IAdminTopicManagement {

        public Task<object> SendToKafka(string message);
    }
}
