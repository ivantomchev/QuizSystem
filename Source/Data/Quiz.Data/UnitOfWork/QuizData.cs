namespace Quiz.Data.UnitOfWork
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;

    using Quiz.Data.Common.Repository;
    using Quiz.Data.Models;

    public class QuizData : IQuizData
    {
        private readonly IApplicationDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public QuizData(IApplicationDbContext context)
        {
            this.context = context;
        }

        public IApplicationDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Author> Authors
        {
            get { return this.GetRepository<Author>(); }
        }

        public IRepository<Quote> Quotes
        {
            get { return this.GetRepository<Quote>(); }
        }

        public IRepository<Question> Questions
        {
            get { return this.GetRepository<Question>(); }
        }

        public IRepository<Exam> Exams
        {
            get { return this.GetRepository<Exam>(); }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
