using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basics
{
    /// <summary>
    /// Iskolai kontextus: A diákok osztályokba járnak, az osztályokba diákok és van osztályfőnöke. Egy tanár lehet osztályfőnök de nem biztos, hogy tényleg az. Egy diáknak vannak tanárai.
    /// </summary>
    class Context : DbContext
    {
        const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BasicSampleContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        /// <summary>
        /// Diákok táblája.
        /// </summary>
        public DbSet<Student> Students { get; set; }
        /// <summary>
        /// Osztályok táblája.
        /// </summary>
        public DbSet<Class> Classes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
    class Student
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(50)]
        public required string FirstName { get; set; }
        [Required, StringLength(50)]
        public required string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        [Required]
        public required Class Class { get; set; }
    }
    class Class
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(10)]
        public required string Identifier { get; set; }
        public List<Student> Students { get; } = new();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var c = new Class()
            {
                Identifier = "12.B"
            };
            Student s = new Student()
            {
                LastName = "Szegedi",
                FirstName = "Barnabás",
                Class = c
            };
        }
    }
}
