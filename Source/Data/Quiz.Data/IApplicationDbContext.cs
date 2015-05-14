namespace Quiz.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Quiz.Data.Models;

    public interface IApplicationDbContext
    {
        IDbSet<Author> Authors { get; set; }

        IDbSet<Quote> Quotes { get; set; }

        IDbSet<Question> Questions { get; set; }

        IDbSet<Exam> Exams { get; set; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
