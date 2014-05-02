using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CellularAutomaton.Domain.Interfaces
{
    public interface ICellularAutomatonUser : IUser, IEntity
    {
        string NickName { get; set; }

        DateTime BirthDay { get; set; }

        string Avatar { get; set; }

        string Thumbnail { get; set; }

        string Icon { get; set; }

        string ConfirmationToken { get; set; }

        bool IsConfirmed { get; set; }

        #region Navigation Properties

        [InverseProperty("Recipient")]
        List<Message> IncomingMessages { get; set; }

        [InverseProperty("Sender")]
        List<Message> OutgoingMessages { get; set; }

        List<Mark> Marks { get; set; }

        #endregion
    }
}
