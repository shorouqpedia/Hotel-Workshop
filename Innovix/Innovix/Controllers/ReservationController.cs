using Innovix.Models;
using Innovix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationOp _res;
        private readonly InnovixDBContext _db;

        public ReservationController(IReservationOp res, InnovixDBContext db)
        {
            _res = res;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ReservationViewModel model = new ReservationViewModel();
            model.CheckIn = model.CheckOut = DateTime.Now;
            FillDropDownList(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ReservationViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                if (model.CheckOut < model.CheckIn)
                {
                    ViewBag.Error = "Check-out date must be after check-in date .";
                    FillDropDownList(model);
                    return View(model);
                }

                decimal total;
                try
                {
                    total = _res.GetReservationTotal(model.CheckIn, model.CheckOut, model.Adults, model.Children, int.Parse(model.RoomTypeId), int.Parse(model.MealPlanId));
                }
                catch
                {
                    ViewBag.Error = "Something went Wrong";
                    FillDropDownList(model);
                    return View(model);
                }
                ViewBag.Success = "The Total Rate Is " + total.ToString();
                model = new ReservationViewModel();
                model.CheckIn = model.CheckOut = DateTime.Now;
                FillDropDownList(model);
                return View(model);
            }
            FillDropDownList(model);
            return View(model);
        }

        public void FillDropDownList(ReservationViewModel model)
        {
            var dropList = (from types in _db.RoomTypes
                            select new SelectListItem()
                            {
                                Text = types.Name,
                                Value = types.Id.ToString()
                            }).ToList();
            dropList.Insert(0, new SelectListItem()
            {
                Text = "Please select room type",
                Value = string.Empty
            });

            model.RoomType = dropList;

            dropList = (from meals in _db.MealPlans
                        select new SelectListItem()
                        {
                            Text = meals.Name,
                            Value = meals.Id.ToString()
                        }).ToList();
            dropList.Insert(0, new SelectListItem()
            {
                Text = "Please select meal plan",
                Value = string.Empty
            });

            model.MealPlan = dropList;
        }

    }

}
