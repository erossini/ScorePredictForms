﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScorePredict.Common.Data
{
    public class WeekSummary
    {
        public int Ranking { get; set; }
        public string UserId { get; set; }
        public string WeekId { get; set; }
        public int Points { get; set; }
        public int UserCount { get; set; }
        public int TotalPredictions { get; set; }
        public int WeekNumber { get; set; }
        public int Year { get; set; }
        public int GamesCount { get; set; }
    }
}
