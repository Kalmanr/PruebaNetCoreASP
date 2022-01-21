using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] Models.Request.ActivityRequest model)
        {


            using (Models.inmobiliariaContext db = new Models.inmobiliariaContext())
            {

                var PropiertyStatus = (from d in db.CsPropierties
                                       where d.Id == model.PropiertyId && d.Status == "Inactiva"
                                       select d.Status).Count();


                var PropiertyAvaliable = (from b in db.CsActivities
                                          where b.PropiertyId == model.PropiertyId &&
                                          b.Schedule.Date == model.Schedule.Date &&
                                          b.Schedule.Hour == model.Schedule.Hour
                                          select db).Count();



                if (PropiertyStatus == 0 && PropiertyAvaliable == 0)
                {
                    Models.CsActivity oActivity = new Models.CsActivity();
                    oActivity.PropiertyId = model.PropiertyId;
                    oActivity.Schedule = model.Schedule;
                    oActivity.Title = model.Title;
                    oActivity.Status = model.Status;
                    oActivity.CreatedAt = DateTime.Now;
                    oActivity.UpdatedAt = DateTime.Now;
                    db.CsActivities.Add(oActivity);
                    db.SaveChanges();

                }
                return Ok();

            }


        }

        [HttpPut]
        public ActionResult Put([FromBody] Models.Request.ActivityReagenda model)
        {
            using (Models.inmobiliariaContext db = new Models.inmobiliariaContext())
            {

                var ActivityStatus = (from a in db.CsActivities
                                      where a.Id == model.IdActivity && a.Status == "Cancelada"
                                      select a).Count();

                if (ActivityStatus == 0)
                {
                    Models.CsActivity oActivity = db.CsActivities.Find(model.IdActivity);
                    oActivity.Id = model.IdActivity;
                    oActivity.Schedule = model.Schedule;
                    oActivity.UpdatedAt = DateTime.Now;
                    db.Entry(oActivity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest(); 
                }

            }


        }

        [HttpDelete]

        public ActionResult Delete([FromBody] Models.Request.ActivityCancela model)
        {
            using (Models.inmobiliariaContext db = new Models.inmobiliariaContext())
            {
                if (model.Status == "Cancelada")
                {
                    Models.CsActivity oActivity = db.CsActivities.Find(model.IdActivity);
                    oActivity.Id = model.IdActivity;
                    oActivity.Status = model.Status;
                    oActivity.UpdatedAt = DateTime.Now;
                    db.Entry(oActivity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }

        [HttpGet]
        public ActionResult Get([FromBody] Models.Request.GetActivity model)
        {
            using (Models.inmobiliariaContext db = new Models.inmobiliariaContext())
            {
                var GetActivityFiltred = (from n in db.CsActivities
                                          join m in db.CsPropierties on n.PropiertyId equals m.Id
                                          where n.Schedule >= model.FechaInicio
                                          && n.Schedule <= model.FechaFin
                                          && n.Status == model.Status
                                          select new Models.Request.GetActivitiesAll { Id = n.Id,
                                              Schedule = n.Schedule,
                                              Title = n.Title,
                                              CreatedAt = n.CreatedAt,
                                              Status = n.Status,
                                              Condition = (n.Status == "Activa" && n.Schedule >= DateTime.Now) ? "Pendiente a Realizar" : (n.Status == "Activa" && n.Schedule <= DateTime.Now) ? "Atrasada" : (n.Status == "Realizada") ? "Finalizada" : "Cancelada",
                                              IdPropierty = n.PropiertyId,
                                              TitlePropierty = m.Title,
                                              AddressPropierty = m.Address,
                                              Survey = "www.survey.com" }).ToList();
                return Ok(GetActivityFiltred);
            }
        }

        [HttpGet]
        [Route("GetAllActivities")]
        public ActionResult Get()
        {
            using (Models.inmobiliariaContext db = new Models.inmobiliariaContext())
            {
                var GetActivity = (from n in db.CsActivities
                                   join m in db.CsPropierties on n.PropiertyId equals m.Id
                                   where n.Schedule >= DateTime.Now.AddDays(-3)
                                   && n.Schedule <= DateTime.Now.AddDays(14)
                                   select new Models.Request.GetActivitiesAll
                                   {
                                       Id = n.Id,
                                       Schedule = n.Schedule,
                                       Title = n.Title,
                                       CreatedAt = n.CreatedAt,
                                       Status = n.Status,
                                       Condition = (n.Status == "Activa" && n.Schedule >= DateTime.Now) ? "Pendiente a Realizar" : (n.Status == "Activa" && n.Schedule <= DateTime.Now) ? "Atrasada" : (n.Status == "Realizada") ? "Finalizada" : "Cancelada",
                                       IdPropierty = n.PropiertyId,
                                       TitlePropierty = m.Title,
                                       AddressPropierty = m.Address,
                                       Survey = "www.survey.com"
                                   }).ToList();
                return Ok(GetActivity);
            }
        }

        

    }
}
