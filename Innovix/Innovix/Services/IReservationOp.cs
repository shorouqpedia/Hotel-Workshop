using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Services
{
    public interface IReservationOp
    {
        public decimal GetReservationTotal(DateTime ChckIn_Date, DateTime ChckOut_Date,int AdultsNo , int ChildrenNo, int RoomTypeId , int MealPlanId);
    }
}
