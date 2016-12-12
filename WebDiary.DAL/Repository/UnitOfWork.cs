using System;
using WebDiary.DAL.Context;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<GenericRepository<Note, int>> _noteRepository;
        private readonly Lazy<GenericRepository<Tag, int>> _tagRepository;
        private readonly Lazy<GenericRepository<ApplicationUser, string>> _userRepository;
        private readonly Lazy<GenericRepository<Comment, int>> _commentRepository;
        private readonly Lazy<GenericRepository<RequestFriend, int>> _requestFriendRepository;

        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            _noteRepository = new Lazy<GenericRepository<Note, int>>(() => new GenericRepository<Note, int>(_dbContext));
            _tagRepository = new Lazy<GenericRepository<Tag, int>>(() => new GenericRepository<Tag, int>(_dbContext));
            _userRepository = new Lazy<GenericRepository<ApplicationUser, string>>(() => new GenericRepository<ApplicationUser, string>(_dbContext));
            _commentRepository = new Lazy<GenericRepository<Comment, int>>(()=> new GenericRepository<Comment, int>(_dbContext));
            _requestFriendRepository = new Lazy<GenericRepository<RequestFriend, int>>(() => new GenericRepository<RequestFriend, int>(_dbContext));
        }

        public IRepository<Note, int> NoteRepository => _noteRepository.Value;

        public IRepository<Tag, int> TagRepository => _tagRepository.Value;

        public IRepository<ApplicationUser, string> UserRepository => _userRepository.Value;

        public IRepository<RequestFriend, int> RequestFriendRepository => _requestFriendRepository.Value;

        public IRepository<Comment, int> CommentRepository => _commentRepository.Value; 

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
