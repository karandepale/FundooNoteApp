using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBusiness : ICollabBusiness
    {
        private readonly ICollabRepo collabRepo;
        public CollabBusiness(ICollabRepo collabRepo)
        {
            this.collabRepo = collabRepo;   
        }



        // CREATE COLLAB:-
        public CollabEntity CreateCollab(CollabCreateModel model, long NoteID, long userId)
        {
            try
            {
                return collabRepo.CreateCollab(model, NoteID, userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //GET ALL COLLABS:-
        public List<CollabEntity> GetCollabsForANote(long NoteID)
        {
            try
            {
                return collabRepo.GetCollabsForANote(NoteID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}
