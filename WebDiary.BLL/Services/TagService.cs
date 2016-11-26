using System.Collections.Generic;
using System.Linq;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork iUnitOfWork)
        {
            _unitOfWork = iUnitOfWork;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _unitOfWork.TagRepository.Get();
        }

        public void AddTag(Tag tag)
        {
            _unitOfWork.TagRepository.Create(tag);
            _unitOfWork.Save();
        }

        public Tag GetTagById(int id)
        {
            return _unitOfWork.TagRepository.Get(id);
        }

        public Tag GetByName(string name)
        {
            return _unitOfWork.TagRepository.Get(t => t.Name == name).FirstOrDefault();
        }

        public void TagUpdate(Tag tag)
        {
            _unitOfWork.TagRepository.Update(tag);
            _unitOfWork.Save();
        }

        public void DeleteTag(Tag tag)
        {
            _unitOfWork.TagRepository.Delete(tag);
            _unitOfWork.Save();
        }

        public IEnumerable<Tag> GetTagsByNote(Note note)
        {
            return _unitOfWork.TagRepository.Select(t => t).Where(t=>t.Notes.FirstOrDefault(n => n.Id == note.Id)!=null);
        }
    }
}
