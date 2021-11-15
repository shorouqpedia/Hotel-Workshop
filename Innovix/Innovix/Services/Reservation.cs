using Innovix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Services
{
    public class Reservation : IReservationOp
    {
        InnovixDBContext _db;
        public Reservation(InnovixDBContext db)
        {
            _db = db;
        }
        public decimal GetReservationTotal
            (DateTime ChckIn_Date, DateTime ChckOut_Date, int AdultsNo, int ChildrenNo, int RoomTypeId, int MealPlanId)
        {
            var Orig_CheckIn = ChckIn_Date;
            var Orig_CheckOut = ChckOut_Date;

            decimal TotalRatePerRoom = 0;
            int RoomsNo = 0;

            var AllSeasonId_InType = _db.SeasonTypes.Where(a => a.typeId == RoomTypeId).Select(a => a.SeasonId).ToList();
            var season = _db.Seasons.Where(a => AllSeasonId_InType.Contains(a.Id) && a.FromDate.CompareTo(ChckIn_Date) <= 0).ToList().Last();
            bool inSameSeason;
            int days;
            do
            {
                inSameSeason = ChckOut_Date.CompareTo(season.ToDate) <= 0;
                if (inSameSeason)
                {
                    days = (int)(ChckOut_Date - ChckIn_Date).TotalDays + 1;
                    TotalRatePerRoom += days * _db.SeasonTypes.FirstOrDefault(a => a.SeasonId == season.Id && a.typeId == RoomTypeId).Rate;
                }
                else //not in same season
                {
                    days = (int)(season.ToDate - ChckIn_Date).TotalDays + 1;
                    TotalRatePerRoom += days * _db.SeasonTypes.FirstOrDefault(a => a.SeasonId == season.Id && a.typeId == RoomTypeId).Rate;
                    ChckIn_Date = season.ToDate.AddDays(1);
                    season = _db.Seasons.FirstOrDefault(a => AllSeasonId_InType.Contains(a.Id) && a.FromDate.CompareTo(ChckIn_Date) == 0);
                }
            } while (!inSameSeason);

            RoomsNo = (int) Math.Ceiling((double)AdultsNo / 2);
            if (ChildrenNo > (2 * RoomsNo))
            {
                int RemChildren = ChildrenNo - (2 * RoomsNo);
                RoomsNo += (int)Math.Ceiling((double)RemChildren / 2);
            }

            decimal MealsRate = 0;
            MealPlan MealPlan = _db.MealPlans.FirstOrDefault(a => a.Id == MealPlanId);

            if (Orig_CheckIn.Month>=1&&Orig_CheckIn.Month<=5) // check-in date is in low Season
            {
                if (Orig_CheckOut.Month >= 1 && Orig_CheckIn.Month <= 5) //check-out also is in low season
                {
                    days = (int)(Orig_CheckOut - Orig_CheckIn).TotalDays+1;
                    MealsRate = MealPlan.LowSeasonRate * days;
                }
                else //check-out is in high Season
                {
                    days = (int)(new DateTime(Orig_CheckIn.Year, 5, 30) - Orig_CheckIn).TotalDays+1; //count days in low season
                    MealsRate = MealPlan.LowSeasonRate * days;
                    days = (int)(Orig_CheckOut- new DateTime(Orig_CheckOut.Year, 6, 1)).TotalDays+1; //count days in high season
                    MealsRate += MealPlan.HighSeasonRate * days;
                }
            }
            else // check-in date is in high Season
            {
                if (Orig_CheckOut.Month >= 1 && Orig_CheckIn.Month <= 5) //check-out is in low season
                {
                    days = (int)(new DateTime(Orig_CheckIn.Year, 12, DateTime.DaysInMonth(Orig_CheckIn.Year, 12))-Orig_CheckIn).TotalDays+1; //count days in high season
                    MealsRate = MealPlan.HighSeasonRate * days;
                    days = (int)(Orig_CheckOut - new DateTime(Orig_CheckOut.Year, 1, 1)).TotalDays + 1; //count days in low season
                    MealsRate += MealPlan.LowSeasonRate * days;
                }
                else //check-out also is in high Season
                {
                    days = (int)((Orig_CheckOut - Orig_CheckIn).TotalDays + 1);
                    MealsRate = MealPlan.HighSeasonRate * days;
                }
            }

            return (TotalRatePerRoom * RoomsNo) + (MealsRate * (AdultsNo + ChildrenNo)) ;
        }
    }
}
