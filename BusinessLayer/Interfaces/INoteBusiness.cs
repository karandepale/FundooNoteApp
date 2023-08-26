using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBusiness
    {
        public NoteEntity CreateNote(NoteCreateModel model , long userid);
        public List<NoteEntity> GetAllNotes(long userID);
    }
}
