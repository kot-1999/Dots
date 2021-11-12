using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dots.services;
using dots.forms;

namespace WebApplication.APIControllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsSerciceController : ControllerBase
    {

        EntityCommentsService entityCommentsService = new();


        [Route("add")]
        [HttpPost]
        public void Add(CommentForm form)
        {
            entityCommentsService.Add(form);
        }


        [Route("get")]
        [HttpPost]
        public CommentForm Get(CommentForm form)
        {
            return (CommentForm)entityCommentsService.Get(form);
        }


        [Route("getlist")]
        [HttpGet]
        public List<CommentForm> GetList()
        {
            List<CommentForm> result = new List<CommentForm>();

            foreach (CommentForm t in entityCommentsService.GetList())
            {
                result.Add(t);
            }

            return result;
        }


        [Route("getlist/{number:int}")]
        [HttpGet]
        public List<CommentForm> GetList(int number)
        {
            List<CommentForm> result = new List<CommentForm>();

            foreach (CommentForm t in entityCommentsService.GetList(number))
            {
                result.Add(t);
            }
            return result;
        }


        [Route("update")]
        [HttpPost]
        public void Update(CommentForm form)
        {
            entityCommentsService.Update(form);
        }
    }
}
