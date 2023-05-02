using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodewarsSprintBackend.Models;
using CodewarsSprintBackend.Services.Context;
using System.Linq;

namespace CodewarsSprintBackend.Services
{
    public class ReservationService
    {
        private readonly DataContext _context;
        public ReservationService(DataContext context)
        {
            _context = context;
        }
        public bool AddReservation (ReservationModel newRes)
        {
            _context.Add(newRes);
            return _context.SaveChanges() != 0;
        }
        public IEnumerable<ReservationModel> GetAllReservations()
        {
            return _context.ReservationInfo;
        }
    }

}