namespace Quiz.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Quiz.Data.Common.Repository;
    using Quiz.Data.Models;
    using Quiz.Data;

    public interface IQuizData
    {
        IApplicationDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Author> Authors { get; }

        IRepository<Quote> Quotes { get; }

        IRepository<Question> Questions { get; }

        IRepository<Exam> Exams { get; }

        int SaveChanges();
    }
}
