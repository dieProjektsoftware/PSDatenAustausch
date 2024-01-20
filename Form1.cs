using System.Net;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Web;



namespace PSDatenAustausch;

public partial class Form1 : Form
{
    // Hier wird entschieden ob wir mit einem Server oder einem Client arbeiten
    // Der Server holt die Daten von einem FTP Server und schreibt diese in ein Lokales Verzeichnis
    // und löscht danach die Dateien auf dem FTP Server
    // Der Client schreibt die Dateien von einem lokalen Verzeichnis zum FTP Server
    // und löscht danach die Lokalen Dateien

    readonly bool server_ = (ConfigurationManager.AppSettings["PSFileServer"] == "JA");
    readonly bool client_ = (ConfigurationManager.AppSettings["PSFileClient"] == "JA");
    

    FTP fTP = new();







    public Form1()
    {
        InitializeComponent();

        fTP._user = "492073-PSfiles";
        fTP._password = "gkXbs9tcVs<u";
        fTP._url = "ftp://ftp.risikobeurteilung.at-web.biz";
        fTP._localDestinDir = ConfigurationManager.AppSettings["Path"];

    }

    private void _Start()
    {

        if (server_ == true & client_ == false)
        {
            // ich bin ein Server

            Info("Ich bin ein Server.");
            labelInfo.Text = "SERVER";
            labelInfo.ForeColor = Color.Blue;
            timer1.Interval = 1000;
            timer1.Enabled = true;

        }
        else if (server_ == false & client_ == true)
        {
            // Ich bin ein client
            Info("Ich bin ein Client");
            labelInfo.Text = "CLIENT";
            labelInfo.ForeColor = Color.Green;
            timer1.Interval = 1000;
            timer1.Enabled = true;


        }
        else
        {
            // ich bin nix gescheites
            Info("Ich bin nix kein Server kein Client!");
            labelInfo.Text = "NIX";
            labelInfo.ForeColor = Color.Red;
        }

    }

    private void Form1_Load(object sender, EventArgs e)
    {
        txt_Pfad.Text = fTP._localDestinDir;

        if ((ConfigurationManager.AppSettings["StartAutomatic"] == "JA"))
        {
            _Start();
        }


    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        if (server_) { Server(); }

        if (client_) { Client(); }

        rB_1.Checked = !rB_1.Checked;
    }

    private void Info(string Wert)
    {
        txt_Info.Text = txt_Info.Text + Wert + Environment.NewLine;

    }

    private void Server()
    {
        timer1.Enabled = false;


        var Pfad = txt_Pfad.Text;
        timer1.Enabled = false;

        string[] list_ = fTP.GetFileList();

        string InfoErr = "";
        if (list_ != null)
        {
            if (fTP.Download_(list_[0],ref InfoErr))
            {
                Info("Downl.:" + list_[0]);
                if (fTP.DelFile(list_[0], ref InfoErr))
                {
                    Info("Downl.:" + list_[0]);
                }
                
            }
            else
            {
                Info("DownErr.:" + InfoErr);
            }
            

        }


        timer1.Enabled = true;
    }

    private void Client()
    {
        // Der Client  lädt die Dateien auf den FTP Server und löscht diese lokal
        // es werden alle Dateien unabhängig von Format und Größe hochgeladen

        //timer1.Enabled = false;
        //fTP._user = "492073-PSfiles";
        //fTP._password = "gkXbs9tcVs<u";
        //fTP._url = "ftp://ftp.risikobeurteilung.at-web.biz";



        var Pfad = txt_Pfad.Text;


        // string[] list_ = fTP.GetFileList();



        //1. Versuch
        if (txt_Pfad.Text != "")
        {
            System.IO.DirectoryInfo Verzeichnis = new System.IO.DirectoryInfo(Pfad);

            foreach (System.IO.FileInfo File in Verzeichnis.GetFiles())
            {
                //dirLeer = false;
                Info(File.Name); // + "Länge: " + File.Name.Length;
                string sBack = "";
                if (fTP.FtpUpload( File.Name, ref sBack))
                {
                    FileInfo file_ = new FileInfo(Pfad + "\\" + File.Name);
                    file_.Delete();
                    Info("Upload:" + sBack);
                }
                {
                    Info(sBack + " ERR!");
                }

            }
            //}
        }
        else
        {
            Info("Bitte geben Sie einen Pfad ein.");
            //txt_Pfad.Focus();
        }

        timer1.Enabled = true;


    }

    private void label2_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Die Software kann als Server oder als Client ausgeführt werden. Die Einstellung erfolgt über die [PSDatenAustausch.dll.config]Datei.\n" +
                        "### Client ###\n" +
                        "Der Client lädt die Dateien aus einem lokalen Verzeichnis auf einen FTP Server und löscht danach die lokale Datei.\n" +
                        "### Server ###\n" +
                        "Der Server Lädt die Dateien von einem FTP Server und speichert diese lokal ab. Nach dem Download wird die Datei auf dem FTP Server gelöscht."
                        , "How to Use");
    }

    private void btn_Start_Click(object sender, EventArgs e)
    {
        _Start();
    }
}

