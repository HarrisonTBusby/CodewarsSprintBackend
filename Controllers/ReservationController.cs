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

        [HttpPost]
        [Route("UpdateReservation")]

        public bool UpdateReservation(ReservationModel ResItem)
        {
            return _data.UpdateReservation(ResItem);
        }

        [HttpPost]
        [Route("MarkDeletedByResId/{ResId}")]
        public bool MarkDeletedByResId(int ResId)
        {
            return _data.MarkDeletedByResId(ResId);
        }

        [HttpPost]
        [Route("MarkCompletedByResId/{ResId}")]
        public bool MarkCompletedByResId(int ResId)
        {
            return _data.MarkCompletedByResId(ResId);
        }

        [HttpGet]
        [Route("GetAllReservations")]

        public IEnumerable<ReservationModel> GetAllReservations()
        {
            return _data.GetAllReservations();
        }

        [HttpGet]
        [Route("GetAllActive")]

        public IEnumerable<ReservationModel> GetAllActive()
        {
            return _data.GetAllActice();
        }

        [HttpGet]
        [Route("GetActiveByUser/{UserId}")]
        public ReservationModel GetActiveByUser(int UserId)
        {
            return _data.GetActiveByUser(UserId);
        }

        [HttpGet]
        [Route("GetAllByUser/{UserId}")]

        public IEnumerable<ReservationModel> GetAllByUser(int UserId)
        {
            return _data.GetAllByUser(UserId);
        }

        [HttpGet]
        [Route("GetAllCompletedByUser/{UserId}")]

        public IEnumerable<ReservationModel> GetAllCompletedByUser(int UserId)
        {
            return _data.GetAllCompletedByUser(UserId);
        }

        [HttpPost]
        [Route("DANGERHardDeleteByResId/{ResId}")]
        public bool DANGERHardDeleteByResId(int ResId)
        {
            return _data.DANGERHardDeleteByResId(ResId);
        }
        
    }
}