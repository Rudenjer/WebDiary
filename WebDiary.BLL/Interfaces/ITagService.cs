using System.Collections.Generic;
using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();

        void AddTag(Tag tag);

        Tag GetTagById(int id);

        Tag GetByName(string name);

        void TagUpdate(Tag tag);

        void DeleteTag(Tag tag);
        IEnumerable<Tag> GetTagsByNote(Note note);
    }
}