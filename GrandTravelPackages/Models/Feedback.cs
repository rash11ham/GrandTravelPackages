using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int OrderId { get; set; }
        public int PackageId { get; set; }
        public string OrdFeedback { get; set; }
        public string UserId { get; set; }
    }
}
