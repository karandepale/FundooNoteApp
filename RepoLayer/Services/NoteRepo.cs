﻿using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundooContext fundooContext;
        public NoteRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }


        //NOTE CREATE:-
        public NoteEntity CreateNote(NoteCreateModel model , long userid)
        {
            NoteEntity noteEntity = new NoteEntity();
            
            noteEntity.UserID = userid;
            noteEntity.Title = model.Title;
            noteEntity.Description = model.Description;
            noteEntity.Reminder = model.Reminder;
            noteEntity.Background = model.Background;
            noteEntity.Image = model.Image;
            noteEntity.IsArchive = model.IsArchive;
            noteEntity.IsPin = model.IsPin;
            noteEntity.IsTrash = model.IsTrash;

            fundooContext.Note.Add(noteEntity);
            fundooContext.SaveChanges();

            if(noteEntity != null)
            {
                return noteEntity;
            }
            else
            {
                return null;
            }
        }


        //GET ALL NOTES:-
        public List<NoteEntity> GetAllNotes(long userID)
        {
            try
            {
                var noteList = fundooContext.Note.Where
                    (data => data.UserID == userID).ToList();
                if(noteList != null)
                {
                    return noteList;
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
