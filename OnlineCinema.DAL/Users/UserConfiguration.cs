using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineCinema.Domain.Core;
using OnlineCinema.Domain.User;

namespace OnlineCinema.DAL.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        
        builder.OwnsOne(b => b.PersonalName, 
            pn =>
            {
                pn.OwnsOne(p => p.FirstName, 
                    fn =>
                        fn
                            .Property(x => x.Value).HasColumnName("FirstName")
                            .HasColumnType("nvarchar(20)"));

                pn.OwnsOne(p => p.LastName, 
                    ln =>
                        ln
                            .Property(x => x.Value).HasColumnName("LastName")
                            .HasColumnType("nvarchar(20)")
                    );
            });

        var ageConverter = new ValueConverter<Age, int>(
            v => v.Value,
            v => new Age(v));

        builder
            .Property(p => p.Age)
            .HasConversion(ageConverter)
            .HasColumnName("Age")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(p => p.Country)
            .HasMaxLength(20);
        
        builder.OwnsOne(b => b.Username, 
            pn =>
            {
                pn.Property(x => x.Value).HasColumnName("Username")
                    .HasColumnType("nvarchar(20)")
                    .IsRequired();
            });

        builder.Property(p => p.DateOfBirth);
        builder.Property(p => p.ImageSource);
        builder.OwnsOne(b => b.Email, 
            pn =>
            {
                pn.Property(x => x.Value).HasColumnName("Email")
                    .HasColumnType("nvarchar(20)");
            });
        
        var passwordConverter = new ValueConverter<Password, byte[]>(
            v => v.SaltedHash(),
            hash => new Password(hash));
        
        builder
            .Property(p => p.Password)
            .HasConversion(passwordConverter)
            .HasColumnName("Password")
            .HasColumnType("binary(64)")
            .IsRequired();
    }
}