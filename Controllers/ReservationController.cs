using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodewarsSprintBackend.Models;
using CodewarsSprintBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodewarsSprintBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _data;
        public ReservationController(ReservationService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddReservation")]

        public bool AddReservation(ReservationModel newRes)
        {
            return _data.AddReservation(newRes);
        }

        [HttpGet]
        [Route("GetAllReservations")]

        public IEnumerable<ReservationModel> GetAllReservations()
        {
            return _data.GetAllReservations();
        }
    }
}