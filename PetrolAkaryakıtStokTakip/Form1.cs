using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PetrolAkaryakıtStokTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbPetrolStokTakip;Integrated Security=True");
        
        void BenzinListe()
        {
            //KURSUNUSUZ95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLBENZIN where PETROLTUR='Kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                labelKURSUNUSUZ95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblKurşunsuz95.Text = dr[4].ToString();

            }
            baglanti.Close();

            //KURSUNUSUZ97
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='Kurşunsuz97'", baglanti);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                labelKurşunsuz97.Text = dr1[3].ToString();
                progressBar2.Value = int.Parse(dr1[4].ToString());
                lblKurşunsuz97.Text = dr1[4].ToString();

            }
            baglanti.Close();

            //EuroDizel10
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='EuroDizel10'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                EuroDizel10.Text = dr2[3].ToString();
                progressBar3.Value = int.Parse(dr2[4].ToString());
                lblEuroDizel10.Text = dr2[4].ToString();

            }
            baglanti.Close();

            //YeniProDizel
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='YeniProDizel'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                YeniProDizel.Text = dr3[3].ToString();
                progressBar4.Value = int.Parse(dr3[4].ToString());
                lblYeniProDizel.Text = dr3[4].ToString();

            }
            baglanti.Close();

            //Gaz
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='Gaz'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                labelGaz.Text = dr4[3].ToString();
                progressBar5.Value = int.Parse(dr4[4].ToString());
                lblGaz.Text = dr4[4].ToString();

            }
            baglanti.Close();


            //kasa oran
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select * from TBLKASA", baglanti);
            SqlDataReader dr5=komut5.ExecuteReader();
            while (dr5.Read())
            {
                labelKASA.Text= dr5[0].ToString();  
            }
            baglanti.Close (); 
        }
       

       
        
        private void Form1_Load(object sender, EventArgs e)
        {
            BenzinListe();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double Kursunsuz95, litre, tutar;
            Kursunsuz95=Convert.ToDouble(labelKURSUNUSUZ95.Text);
            litre=Convert.ToDouble(numericUpDown1.Value);
            tutar = Kursunsuz95 * litre;
            textBoxKurşunsuz95.Text=tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double Kursunsuz97, litre, tutar;
            Kursunsuz97 = Convert.ToDouble(labelKurşunsuz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = Kursunsuz97 * litre;
            textBoxKurşunsuz97.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double euroDizel10, litre, tutar;
            euroDizel10 = Convert.ToDouble(EuroDizel10.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = euroDizel10 * litre;
            textBoxEuroDizel10.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double yeniProDizel, litre, tutar;
            yeniProDizel = Convert.ToDouble(YeniProDizel.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = yeniProDizel * litre;
            textBoxYeniProDizel.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(labelGaz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = gaz * litre;
            textBoxGaz.Text = tutar.ToString();
        }

        private void btnDepoDoldur_Click(object sender, EventArgs e)
        {
            // Kurşunsuz95
            if (numericUpDown1.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) values (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@P1", textBoxPlaka.Text);
                komut.Parameters.AddWithValue("@P2", "Kurşunsuz95");
                komut.Parameters.AddWithValue("@P3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@P4", decimal.Parse(textBoxKurşunsuz95.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@P1", baglanti);
                komut2.Parameters.AddWithValue("@P1", decimal.Parse(textBoxKurşunsuz95.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK-@P1 where PETROLTUR='Kurşunsuz95'", baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            // Kurşunsuz97
            if (numericUpDown2.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) values (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@P1", textBoxPlaka.Text);
                komut.Parameters.AddWithValue("@P2", "Kurşunsuz97");
                komut.Parameters.AddWithValue("@P3", numericUpDown2.Value);
                komut.Parameters.AddWithValue("@P4", decimal.Parse(textBoxKurşunsuz97.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@P1", baglanti);
                komut2.Parameters.AddWithValue("@P1", decimal.Parse(textBoxKurşunsuz97.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK-@P1 where PETROLTUR='Kurşunsuz97'", baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown2.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            // EuroDizel10
            if (numericUpDown3.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) values (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@P1", textBoxPlaka.Text);
                komut.Parameters.AddWithValue("@P2", "EuroDizel10");
                komut.Parameters.AddWithValue("@P3", numericUpDown3.Value);
                komut.Parameters.AddWithValue("@P4", decimal.Parse(textBoxEuroDizel10.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@P1", baglanti);
                komut2.Parameters.AddWithValue("@P1", decimal.Parse(textBoxEuroDizel10.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK-@P1 where PETROLTUR='EuroDizel10'", baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown3.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            // YeniProDizel
            if (numericUpDown4.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) values (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@P1", textBoxPlaka.Text);
                komut.Parameters.AddWithValue("@P2", "YeniProDizel");
                komut.Parameters.AddWithValue("@P3", numericUpDown4.Value);
                komut.Parameters.AddWithValue("@P4", decimal.Parse(textBoxYeniProDizel.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@P1", baglanti);
                komut2.Parameters.AddWithValue("@P1", decimal.Parse(textBoxYeniProDizel.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK-@P1 where PETROLTUR='YeniProDizel'", baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown4.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            // Gaz
            if (numericUpDown5.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) values (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@P1", textBoxPlaka.Text);
                komut.Parameters.AddWithValue("@P2", "Gaz");
                komut.Parameters.AddWithValue("@P3", numericUpDown5.Value);
                komut.Parameters.AddWithValue("@P4", decimal.Parse(textBoxGaz.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@P1", baglanti);
                komut2.Parameters.AddWithValue("@P1", decimal.Parse(textBoxGaz.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK-@P1 where PETROLTUR='Gaz'", baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown5.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            MessageBox.Show("Satış yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BenzinListe();  // Verileri güncelle
        }

        private void buttonBenzınekle_Click(object sender, EventArgs e)
        {
            // Kurşunsuz95
            if (numericUpDown1.Value > 0) // Eğer eklemek için değer varsa
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLBENZIN set STOK=STOK+@P1 where PETROLTUR='Kurşunsuz95'", baglanti);
                komut.Parameters.AddWithValue("@P1", numericUpDown1.Value); // Kullanıcının girdiği miktarı ekle
                komut.ExecuteNonQuery();

                // Kurşunsuz95 için kasadan maliyet düş
                SqlCommand komutKasa = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@P1", baglanti);
                komutKasa.Parameters.AddWithValue("@P1", numericUpDown1.Value * decimal.Parse(textBoxKurşunsuz95.Text)); // Miktar * Fiyat
                komutKasa.ExecuteNonQuery();
                baglanti.Close();
            }

            // Kurşunsuz97 
            if (numericUpDown2.Value > 0)
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLBENZIN set STOK=STOK+@P1 where PETROLTUR='Kurşunsuz97'", baglanti);
                komut2.Parameters.AddWithValue("@P1", numericUpDown2.Value);
                komut2.ExecuteNonQuery();

                // Kurşunsuz97 için kasadan maliyet düş
                SqlCommand komutKasa2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@P1", baglanti);
                komutKasa2.Parameters.AddWithValue("@P1", numericUpDown2.Value * decimal.Parse(textBoxKurşunsuz97.Text));
                komutKasa2.ExecuteNonQuery();
                baglanti.Close();
            }

            // EuroDizel10
            if (numericUpDown3.Value > 0)
            {
                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK+@P1 where PETROLTUR='EuroDizel10'", baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown3.Value);
                komut3.ExecuteNonQuery();

                // EuroDizel10 için kasadan maliyet düş
                SqlCommand komutKasa3 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@P1", baglanti);
                komutKasa3.Parameters.AddWithValue("@P1", numericUpDown3.Value * decimal.Parse(textBoxEuroDizel10.Text));
                komutKasa3.ExecuteNonQuery();
                baglanti.Close();
            }

            // YeniProDizel
            if (numericUpDown4.Value > 0)
            {
                baglanti.Open();
                SqlCommand komut4 = new SqlCommand("update TBLBENZIN set STOK=STOK+@P1 where PETROLTUR='YeniProDizel'", baglanti);
                komut4.Parameters.AddWithValue("@P1", numericUpDown4.Value);
                komut4.ExecuteNonQuery();

                // YeniProDizel için kasadan maliyet düş
                SqlCommand komutKasa4 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@P1", baglanti);
                komutKasa4.Parameters.AddWithValue("@P1", numericUpDown4.Value * decimal.Parse(textBoxYeniProDizel.Text));
                komutKasa4.ExecuteNonQuery();
                baglanti.Close();
            }

            // Gaz
            if (numericUpDown5.Value > 0)
            {
                baglanti.Open();
                SqlCommand komut5 = new SqlCommand("update TBLBENZIN set STOK=STOK+@P1 where PETROLTUR='Gaz'", baglanti);
                komut5.Parameters.AddWithValue("@P1", numericUpDown5.Value);
                komut5.ExecuteNonQuery();

                // Gaz için kasadan maliyet düş
                SqlCommand komutKasa5 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@P1", baglanti);
                komutKasa5.Parameters.AddWithValue("@P1", numericUpDown5.Value * decimal.Parse(textBoxGaz.Text));
                komutKasa5.ExecuteNonQuery();
                baglanti.Close();
            }

            MessageBox.Show("Depo başarılı bir şekilde dolduruldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Stokları güncelle
            BenzinListe();
        }

    }
}

