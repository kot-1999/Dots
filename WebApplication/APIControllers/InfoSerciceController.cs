using dots.forms;
using dots.services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication.APIControllers
{
    [Route("api/info")]
    [ApiController]
    public class InfoSerciceController :  ControllerBase
    {
        EntityInfoService entityInfoService = new();


        [Route("add")]
        [HttpPost]
        public void Add(InfoForm form)
        {
            try
            {
                entityInfoService.Autorize(form);
            }catch(PlayerAutorizationExeption e)
            {
                throw new PlayerAutorizationExeption();
            }
            
        }


        [Route("get")]
        [HttpPost]
        public InfoForm Get(InfoForm form)
        {
            return (InfoForm) entityInfoService.Get(form);
        }


        [Route("getlist")]
        [HttpGet]
        public List<InfoForm> GetList()
        {
            List<InfoForm> result = new List<InfoForm>();
            
            foreach (InfoForm t in entityInfoService.GetList())
            {
                result.Add(t);
            }
            
            return result;
        }


        [Route("getlist/{number:int}")]
        [HttpGet]
        public List<InfoForm> GetList(int number)
        {
            List<InfoForm> result = new List<InfoForm>();

            foreach (InfoForm t in entityInfoService.GetList(number))
            {
                result.Add(t);
            }
            return result;
        }


        [Route("update")]
        [HttpPost]
        public void Update(InfoForm form)
        {
            entityInfoService.UpdateScore(form);
        }

      
    }
}
