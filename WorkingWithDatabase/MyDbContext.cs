using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWithDatabase
{
    public class MyDbContext : DbContext
    {

        //creatim empty constructor:
        public MyDbContext()
        {
            
        }

        public MyDbContext(DbContextOptions options) : base (options)
        {

        }
        //dbcontext class всегда должен быть унаследован, so we created a new class mydbcontext
        public DbSet<User> Users { get; set; }



        //если нужно вывести данные с др таблицы (не юзерс), прописываем еще проперти 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=AdoNetExample;Trusted_Connection=True;Encrypt=False";
            base.OnConfiguring(optionsBuilder);

            //чтобы не вызыват мтеод sqlserver, нужна след проверка:
            if (optionsBuilder.IsConfigured == false)
            {
                //вызывается optionsBuilder.UseSqlServer, т.к свойство isCOnfigured все еще false:
                //как только этот мтеод выполняетс, ему присваивается true, сколько бы мы ни инициализировали dbCOntext
                optionsBuilder.UseSqlServer(connectionString);
                //объект optionsbuilder - static, шареный на всех, благодаря этому sqlserver
                //можно не вызыыать каждый раз
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //основной метод для конфигурации сущностей:
            // в осн будем юзать только его:
            modelBuilder.Entity<User>(entityTypeBuilder =>
            {
                //специфицируем какая проперти является primary key:
                entityTypeBuilder.HasKey(w => w.UserId);
                entityTypeBuilder.Property(q => q.FirstName).IsRequired().HasMaxLength(50);
                entityTypeBuilder.Property(q => q.LastName).IsRequired().HasMaxLength(50);

                entityTypeBuilder.HasIndex(q =>
                //если набрасываем unique на поля, юзаем hasindex и анонимные поля (либо q=>q.FirstName, если помечаем только одно проперти юником),  :
                new { q.FirstName, q.LastName }).IsUnique();
            });
        }
    }
}
