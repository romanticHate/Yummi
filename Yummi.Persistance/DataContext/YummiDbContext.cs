using Microsoft.EntityFrameworkCore;
using Yummi.Core.Entities;

namespace Yummi.Persistance.DataContext
{
    public partial class YummiDbContext : DbContext
    {
        public YummiDbContext()
        {
        }

        public YummiDbContext(DbContextOptions<YummiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Measure> Measures { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeAuthor> RecipeAuthors { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<RecipeRating> RecipeRatings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.HasIndex(e => e.Email, "UQ__Author__AB6E6164D4993BAC")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Calories)
                    .HasMaxLength(10)
                    .HasColumnName("calories")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.ToTable("Measure");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CookTime).HasColumnName("cook_time");

                entity.Property(e => e.Course)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("course");

                entity.Property(e => e.Cuisine)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cuisine");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Instructions)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("instructions");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PrepTime).HasColumnName("prep_time");
            });

            modelBuilder.Entity<RecipeAuthor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RecipeAuthor");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_author_recipe");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_recipe_author");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                //entity.HasNoKey();
                entity.Property(e => e.Id).HasColumnName("id");

                entity.ToTable("RecipeIngredient");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.MeasureId).HasColumnName("measure_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany()
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ingredient");

                entity.HasOne(d => d.Measure)
                    .WithMany()
                    .HasForeignKey(d => d.MeasureId)
                    .HasConstraintName("fk_measure");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_recipe");
            });

            modelBuilder.Entity<RecipeRating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RecipeRating");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.RatingId).HasColumnName("rating_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_author_rating");

                entity.HasOne(d => d.Rating)
                    .WithMany()
                    .HasForeignKey(d => d.RatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rating_recipe");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_recipe_rating");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
