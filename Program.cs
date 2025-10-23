using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;


namespace PatientDBProject
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            var repo = new VeriTabaniIslemleri();
            var oturum = new Oturum();

          
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(repo, oturum));
          
        }
       
        }


    }