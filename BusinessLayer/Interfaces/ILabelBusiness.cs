using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBusiness
    {
        public LabelEntity CreateLabel(LabelCreateModel model, long NoteID);
        public List<LabelEntity> GetAllLabels(long NoteId);
        public LabelEntity UpdateLabel(LabelUpdateModel model, long LabelID);
        public void DeleteLabel(long LabelID);
    }
}
