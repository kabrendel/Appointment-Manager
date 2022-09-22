using System;
using System.Globalization;
using System.Windows.Forms;

namespace Appointment_Scheduler
{
    public partial class Login : Form
    {
        readonly Main main;
        private readonly Repository Repo;
        public Login(Main main)
        {
            InitializeComponent();
            this.main = main;
            Repo = new Repository();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            //  Change labels for es-MX Spanish-Mexico culture.
            if (CultureInfo.CurrentCulture.Name == "es-MX")
            {
                lblUser.Text = "Usuario:";
                lblPass.Text = "Clave:";
                buttonLogin.Text = "Acceso";
                buttonExit.Text = "Salida";
                this.Text = "Acceso";
            }
            else if (CultureInfo.CurrentCulture.Name == "en-US")
            {
                //  Do nothing, default application language.
            }
            else
            {
                //  Show message that system language is not supported by the application.
                MessageBox.Show("Language not supported: " + CultureInfo.CurrentCulture.Name + "\nPlease change to English (United States), en-US, or Spanish (Mexico), es-MX.", this.Text);
            }
        }
        //  Methods
        public Tuple<bool, string> UserLogin(string user, string pass)
        {
            if (Repo.GetUserObject(user) == null)
            {
                return _ = Tuple.Create(false, "User");
            }
            else
            {
                if (Repo.GetUserPassword(user) == pass)
                {
                    Repo.SetUser(Repo.GetUserObject(user));
                    return _ = Tuple.Create(true, "Pass");
                }
                else
                {
                    return _ = Tuple.Create(false, "Pass");
                }
            }
        }
        private bool ValidateText(string name, string data)
        {
            switch (name)
            {
                case "textUser":
                    return (data.Length > 0) && (data.Length <= 50);
                case "textPass":
                    return (data.Length > 0) && (data.Length <= 50);
                default:
                    return false;
            }
        }
        //  Events
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                main.Dispose();
            }
        }
        private void TextPass_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool check = ValidateText(textBox.Name, textBox.Text);
            if (check)
            {
                if ((textUser.Text.Length > 0) && (textPass.Text.Length > 0))
                {
                    buttonLogin.Enabled = true;
                }
            }
        }
        //  Buttons
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if ((textUser.TextLength == 0) || (textPass.TextLength == 0))
            {
                if (CultureInfo.CurrentCulture.Name == "es-MX")
                {
                    MessageBox.Show("El campo no puede estar en blanco.", this.Text);
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    MessageBox.Show("Field can not be blank.", this.Text);
                    this.DialogResult = DialogResult.None;
                }
                return;
            }
            Tuple<bool, string> results = UserLogin(textUser.Text, textPass.Text);
            if (results.Item1)
            {
                FileLog.Log(true, textUser.Text);
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {
                switch (results.Item2)
                {
                    case "DB":
                        if (CultureInfo.CurrentCulture.Name == "es-MX")
                        {
                            MessageBox.Show("Error al conectarse a la base de datos.", this.Text);
                            FileLog.Log(false, textUser.Text);
                            this.DialogResult = DialogResult.None;
                        }
                        else
                        {
                            MessageBox.Show("Error connecting to database.", this.Text);
                            FileLog.Log(false, textUser.Text);
                            this.DialogResult = DialogResult.None;
                        }
                        break;
                    case "User":
                        if (CultureInfo.CurrentCulture.Name == "es-MX")
                        {
                            MessageBox.Show("Nombre de usuario no encontrado en la base de datos.", this.Text);
                            FileLog.Log(false, textUser.Text);
                            this.DialogResult = DialogResult.None;
                        }
                        else
                        {
                            MessageBox.Show("User name not found in database.", this.Text);
                            FileLog.Log(false, textUser.Text);
                            this.DialogResult = DialogResult.None;
                        }
                        break;
                    case "Pass":
                        if (CultureInfo.CurrentCulture.Name == "es-MX")
                        {
                            MessageBox.Show("La contraseña no coincide para el usuario.", this.Text);
                            FileLog.Log(false, textUser.Text);
                            this.DialogResult = DialogResult.None;
                        }
                        else
                        {
                            MessageBox.Show("Password does not match for user.", this.Text);
                            FileLog.Log(false, textUser.Text);
                            this.DialogResult = DialogResult.None;
                        }
                        break;
                }
            }
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            main.Dispose();
        }
    }//  End of Class
}