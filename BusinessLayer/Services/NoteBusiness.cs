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

        //GET ALL NOTES:-
        public List<NoteEntity> GetAllNotes(long userID)
        {
            try
            {
                return noteRepo.GetAllNotes(userID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // GET NOTE BY ID:-
        public NoteEntity GetNoteByID(long NoteID, long UserID)
        {
            try
            {
                return noteRepo.GetNoteByID(NoteID, UserID);
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        //UPDATE NOTE:-
        public NoteEntity UpdateNote(NoteUpdateModel model, long NoteID, long UserID)
        {
            try
            {
                return noteRepo.UpdateNote(model, NoteID, UserID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //DELETE NOTE:-
        public void DeleteNote(long NoteID, long UserID)
        {
            try
            {
                noteRepo.DeleteNote(NoteID, UserID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}
