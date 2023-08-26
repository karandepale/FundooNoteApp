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

        public NoteEntity CreateNote(NoteCreateModel model)
        {
            try
            {
                return noteRepo.CreateNote(model);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}
