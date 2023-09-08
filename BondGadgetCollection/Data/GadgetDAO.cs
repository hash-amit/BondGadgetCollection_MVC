using BondGadgetCollection.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BondGadgetCollection.Data
{
    internal class GadgetDAO
    {
        private string con_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BondGadgetDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Performs all operations on the database. get all, create, delete, edit, get one, search etc.
        public List<GadgetModel> FetchAll()
        {
            List<GadgetModel> returnList = new List<GadgetModel>();

            // access the database
            using (SqlConnection connection = new SqlConnection(con_string))
            {
                string sqlQuery = "Select * from dbo.Gadgets";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        // Create a new gadget object. Add it to the list to return.
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(1);
                        gadget.Description = reader.GetString(2);
                        gadget.AppearsIn = reader.GetString(3);
                        gadget.WithThisActor = reader.GetString(4);

                        returnList.Add(gadget);
                    }
                }
            }

            return returnList;
        }

        public GadgetModel FetchOne(int id)
        {
            // Create a new gadget object. Add it to the details to return.
            GadgetModel gadget = new GadgetModel();

            // access the database
            using (SqlConnection connection = new SqlConnection(con_string))
            {
                string sqlQuery = "Select * from dbo.Gadgets where Id = @id";
                
                // associate @id with id parameter

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id)); is same as given below line
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(1);
                        gadget.Description = reader.GetString(2);
                        gadget.AppearsIn = reader.GetString(3);
                        gadget.WithThisActor = reader.GetString(4);
                    }
                }
            }
            return gadget;
        }


        // Create new
        public void CreateOrUpdate(GadgetModel gadgetModel)
        {
            // If gadgetmodel.id <= 1 then create

            // If gadgetmodel.id > 1 then update is meant

            // Create a new gadget object. Add it to the details to return.
            GadgetModel gadget = new GadgetModel();

            // access the database
            using (SqlConnection connection = new SqlConnection(con_string))
            {
                string sqlQuery = "";
                if (gadgetModel.Id <= 0)
                {
                    sqlQuery = "INSERT INTO dbo.Gadgets Values (@Name,@Description,@AppearsIn,@WithThisActor)";
                }
                else
                {
                    // Update
                    sqlQuery = "UPDATE dbo.Gadgets SET Name = @Name, Description = @Description, AppearsIn = @AppearsIn, WithThisActor = @WithThisActor WHERE ID = @Id";
                }
                    

                // associate @id with id parameter

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id)); is same as given below line
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 1000).Value = gadgetModel.Id;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar,1000).Value = gadgetModel.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar,1000).Value = gadgetModel.Description;
                cmd.Parameters.Add("@AppearsIn", SqlDbType.VarChar,1000).Value = gadgetModel.AppearsIn;
                cmd.Parameters.Add("@WithThisActor", SqlDbType.VarChar,1000).Value = gadgetModel.WithThisActor;

                connection.Open();
                /*int newID = */cmd.ExecuteNonQuery();
                //return newID;
            }
        }

        internal void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(con_string))
            {
                string sqlQuery = "DELETE FROM dbo.Gadgets WHERE ID = @Id";

                // associate @id with id parameter

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                GadgetModel gadgetModel = new GadgetModel();
                //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id)); is same as given below line
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 1000).Value = id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }

        }

        internal List<GadgetModel> SearchForName(string searchPhrase)
        {
            List<GadgetModel> returnList = new List<GadgetModel>();

            // access the database
            using (SqlConnection connection = new SqlConnection(con_string))
            {
                string sqlQuery = "SELECT * FROM dbo.Gadgets WHERE NAME LIKE @searchForMe";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.Add("@searchForMe", SqlDbType.NVarChar).Value = "%"+searchPhrase+"%";

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Create a new gadget object. Add it to the list to return.
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(1);
                        gadget.Description = reader.GetString(2);
                        gadget.AppearsIn = reader.GetString(3);
                        gadget.WithThisActor = reader.GetString(4);

                        returnList.Add(gadget);
                    }
                }
            }

            return returnList;
        }
    }
}