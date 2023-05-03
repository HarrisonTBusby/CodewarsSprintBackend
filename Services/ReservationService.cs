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
        public bool CheckIfKataAlreadyReserved(string KataId, string KataLang)
        {
            return _context.ReservationInfo.SingleOrDefault(res => res.KataId == KataId && res.KataLang == KataLang && res.isCompleted == false && res.isDeleted == false) != null;
            //  Checks if the Kata/Language combo is already in database AND that is currently is progress (doesn't count deleted/completed)
        }
        public bool CheckIfKataAssignedToUser(int UserId)
        {
            return _context.ReservationInfo.SingleOrDefault(res => res.UserId == UserId && res.isCompleted == false && res.isDeleted == false) != null;
            // Checks if user is already assigned a kata AND that is currently is progress (doesn't count deleted/completed)
        }
        public bool AddReservation (ReservationModel newRes)
        {
            bool result = false;
            if (!CheckIfKataAlreadyReserved(newRes.KataId, newRes.KataLang) && !CheckIfKataAssignedToUser(newRes.UserId)) {
                _context.Add(newRes);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool UpdateReservation(ReservationModel ResUpdate)
        {
            _context.Update<ReservationModel>(ResUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool MarkDeletedByResId(int Id)
        {
            bool result = false;
            var ResDelete = _context.ReservationInfo.SingleOrDefault(item => item.Id == Id);
            if (ResDelete != null) {
                ResDelete.isDeleted = true;
                _context.Update<ReservationModel>(ResDelete);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool MarkCompletedByResId(int Id)
        {
            bool result = false;
            var ResComplete = _context.ReservationInfo.SingleOrDefault(item => item.Id == Id);
            if (ResComplete != null) {
                ResComplete.isCompleted = true;
                _context.Update<ReservationModel>(ResComplete);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool DANGERHardDeleteByResId(int Id)
        {
            bool result = false;
            var ResDelete = _context.ReservationInfo.SingleOrDefault(item => item.Id == Id);
            if (ResDelete != null) {
                _context.Remove<ReservationModel>(ResDelete);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public IEnumerable<ReservationModel> GetAllReservations()
        {
            return _context.ReservationInfo;
        }

        public IEnumerable<ReservationModel> GetAllActice()
        {
            return _context.ReservationInfo.Where(item => item.isCompleted == false && item.isDeleted == false);
        }

        public ReservationModel GetActiveByUser(int UserId)
        {
            return _context.ReservationInfo.SingleOrDefault(item => item.UserId == UserId && item.isCompleted == false && item.isDeleted == false);
        }

        public IEnumerable<ReservationModel> GetAllByUser(int UserId)
        {
            return _context.ReservationInfo.Where(item => item.UserId == UserId);
        }
        public IEnumerable<ReservationModel> GetAllCompletedByUser(int UserId)
        {
            return _context.ReservationInfo.Where(item => item.UserId == UserId && item.isCompleted == true);
        }

        
    }

}