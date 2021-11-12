using dots.forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;




namespace dots.services
{

    public class PlayerAutorizationExeption : Exception
    {

    }

    public class EntityInfoService 
    {
        public void Autorize(InfoForm form)
        {
            using (var context = new InfoDbContext())
            {
                try
                {
                    context.InfoForms.Add((InfoForm)form);
                    context.SaveChanges();
                }
                catch(DbUpdateException ee)
                {
                    try
                    {
                        InfoForm f1=Get(form);
                     
                        if (!f1.Email.Equals(form.Email))
                        {
                           throw new PlayerAutorizationExeption();
                        }
                       
                    }catch(Exception e)
                    {
                       throw new PlayerAutorizationExeption();
                    }
                    
                }
            }

        }

        public InfoForm Get(Form form)
        {
            InfoForm f = (InfoForm)form;
            using (var context = new InfoDbContext())
            {
                return context.InfoForms.Single(a => f.Player == a.Player);
            }
        }

        public List<InfoForm> GetList()
        {
            List<InfoForm> result = new List<InfoForm>();
            using (var context = new InfoDbContext())
            {
                foreach (var t in (from s in context.InfoForms orderby s.HighestScore descending select s).ToList())
                {
                    result.Add(t);
                }
            }
            return result;
        }

        public List<InfoForm> GetList(int number)
        {
            List<InfoForm> result = new List<InfoForm>();
            using (var context = new InfoDbContext())
            {
                foreach (var t in (from s in context.InfoForms orderby s.HighestScore descending select s).Take(number).ToList())
                {
                    result.Add(t);
                }
            }
            return result;
        }

        public void Refresh()
        {
            using (var context = new InfoDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Info");
            }

        }

        public void UpdateScore(InfoForm form)
        {
            InfoForm f = (InfoForm)form;
            using (var context = new InfoDbContext())
            {

                context.InfoForms.Update(f);
                context.SaveChanges();
            }
        }
    }
}