using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBusiness
    {
        public CollabEntity CreateCollab(CollabCreateModel model, long NoteID, long userId);
<<<<<<< HEAD
=======
        public CollabEntity GetCollabsForANote(long NoteID);
<<<<<<< HEAD
>>>>>>> Dev
=======
        public void DeleteACollab(long CollabID);
>>>>>>> Collab_DeleteCollab_API
    }
}
