using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }


        //CREATE NOTE:-
        public NoteEntity CreateNote(NoteCreateModel model , long userid)
        {
            try
            {
                return noteRepo.CreateNote(model , userid);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}
