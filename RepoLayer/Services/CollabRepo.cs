using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooContext fundooContext;
        public CollabRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }



        // CREATE COLLAB:-
        public CollabEntity CreateCollab(CollabCreateModel model, long NoteID , long userId)
        {
            try
            {
              
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.Email = model.Email;
                collabEntity.UserID = userId;
                collabEntity.NoteID = NoteID;

                fundooContext.Collab.Add(collabEntity);
                fundooContext.SaveChanges();

                if (collabEntity != null)
                {
                    return collabEntity;
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



        // GET ALL COLLABS:-
        public List<CollabEntity> GetCollabsForANote(long NoteID)
        {
            try
            {
                var collabList = fundooContext.Collab.Where
                    (data => data.NoteID == NoteID).ToList();

                if(collabList != null)
                {
                    return collabList;
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
