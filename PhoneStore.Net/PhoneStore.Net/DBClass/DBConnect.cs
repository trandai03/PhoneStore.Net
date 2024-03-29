﻿using System.Data.SQLite;
using System.Data;
using PhoneStore.Net.Model;
using PhoneStore.Net.Controller;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SqlClient;
using PhoneStore.Net.View;

namespace PhoneStore.Net.DBClass
{
    internal class DBConnect
    {
        public class DataProvider
        {
            string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
            SQLiteConnection _con = new SQLiteConnection();
            private static DataProvider instance;
            public static DataProvider Instance
            {
                get
                {
                    if (instance == null)
                        instance = new DataProvider();
                    return DataProvider.instance;
                }
                private set { DataProvider.instance = value; }
            }

            public DataTable Sql_select(string sql_querry)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql_querry, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Console.WriteLine(reader.ToString());
                dt.Load(reader);
                _con.Close();
                return dt;
            }
            public void NhapPhieu(PHIEUNHAP phieu)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = $"INSERT INTO PHIEUNHAPs (MAPN, MAND, NGAYNHAP) VALUES ({phieu.MAPN}, '{phieu.MAND}', '{phieu.NGAYNHAP.ToString("yyyy-MM-dd HH:mm:ss")}');";

                Console.WriteLine(query);
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.ExecuteNonQuery();

