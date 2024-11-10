using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.DataAccess.Helpers
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Title { get; set; }

        public ErrorResponse(int status, string title)
        {
            Status = status;
            Title = title;
        }
    }
}
