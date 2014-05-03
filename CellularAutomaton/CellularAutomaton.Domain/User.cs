using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CellularAutomaton.Domain
{
    public class User: IdentityUser, IEntity
    {
        public override string Id { get; set; }

        public string NickName { get; set; }

        public DateTime BirthDay { get; set; }

        public string Avatar { get; set; }

        public string Thumbnail { get; set; }

        public string Icon { get; set; }

        public string ConfirmationToken { get; set; }

        public bool IsConfirmed { get; set; }

        [InverseProperty("Recipient")]
        public virtual List<Message> IncomingMessages { get; set; }

        [InverseProperty("Sender")]
        public virtual List<Message> OutgoingMessages { get; set; }
    }
}
