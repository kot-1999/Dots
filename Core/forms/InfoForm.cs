

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace dots.forms
{
    [Serializable]
    [DataContract]
    public class InfoForm : Form
    {

       
 
        [Key]
        [DataMember]
        public string Player { get;  set; }

        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int HighestScore { get; set; }



  

        public InfoForm()
        {

        }

        public InfoForm(string Player, string Email, int HighestScore)
        {
      
            this.Email = Email;
            this.Player = Player;
            this.HighestScore = HighestScore;
        }

        public override string ToString()
        {
            return base.ToString() + " " + Player + " " + Email + " " + HighestScore ;
        }

    }
}
