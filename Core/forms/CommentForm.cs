

using System;
using System.ComponentModel.DataAnnotations;

namespace dots.forms
{
    [Serializable]
    public class CommentForm : Form
    {
        

        public int Id { get; set; }
        public string Player { get; set; }
        public string Comment { get; set; }
        public int Raiting { get; set; }
        public CommentForm(string Player, string Comment, int Raiting)
        {
            this.Comment = Comment;
            this.Player = Player;
            this.Raiting = Raiting;

        }
        public CommentForm()
        {
    
        }

        public override string ToString()
        {
            return base.ToString()+" " + Id+" " + Player + " "+Comment + " "+Raiting;
        }


    }
}
