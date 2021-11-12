using System;
using System.Text.RegularExpressions;

namespace dots
{
    [Serializable]
    public class Player
    {
        public int Id { get; set; }


        
     /*   private readonly Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex nameRegex = new Regex(@"^([\w\.\-]+)");*/

        public int Score { get; set; }
        public int Highest { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }



        /* public void SetName()
         {
             do
             {
                 Console.Write("Enter your name: ");
             }
             while (ValidateName(name = Console.ReadLine()) != true) ;
         }

         public void SetEmail()
         {
             do
             {
                 Console.Write("Enter your email: ");
             }
             while (ValidateEmail(email = Console.ReadLine()) != true) ;
         }*/

        /* private bool ValidateName(string n)
         {
             var match = nameRegex.Match(n);
             return match.Success;
         }

         private bool ValidateEmail(string n)
         {
             var match = emailRegex.Match(n);
             return match.Success;
         }*/
    }
}