﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sms
{
    public partial class mark : UserControl
    {
        string p;
        string ms;
        int to, tt, cmp, qu,ass;
        int i;
        int n;
        string pid;
        int k=0;
        string sqlcon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ELDHOS AJI\source\repos\university_project-master\UP\UP\university_project\sms\sms\db.mdf"";Integrated Security=True";

        public mark()
        {
            InitializeComponent();
        }

        private void Section_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sub_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                n = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT studentID,studentName FROM student WHERE ssection='" + Section.Text + "' AND ssem='" + Semester.Text + "'", con);
                using (SqlDataReader d = cmd.ExecuteReader())
                {
                    
                    while (d.Read())
                    {
                        i++;
                        n = dg.Rows.Add();
                        dg.Rows[n].Cells[0].Value = d["studentID"].ToString();
                        dg.Rows[n].Cells[1].Value = d["studentName"].ToString();
                        dg.Rows[n].Cells[2].Value = 0;
                        dg.Rows[n].Cells[3].Value = 0;
                        dg.Rows[n].Cells[4].Value = 0;
                        dg.Rows[n].Cells[5].Value = 0;
                        dg.Rows[n].Cells[6].Value = 0;
                        
                    }
                }
                con.Close();
                //  SqlDataAdapter sd = new SqlDataAdapter("SELECT studentID,studentName FROM student WHERE ssection='" + Section.Text + "'", con);
                //  DataTable dt = new DataTable();
                // sd.Fill(dt);
                // sid.dis = "";

            }
        }

        private void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                con.Open();
                string sql = "SELECT cname FROM cource WHERE sem='" + Semester.Text + "' AND dep='CSE'";
                SqlCommand cmd = new SqlCommand(sql, con);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        sub.Items.Add(r["cname"]).ToString();
                    }
                }
                con.Close();
            }
        }

        private void sv_Click(object sender, EventArgs e)
        {
           
            int i = 0;

            while (i <= n)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(sqlcon))
                    {
                        con.Open();
                        string sql1 = "SELECT cid FROM cource WHERE cname='" + sub.Text + "'";
                        SqlCommand cmd1 = new SqlCommand(sql1, con);
                        using (SqlDataReader r = cmd1.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                pid = r["cid"].ToString();
                            }
                        }

                        ms = pid + "m";
                    
                        string id = dg.Rows[i].Cells[0].Value.ToString();
                        sql1 = "SELECT * FROM ["+ms+"] WHERE studentID='" + id + "'";
                        SqlCommand cmd2 = new SqlCommand(sql1, con);
                        int n=cmd2.ExecuteNonQuery();

                       /* using (SqlDataReader r = cmd1.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                k++;
                            }
                        }*/



                        
                            to = int.Parse(dg.Rows[i].Cells[2].Value.ToString());
                        
                       
                            tt = int.Parse(dg.Rows[i].Cells[3].Value.ToString());
                       
                        

                       
                            cmp = int.Parse(dg.Rows[i].Cells[4].Value.ToString());
                       
                        

                       
                            qu = int.Parse(dg.Rows[i].Cells[5].Value.ToString());
                   
                      
                       
                            ass = int.Parse(dg.Rows[i].Cells[6].Value.ToString());
                      

                       
                            string sql = "INSERT INTO [" + ms + "](studentID,t1,t2,cmp,qu,assi) VALUES('" + id + "','" + to + "','" + tt + "','" + cmp + "','" + qu + "','"+ass+"')";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        //}
                        //else
                       // {
                         //   string sql = "UPDATE [" + ms + "] SET t1='" + to + "',t2='" + tt + "',cmp='" + cmp + "',qu='" + qu + "',assi='"+ass+"' WHERE studentID='"+id+"'";
                           // SqlCommand cmd = new SqlCommand(sql, con);
                           // cmd.ExecuteNonQuery();
                        //}
                        i++;
                        con.Close();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
            MessageBox.Show("Mark submited");
        }
    }
}
