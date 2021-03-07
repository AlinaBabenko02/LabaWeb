using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AirTickets_project
{
    public partial class Air_TicketsContext : DbContext
    {
        public Air_TicketsContext()
        {
        }

        public Air_TicketsContext(DbContextOptions<Air_TicketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-1LJ593T\\SQLEXPRESS; Database=Air_Tickets; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.AirportsId);

                entity.Property(e => e.AirportsId).HasColumnName("Airports_Id");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .HasColumnName("adress");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.NameOfAirport)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name_of_airport");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientsId)
                    .HasName("PK_client");

                entity.Property(e => e.ClientsId).HasColumnName("Clients_ID");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Passport)
                    .HasMaxLength(50)
                    .HasColumnName("passport");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.FlightsId);

                entity.Property(e => e.FlightsId).HasColumnName("Flights_Id");

                entity.Property(e => e.ArrivalTime).HasColumnName("arrival_time");

                entity.Property(e => e.DepartureTime).HasColumnName("departure_time");

                entity.Property(e => e.FlightTime).HasColumnName("flight_time");

                entity.Property(e => e.PlaceOfArrival).HasColumnName("place_of_arrival");

                entity.Property(e => e.PlaceOfDeparture).HasColumnName("place_of_departure");

                entity.Property(e => e.PlanesId).HasColumnName("Planes_Id");

                entity.HasOne(d => d.PlaceOfArrivalNavigation)
                    .WithMany(p => p.FlightPlaceOfArrivalNavigations)
                    .HasForeignKey(d => d.PlaceOfArrival)
                    .HasConstraintName("FK_Flights_Airports");

                entity.HasOne(d => d.PlaceOfDepartureNavigation)
                    .WithMany(p => p.FlightPlaceOfDepartureNavigations)
                    .HasForeignKey(d => d.PlaceOfDeparture)
                    .HasConstraintName("FK_Flights_Airports1");

                entity.HasOne(d => d.Planes)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.PlanesId)
                    .HasConstraintName("FK_Flights_Planes");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.ModelsId)
                    .HasName("PK_plane_model");

                entity.Property(e => e.ModelsId).HasColumnName("Models_Id");

                entity.Property(e => e.Firm)
                    .HasMaxLength(50)
                    .HasColumnName("firm");

                entity.Property(e => e.ModelName)
                    .HasMaxLength(50)
                    .HasColumnName("model_name");

                entity.Property(e => e.NumberOfSeats).HasColumnName("number_of_seats");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.HasKey(e => e.PlanesId)
                    .HasName("PK_plane");

                entity.Property(e => e.PlanesId).HasColumnName("Planes_Id");

                entity.Property(e => e.ModelsId).HasColumnName("Models_Id");

                entity.HasOne(d => d.Models)
                    .WithMany(p => p.Planes)
                    .HasForeignKey(d => d.ModelsId)
                    .HasConstraintName("FK_Planes_Models");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketsId)
                    .HasName("PK_ticket");

                entity.Property(e => e.TicketsId).HasColumnName("Tickets_Id");

                entity.Property(e => e.ClientsId).HasColumnName("Clients_Id");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.FlightsId).HasColumnName("Flights_Id");

                entity.Property(e => e.TypesId).HasColumnName("Types_Id");

                entity.HasOne(d => d.Clients)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ClientsId)
                    .HasConstraintName("FK_Tickets_Clients");

                entity.HasOne(d => d.Flights)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FlightsId)
                    .HasConstraintName("FK_Tickets_Flights");

                entity.HasOne(d => d.Types)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TypesId)
                    .HasConstraintName("FK_Tickets_Types");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.TypesId)
                    .HasName("PK_ticket_type");

                entity.Property(e => e.TypesId).HasColumnName("Types_Id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .HasColumnName("type_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
