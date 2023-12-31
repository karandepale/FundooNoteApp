﻿using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }


        //CREATE LABEL:-
        public LabelEntity CreateLabel(LabelCreateModel model, long NoteID, long UserID)
        {
            try
            {
                return labelRepo.CreateLabel(model, NoteID , UserID);
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }


        //GET ALL LABELS FOR A NOTE:-
        public List<LabelEntity> GetAllLabels(long NoteId)
        {
            try
            {
                return labelRepo.GetAllLabels(NoteId);
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }


        //UPDATE LABEL:-
        public LabelEntity UpdateLabel(LabelUpdateModel model, long LabelID)
        {
            try
            {
                return labelRepo.UpdateLabel(model, LabelID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //DELETE LABEL:-
        public void DeleteLabel(long LabelID)
        {
            try
            {
                 labelRepo.DeleteLabel(LabelID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
