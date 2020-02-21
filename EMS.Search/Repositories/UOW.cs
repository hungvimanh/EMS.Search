using EMS.Search.Repositories.Models;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace EMS.Search.Repositories
{
    public interface IUOW : IServiceScoped
    {
        Task Begin();
        Task Commit();
        Task Rollback();
        IMajorsRepository MajorsRepository { get; }
        ISubjectGroupRepository SubjectGroupRepository { get; }
        IUniversity_MajorsRepository University_MajorsRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IUniversity_Majors_SubjectGroupRepository University_Majors_SubjectGroupRepository { get; }
    }
    public class UOW : IUOW
    { 
        private EMSContext eMSContext;
        public IMajorsRepository MajorsRepository { get; private set; }
        public ISubjectGroupRepository SubjectGroupRepository { get; private set; }
        public IUniversity_MajorsRepository University_MajorsRepository { get; private set; }
        public IUniversity_Majors_SubjectGroupRepository University_Majors_SubjectGroupRepository { get; private set; }
        public IUniversityRepository UniversityRepository { get; private set; }

        public UOW(EMSContext _eMSContext)
        {
            eMSContext = _eMSContext;
            MajorsRepository = new MajorsRepository(eMSContext);
            SubjectGroupRepository = new SubjectGroupRepository(eMSContext);
            University_MajorsRepository = new University_MajorsRepository(eMSContext);
            University_Majors_SubjectGroupRepository = new University_Majors_SubjectGroupRepository(eMSContext);
            UniversityRepository = new UniversityRepository(eMSContext);
            EntityFrameworkManager.ContextFactory = DbContext => eMSContext;
        }

        public async Task Begin()
        {
            await eMSContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            eMSContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            eMSContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}
