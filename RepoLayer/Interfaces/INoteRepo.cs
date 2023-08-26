using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interfaces
{
    public interface INoteRepo
    {
        public NoteEntity CreateNote(NoteCreateModel model, long userid);
    }
}
