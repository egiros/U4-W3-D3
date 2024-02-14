using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U4_W3_D3
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Cinema = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;   
            SqlConnection conn = new SqlConnection(Cinema);

            string selectedValue = DropDownList2.SelectedValue;

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "Insert into Prenotazioni (Nome, Cognome, SalaSud, SalaNord, SalaEst, Ridotto)" + 
                    "Values(@Nome, @Cognome, @SalaSud, @SalaNord, @SalaEst, @Ridotto)";

                command.Parameters.AddWithValue("@Nome", Nome.Text);
                command.Parameters.AddWithValue("@Cognome", Cognome.Text);
                command.Parameters.AddWithValue("@SalaNord", selectedValue == "Nord" ? true : false);
                command.Parameters.AddWithValue("@SalaEst", selectedValue == "Est" ? true : false);
                command.Parameters.AddWithValue("@SalaSud", selectedValue == "Sud" ? true : false);
                command.Parameters.AddWithValue("@Ridotto", CheckBox1.Checked);

                command.ExecuteNonQuery();

                Response.Write("Inserimento avvenuto con Successo");
            }
            catch (Exception ex) 
            {
                Response.Write(ex.Message);
                Response.Close();
            }
            finally 
            { 
                conn.Close(); 
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string Cinema = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(Cinema);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "SELECT Count(SalaNord) FROM Prenotazioni WHERE SalaNord = 1 UNION ALL SELECT Count(SalaNord) FROM Prenotazioni WHERE SalaNord = 1 AND Ridotto = 1";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    int SalaNord = reader.GetInt32(0);
                    Dettagli.InnerHtml = "Biglietti per sala Nord: " + SalaNord;

                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string Cinema = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(Cinema);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "SELECT Count(SalaEst) FROM PRENOTAZIONI";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int SalaEst = reader.GetInt32(0);
                    Dettagli.InnerHtml = "Biglietti per sala Est: " + SalaEst;

                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string Cinema = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(Cinema);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "SELECT Count(SalaSud) FROM PRENOTAZIONI";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int SalaSud = reader.GetInt32(0);
                    Dettagli.InnerHtml = "Biglietti per sala Sud: " + SalaSud;

                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }
        }
    }
}