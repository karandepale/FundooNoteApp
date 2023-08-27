using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INoteBusiness
    {
        public NoteEntity CreateNote(NoteCreateModel model , long userid);
        public List<NoteEntity> GetAllNotes(long userID);
        public NoteEntity GetNoteByID(long NoteID, long UserID);
        public NoteEntity UpdateNote(NoteUpdateModel model, long NoteID, long UserID);
        public void DeleteNote(long NoteID, long UserID);
        public List<NoteEntity> SearchNoteByQuery(string query, long UserID);
        public bool Archive(long NoteID, long UserID);
        public bool Pin(long NoteID, long UserID);
        public bool Trash(long NoteID, long UserID);
        public Task<Tuple<int, string>> Image(long id, long usedId, IFormFile imageFile);
    }
}
