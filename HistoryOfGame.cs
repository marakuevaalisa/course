using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsApp1
{
    public partial class HistoryOfGame
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public string Winner { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
