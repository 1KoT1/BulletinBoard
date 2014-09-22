using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BulletinBoard.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using log4net;

namespace BulletinBoard.SqlRepository
{
    public class SqlRepository : IRepository
    {
        private string conString = "";

        public SqlRepository()
        {
            var conStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (conStringSettings == null)
            {
                log.Error("Необходимо указать строку соединения с SQL.");
                return;
            }
            if (conStringSettings.ProviderName != "System.Data.SqlClient")
            {
                log.Fatal("Неподдерживаемый тип провайдера SQL.");
                return;
            }

            conString = conStringSettings.ConnectionString;
        }

        public IEnumerable<Advertisement> Advertisements
        {
            get
            {
                using (var connection = new SqlConnection(conString))
                {
                    connection.Open();
                    var command = new SqlCommand(@"SELECT
                                                         Advertisements.IdAdvertisement,
                                                         Advertisements.Name,
                                                         Advertisements.Description,
                                                         Advertisements.PublishDate,
                                                         Advertisements.Price,
                                                         Contacts.IdContacts,
                                                         Contacts.Text
                                                      FROM
                                                         Advertisements,
                                                         Contacts
                                                      WHERE
                                                         Advertisements.Contacts_IdContacts = Contacts.IdContacts",
                                                  connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Advertisement(
                                Convert.ToString(reader["Name"]),
                                Convert.ToString(reader["Description"]),
                                Convert.ToUInt32(reader["Price"]),
                                Convert.ToDateTime(reader["PublishDate"]),
                                new Contacts(
                                    Convert.ToString(reader["Text"]),
                                    Convert.ToInt32(reader["IdContacts"])
                                    ),
                                Convert.ToInt32(reader["IdAdvertisement"])
                                );
                        }
                    }
                }
            }
        }

        public Advertisement CreateAdvertisement(Advertisement advertisement)
        {
            if (advertisement.IdAdvertisement != 0)
            {
                throw new CreatedObjectIsNotEmptyException();
            }

            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();               
                try{
                    var command = new SqlCommand(@"INSERT INTO Contacts
                                                           (Text)
                                                       VALUES
                                                           (@ContactsText);

                                                   INSERT INTO Advertisements
                                                           (Name,
                                                            Description,
                                                            PublishDate,
                                                            Contacts_IdContacts,
                                                            Price)
                                                       VALUES
                                                           (@Name,
                                                            @Description,
                                                            @PublishDate,
                                                            CAST(scope_identity() AS int),
                                                            @Price)

                                                            SELECT CAST(scope_identity() AS int)",
                                                  connection,
                                                  transaction);

                    command.Parameters.AddWithValue("@ContactsText", advertisement.Contacts.Text);
                    command.Parameters.AddWithValue("@Name", advertisement.Name);
                    command.Parameters.AddWithValue("@Description", advertisement.Description);
                    command.Parameters.AddWithValue("@PublishDate", advertisement.PublishDate);
                    command.Parameters.AddWithValue("@Price", Convert.ToInt32(advertisement.Price));

                    advertisement.IdAdvertisement = Convert.ToInt32(command.ExecuteScalar());

                    if (advertisement.IdAdvertisement != 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message);
                    transaction.Rollback();
                }
            }

            return advertisement;
        }

        public void RemoveAdvertisemen(Advertisement advertisement)
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();
                try
                {
                    var command = new SqlCommand(@"DELETE FROM Advertisements
                                                       WHERE Advertisements.IdAdvertisement = @IdAdvertisement",
                                                  connection,
                                                  transaction);

                    command.Parameters.AddWithValue("@IdAdvertisement", advertisement.IdAdvertisement);

                    int numberOfDeletedRows = command.ExecuteNonQuery();

                    if (numberOfDeletedRows == 1)
                    {
                        transaction.Commit();
                    }
                    else if (numberOfDeletedRows == 0)
                    {
                        transaction.Rollback();
                        throw new RepositoryHasNotThisItemException();
                    }
                    else
                    {
                        // Такого не может быть.
                        transaction.Rollback();
                        log.Fatal("Было удалено несколько записей с одинаковыми ID.");
                    }
                }
                catch (RepositoryHasNotThisItemException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        public void UpdateAdvertisemen(Advertisement advertisement)
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();
                try
                {
                    var command = new SqlCommand(@"UPDATE Advertisements
                                                      SET Name = @Name,
                                                          Description = @Description,
                                                          PublishDate = @PublishDate,
                                                          Price = @Price,
                                                      WHERE Advertisements.IdAdvertisement=@IdAdvertisement;

                                                   UPDATE Contacts
                                                      SET Text = @Text
                                                      WHERE IdContact=@IdContact",
                                                  connection,
                                                  transaction);

                    command.Parameters.AddWithValue("@IdAdvertisement", advertisement.IdAdvertisement);
                    command.Parameters.AddWithValue("@Name", advertisement.Name);
                    command.Parameters.AddWithValue("@Description", advertisement.Description);
                    command.Parameters.AddWithValue("@Price", advertisement.Price);
                    command.Parameters.AddWithValue("@IdContact", advertisement.Contacts.IdContacts);
                    command.Parameters.AddWithValue("@Text", advertisement.Contacts.Text);

                    int numberOfUpdatedAdvertisements = command.ExecuteNonQuery();

                    if (numberOfUpdatedAdvertisements != 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                        throw new RepositoryHasNotThisItemException();
                    }
                }
                catch (RepositoryHasNotThisItemException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        ILog log = LogManager.GetLogger(typeof(SqlRepository));
    }
}