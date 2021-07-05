using System;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AdminService.Models;
using AdminService.Services;
using System.Threading.Tasks;

namespace AdminService.Controllers {

    [Route("api/admin")]
    [ApiController]
    public class KafkaProducerController : ControllerBase {
       
        private readonly IAdminTopicManagement _adminTopicManagement;
        

        public KafkaProducerController(IAdminTopicManagement adminTopicManagement) {

            _adminTopicManagement = adminTopicManagement;
        }

 
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] User user) {
            string parsedUser = JsonConvert.SerializeObject(user);
            var response = await _adminTopicManagement.SendToKafka("AddUser", parsedUser);
            return Created(string.Empty, response);
        }
    }
}