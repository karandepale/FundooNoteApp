using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interfaces
{
    public interface ILabelRepo
    {
        public LabelEntity CreateLabel(LabelCreateModel model, long NoteID, long UserID);
        public List<LabelEntity> GetAllLabels(long NoteId);
        public LabelEntity UpdateLabel(LabelUpdateModel model, long LabelID);
        public void DeleteLabel(long LabelID);
    }
}
