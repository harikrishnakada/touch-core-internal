using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Model
{
    public class Badge
    {
        public virtual string BadgeName { get; set; }

        public virtual string Category { get; set; }

        public virtual Guid Id { get; set; }

        public virtual int Score { get; set; }

        //protected void WriteViewModel(Badge viewModel)
        //{
        //    this.BadgeName = viewModel.BadgeName;
        //    this.Category = viewModel.BadgeName;
        //    this.Score = viewModel.Score;
        //}
    }
}