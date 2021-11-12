using dots.forms;
using System;
using System.Collections.Generic;
using System.Linq;


namespace dots.services
{
    public class EntityCommentsService 
    {
        public void Add(Form form)
        {
            using (var context = new CommentsDbContext())
            {
                
                context.Comments.Add((CommentForm)form);
                context.SaveChanges();
            }
        }

        public CommentForm Get(Form form)
        {
            CommentForm f =(CommentForm) form;
            using (var context = new CommentsDbContext())
            {
                return context.Comments.Single(a=>f.Id==a.Id);
            }
        
        }

        public List<CommentForm> GetList()
        {
            List<CommentForm> result=new List<CommentForm>();
            using (var context = new CommentsDbContext())
            {
                foreach(var t in (from s in context.Comments select s).ToList())
                {
                    result.Add(t);
                } 
            }
            return result;
        }
 


        public List<CommentForm> GetList(int number)
        {
            List<CommentForm> result = new List<CommentForm>();
            using (var context = new CommentsDbContext())
            {
                foreach (var t in (from s in context.Comments select s).Take(number).ToList())
                {
                    result.Add(t);
                }
            }
            return result;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Update(Form form)
        {
            CommentForm f = (CommentForm)form;
            using (var context = new CommentsDbContext())
            {
         
                context.Comments.Update(f);
                context.SaveChanges();
            }
        }
    }
}
