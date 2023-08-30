using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
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



        // CREATE LABEL:-
        public LabelEntity CreateLabel(LabelCreateModel model, long NoteID)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.Title = model.Title;
                label.NoteID = NoteID;

                fundooContext.Label.Add(label);
                fundooContext.SaveChanges();

                if (label != null)
                {
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }





    }
}
