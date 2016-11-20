using Ninject;
using WebDiary.BLL.Interfaces;
using WebDiary.BLL.Services;
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

            return kernel;
        }
    }
}