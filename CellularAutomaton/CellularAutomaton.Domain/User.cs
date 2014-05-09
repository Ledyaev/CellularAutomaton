using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CellularAutomaton.Domain
{
    public class User: IdentityUser, IEntity
    {
        public override string Id { get; set; }

        public string NickName { get; set; }

        public string ConfirmationToken { get; set; }

        [JsonIgnore]
        [InverseProperty("Recipient")]
        public virtual List<Message> IncomingMessages { get; set; }

        [JsonIgnore]
        [InverseProperty("Sender")]
        public virtual List<Message> OutgoingMessages { get; set; }
    }
}