                _con.Close();
            }
            public DataTable LoadDH(string sql)
            {
                
                
                    using (SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
                    {
                        _con.Open();
                        DataTable dt = new DataTable();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, _con))
                        {
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                // Add columns to DataTable based on reader schema
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                                }

                                while (reader.Read())
                                {
                                    DataRow row = dt.NewRow();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        try
                                        {
                                            // Attempt to convert datetime strings if necessary
                                            if (reader.GetFieldType(i) == typeof(string) && reader.GetName(i).ToLower().Contains("date"))
                                            {
                                                row[i] = DateTime.Parse(reader.GetValue(i).ToString());
                                            }
                                            else
                                            {
                                                row[i] = reader.GetValue(i);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            // Handle parsing errors (e.g., log the error or set a default value)
                                            row[i] = null; // Or any other appropriate default value
                                        }
                                    }
                                    dt.Rows.Add(row);
                                }
                            }
                        }
                        return dt;
                    }
                
            }
            public DataTable SearchSP(string txbSearch)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                string sql = "SELECT MASP, TENSP,GIA,SL,LOAISP,SIZE, MOTA, HINHSP FROM SANPHAMs";
                sql += " WHERE MASP LIKE @keyword";
                sql += " OR TENSP LIKE @keyword";
                sql += " OR GIA LIKE @keyword";
                sql += " OR LOAISP LIKE @keyword";
                sql += " OR SIZE LIKE @keyword";
                sql += " OR SL LIKE @keyword";
                sql += " OR MOTA LIKE @keyword";
                sql += " OR HINHSP LIKE @keyword";
                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                string keyword = string.Format("%{0}%", txbSearch);
                Console.WriteLine(keyword);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                _con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }
            public DataTable SearchNV(string txbSearch)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                string sql = "SELECT MAND, TENND, GIOITINH , NGSINH, DIACHI , SDT, MAIL, USERNAME, PASS, QTV, AVA FROM NGUOIDUNGs";
                sql += " WHERE MAND LIKE @keyword";
                sql += " OR TENND LIKE @keyword";
                sql += " OR GIOITINH LIKE @keyword";
                sql += " OR DIACHI LIKE @keyword";
                sql += " OR SDT LIKE @keyword";
                sql += " OR MAIL LIKE @keyword";
                



                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                string keyword = string.Format("%{0}%", txbSearch);
                Console.WriteLine(keyword);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                _con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }
            public DataTable SearchNH(string txbSearch)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                string sql = "SELECT TENND , GIOITINH , DIACHI , SDT  , MAIL FROM NGUOIDUNGs";
                sql += " WHERE TENND LIKE @keyword";
                sql += " OR GIOITINH LIKE @keyword";
                sql += " OR DIACHI LIKE @keyword";
                sql += " OR SDT LIKE @keyword";
                sql += " OR MAIL LIKE @keyword";


                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                string keyword = string.Format("%{0}%", txbSearch);
                Console.WriteLine(keyword);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                _con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }
            public DataTable searchDH(string txbSearch)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                string sql = "SELECT SOHD ,MAKH , NGHD , TRIGIA , KHUYENMAI    FROM HOADONs";
                sql += " WHERE SOHD LIKE @keyword";
                sql += " OR MAKH LIKE @keyword";
                sql += " OR TRIGIA LIKE @keyword";
                sql += " OR KHUYENMAI LIKE @keyword";

                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                string keyword = string.Format("%{0}%", txbSearch);
                Console.WriteLine(keyword);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                _con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }

            public DataTable FilterSP(string cxbChon)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                string sql = "SELECT MASP, TENSP,GIA,SL,LOAISP,SIZE, MOTA, HINHSP FROM SANPHAMs WHERE LOAISP = @keyword";
                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                string keyword = string.Format("{0}", cxbChon);
                Console.WriteLine(keyword);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                _con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }
            public DataTable FilterDH(int cxbChon)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                string sql = "SELECT SOHD ,MAKH , NGHD , TRIGIA , KHUYENMAI    FROM HOADONs"; ;
                if(cxbChon == 1)
                {
                     sql = "SELECT SOHD ,MAKH , NGHD , TRIGIA , KHUYENMAI    FROM HOADONs WHERE KHUYENMAI > 0";
                }
                else if(cxbChon ==2)
                {
                     sql = "SELECT SOHD ,MAKH , NGHD , TRIGIA , KHUYENMAI    FROM HOADONs WHERE KHUYENMAI = 0";

                }
                
                DataTable dt = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand(sql, _con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                string keyword = string.Format("{0}", cxbChon);
                Console.WriteLine(keyword);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                _con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                _con.Close();
                return dt;
            }
            
            public List<SANPHAM> List_SP()
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = "SELECT * FROM SANPHAMs";
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<SANPHAM> SPs = new List<SANPHAM>();
                while (reader.Read())
                {
                    SPs.Add(new SANPHAM()
                    {
                        MASP = reader.GetString(0),
                        TENSP = reader.GetString(1),
                        GIA = reader.GetInt32(2),
                        SL = reader.GetInt32(5),
                        LOAISP = reader.GetString(6),
                        SIZE = reader.GetString(7),
                    });
                }
                _con.Close();
                return SPs;
            }
            public List<CTHD> List_CTHD()
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = "SELECT * FROM CTHDs";
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<CTHD> CTHDs = new List<CTHD>();
                while (reader.Read())
                {
                    CTHDs.Add(new CTHD()
                    {
                        SOHD = reader.GetInt32(0),
                        MASP = reader.GetString(1),
                        SL = reader.GetInt32(2),
                    });
                }
                _con.Close();
                return CTHDs;
            }
            public List<HOADON> List_HD()
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = "SELECT * FROM HOADONs";
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<HOADON> HDs = new List<HOADON>();
                while (reader.Read())
                {
                    HDs.Add(new HOADON()
                    {
                        SOHD = reader.GetInt32(0),
                        NGHD = reader.GetDateTime(3),
                        TRIGIA = reader.GetInt32(4),
                    });
                }
                _con.Close();
                return HDs;
            }
            public List<PHIEUNHAP> List_PN()
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = "SELECT * FROM PHIEUNHAPs";
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<PHIEUNHAP> PNs = new List<PHIEUNHAP>();
                while (reader.Read())
                {
                    PNs.Add(new PHIEUNHAP()
                    {
                        MAPN = reader.GetInt32(0),
                        MAND = reader.GetString(1),
                        NGAYNHAP = reader.GetDateTime(2),
                    });
                }
                _con.Close();
                return PNs;
            }
            public List<KHACHHANG> List_KH()
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = "SELECT * FROM KHACHHANGs";
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<KHACHHANG> KHs = new List<KHACHHANG>();
                while (reader.Read())
                {
                    KHs.Add(new KHACHHANG()
                    {
                        MAKH = reader.GetString(0),
                        HOTEN = reader.GetString(1),
                        SDT = reader.GetString(4),
                    });
                }
                _con.Close();
                return KHs;
            }
            public List<NGUOIDUNG> List_ND()
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string query = "SELECT * FROM NGUOIDUNGs";
                SQLiteCommand cmd = new SQLiteCommand(query, _con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<NGUOIDUNG> NDs = new List<NGUOIDUNG>();
                while (reader.Read())
                {
                    NDs.Add(new NGUOIDUNG()
                    {
                        MAND = reader.GetString(0),
                        TENND = reader.GetString(1),
                        AVA = reader.GetString(10),
                    });
                }
                _con.Close();
                return NDs;
            }
            public string main_user_ava(string s)
            {
                string tmp = null; 
                foreach (NGUOIDUNG x in DBConnect.DataProvider.Instance.List_ND())
                {
                    if(x.TENND == s)
                    {
                        tmp = x.AVA; break;
                    }
                }
                return tmp;
            }
            public NGUOIDUNG checkUser(string username, string password)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string USERQUERYSTRING = @"SELECT * FROM NGUOIDUNGs WHERE USERNAME = $username AND PASS = $password";
                var command = _con.CreateCommand();
                command.CommandText = USERQUERYSTRING;
                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    NGUOIDUNG u = new NGUOIDUNG();
                    while (reader.Read())
                    {
                        try
                        {
                            u.MAND = reader.GetString(0);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.TENND = reader.GetString(1);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.NGSINH = DateTime.Parse(reader.GetString(2));
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.GIOITINH = reader.GetString(3);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.SDT = reader.GetString(4);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.DIACHI = reader.GetString(5);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.USERNAME = reader.GetString(6);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            u.PASS = reader.GetString(7);
                        }
                        catch (Exception ex) { }
                        
                        try
                        {
                            u.MAIL = reader.GetString(11);
                        }
                        catch (Exception ex) { }
                    }
                    _con.Close();
                    return u;
                }
                else
                {
                    return null;
                }
            }

            

            public bool changeUserLogin(NGUOIDUNG newU)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string USERQUERYSTRING = @"UPDATE NGUOIDUNGs SET USERNAME = $username , PASS = $pass WHERE MAND = $mand";
                var command = _con.CreateCommand();
                command.CommandText = USERQUERYSTRING;
                command.Parameters.AddWithValue("$username", newU.USERNAME);
                command.Parameters.AddWithValue("$pass", newU.PASS);
                command.Parameters.AddWithValue("$mand", newU.MAND);

                var _ = command.ExecuteNonQuery();

                return true;
            }
        }

        public static string StartupPath { get; }

        public DBConnect() { }
    }
}