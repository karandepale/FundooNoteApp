using RepoLayer.Context;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooContext fundooContext;
        public LabelRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }






    }
}
