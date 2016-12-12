using Ninject;
using WebDiary.BLL.Interfaces;
using WebDiary.BLL.Services;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Pipeline;
using WebDiary.DAL.Repository;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary
{
    public class DependencyConfigurations
    {
        public static IKernel RegisterDependency()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<INoteService>().To<NoteSevrice>();

            kernel.Bind<ITagService>().To<TagService>();

            kernel.Bind<ICommentService>().To<ICommentService>();

            kernel.Bind<IPipeline<Note>>().To<Pipeline<Note>>();

            kernel.Bind<IRequestFriendService>().To<RequestFriendService>();

            return kernel;
        }
    }
}