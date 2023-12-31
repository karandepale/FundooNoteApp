﻿using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundooContext fundooContext; 
        private readonly Cloudinary cloudinary;
        private readonly FileService fileService;

        public NoteRepo(FundooContext fundooContext , Cloudinary cloudinary, FileService fileService)
        {
            this.fundooContext = fundooContext;
            this.cloudinary = cloudinary;
            this.fileService = fileService;
        }


        //CREATE NOTE:-
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


        //GET NOTE BY ID:-
        public NoteEntity GetNoteByID(long NoteID , long UserID)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault
                (data => data.UserID == UserID && data.NoteID == NoteID);
                if(note != null)
                {
                    return note;
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


        //UPDATE NOTE:-
        public NoteEntity UpdateNote(NoteUpdateModel model , long NoteID , long UserID)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault
                    (data => data.UserID == UserID && data.NoteID == NoteID);
                if(note != null)
                {
                    note.Title = model.Title;
                    note.Description = model.Description;
                    note.Reminder = model.Reminder;
                    note.Background = model.Background;
                    note.Image = model.Image;
                    note.IsArchive = model.IsArchive;
                    note.IsPin = model.IsPin; 
                    note.IsTrash = model.IsTrash;

                    fundooContext.SaveChanges();
                    return note;
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


        //DELETE NOTE:-
        public void DeleteNote(long NoteID , long UserID)
        {
            try
            {
                var result = fundooContext.Note.FirstOrDefault
                    (data => data.NoteID == NoteID && data.UserID == UserID);
                if(result != null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //SEARCH NOTE:-
        public List<NoteEntity> SearchNoteByQuery(string query , long UserID)
        {
            try
            {
                var notes = fundooContext.Note.Where
                    (
                    data => data.UserID == UserID &&
                            data.Title.Contains(query) ||
                            data.Description.Contains(query)
                    ).ToList();
                if(notes != null)
                {
                    return notes;
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


        //COPY NOTE:-
        public NoteEntity CopyNote(long NoteID , long UserID)
        {
            try
            {
                var presentNote = fundooContext.Note.FirstOrDefault
                    (data => data.UserID == UserID && data.NoteID == NoteID);
                if(presentNote != null)
                {
                    NoteEntity noteEntity = new NoteEntity
                    {
                        UserID = presentNote.UserID,
                        Title = presentNote.Title,
                        Description = presentNote.Description,
                        Reminder = presentNote.Reminder,
                        Background = presentNote.Background,
                        Image = presentNote.Image,
                        IsArchive = presentNote.IsArchive,
                        IsPin = presentNote.IsPin,
                        IsTrash = presentNote.IsTrash,
                    };
                    fundooContext.Note.Add(noteEntity);
                    fundooContext.SaveChanges();
                    List<NoteEntity> list = new List<NoteEntity>();
                    list.Add(noteEntity);
                    return noteEntity;
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


        //ARCHIVE NOTE:-
        public bool Archive(long NoteID , long UserID)
        {
            try
            {
                var notePresent = fundooContext.Note.FirstOrDefault
                    (data => data.UserID == UserID && data.NoteID == NoteID);
                if(notePresent != null)
                {
                    notePresent.IsArchive = !notePresent.IsArchive;
                    fundooContext.SaveChanges();
                    return notePresent.IsArchive;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //PIN NOTE:-
        public bool Pin(long NoteID, long UserID)
        {
            try
            {
                var notePresent = fundooContext.Note.FirstOrDefault
                    (data => data.UserID == UserID && data.NoteID == NoteID);
                if (notePresent != null)
                {
                    notePresent.IsPin = !notePresent.IsPin;
                    fundooContext.SaveChanges();
                    return notePresent.IsPin;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //TRASH NOTE:-
        public bool Trash(long NoteID, long UserID)
        {
            try
            {
                var notePresent = fundooContext.Note.FirstOrDefault
                    (data => data.UserID == UserID && data.NoteID == NoteID);
                if (notePresent != null)
                {
                    notePresent.IsTrash = !notePresent.IsTrash;
                    fundooContext.SaveChanges();
                    return notePresent.IsTrash;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //IMAGE UPLOAD ON CLOUDINARY:-
        public async Task<Tuple<int, string>> Image(long id, long usedId, IFormFile imageFile)
        {
            var result = fundooContext.Note.FirstOrDefault(x => x.NoteID == id && x.UserID == usedId);
            if (result != null)
            {
                try
                {
                    var data = await fileService.SaveImage(imageFile);
                    if (data.Item1 == 0)
                    {
                        return new Tuple<int, string>(0, data.Item2);
                    }

                    var UploadImage = new ImageUploadParams
                    {
                        File = new CloudinaryDotNet.FileDescription(imageFile.FileName, imageFile.OpenReadStream())
                    };

                    ImageUploadResult uploadResult = await cloudinary.UploadAsync(UploadImage);
                    string imageUrl = uploadResult.SecureUrl.AbsoluteUri;
                    result.Image = imageUrl;

                    fundooContext.Note.Update(result);
                    fundooContext.SaveChanges();

                    return new Tuple<int, string>(1, "Image Uploaded Successfully");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }



    }
}
