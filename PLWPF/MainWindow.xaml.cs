using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL MyBl = new BLimp();
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (Areas)i;
                AreaRequest.Items.Add(newItem);
            }
            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (Types)i;
                choseTheType.Items.Add(newItem);
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewGuestGrid.Visibility = Visibility.Visible;
            StartGrid.Visibility = Visibility.Collapsed;
        }


        private void GuestButton_Checked(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Visible;
            NewGuestGrid.Visibility = Visibility.Collapsed;
        }

        private void HostButton_Checked(object sender, RoutedEventArgs e)
        {
            NewGuestGrid.Visibility = Visibility.Visible;
            StartGrid.Visibility = Visibility.Collapsed;
        }



        private bool CheckEmail(string email)
        {
            if (email.Length == 0)
                return false;
            int i;
            if (email[0] == '@')
                return false;
            for (i = 0; i < email.Length && email[i] != '@'; i++) ;
            if (i >= email.Length - 3)
                return false;
            for (i = 0; i < email.Length && email[i] != '.'; i++) ;
            if (i >= email.Length - 1)
                return false;
            return true;


            // throw new NotImplementedException();
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorMail.Text = string.Empty;
            InsertEmail.BorderBrush = ContinueToRequest.BorderBrush;
            /*if (CheckEmail(InsertEmail.Text) || InsertEmail.Text.Length == 0)
            {
                WrongEmail.Visibility = Visibility.Collapsed;
            }

            else
            {
                WrongEmail.Visibility = Visibility.Visible;
                BorderBrush = Brushes.Red;
            }*/
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ContinueToRequest_Click(object sender, RoutedEventArgs e)
        {
            bool flag=true;
            try
            {
                if (InsertID.Text.Length == 0)
                {
                    flag = false;
                    InsertID.BorderBrush = Brushes.Red;
                    errorID.Text = "שדה חובה";
                }
                else if (!Validation.IsValideID(int.Parse(InsertID.Text)))
                {
                    flag = false;
                    InsertID.BorderBrush = Brushes.Red;
                    errorID.Text = "מספר תעודת הזהות אינו תקין";

                }

            }
            catch(Exception)
            {
                flag = false;
                InsertID.BorderBrush = Brushes.Red;
                errorID.Text = "מספר תעודת הזהות אינו תקין";
            }
            if(InsertEmail.Text.Length==0)
            {
                flag = false;
                InsertEmail.BorderBrush = Brushes.Red;
                errorName.Text = "שדה חובה";
            }
            else if (!Validation.EmailIsValid(InsertEmail.Text))
            {
                flag = false;
                errorMail.Text="כתובת אימייל לא תקינה";
                InsertEmail.BorderBrush = Brushes.Red;
            }
            if (InsertPhoneNumber.Text.Length == 0)
            {
                flag = false;
                InsertPhoneNumber.BorderBrush = Brushes.Red;
                errorPhone.Text = "שדה חובה";
            }
            else if (!Validation.IsValidePhoneNumber(InsertPhoneNumber.Text))
            {
                flag = false;
                errorPhone.Text = "מספר טלפון לא תקין";
                InsertPhoneNumber.BorderBrush = Brushes.Red;
            }
            if(InsertName.Text.Length==0)
            {
                flag = false;
                InsertName.BorderBrush = Brushes.Red;
                errorName.Text = "שדה חובה";
            }
            if(InsertFamilyName.Text.Length==0)
            {
                flag = false;
                InsertFamilyName.BorderBrush = Brushes.Red;
                errorFamilyName.Text = "שדה חובה";
            }
            if (flag)
            {
                NewRequestGrid.Visibility = Visibility.Visible;
                NewGuestGrid.Visibility = Visibility.Collapsed;
            }
        }
        private bool NewGuestDetailsOK()
        {
            if (MyBl.mailGuestIsExist(InsertEmail.Text) || CheckEmail(InsertEmail.Text))
                return false;
            if (MyBl.GuestIsExist(int.Parse(InsertID.Text)))
                return true;
            return true;

        }

        private void InsertID_TextChanged(object sender, TextChangedEventArgs e)
        {
            InsertID.BorderBrush = ContinueToRequest.BorderBrush;
            errorID.Text=string.Empty;
        }

        private void InsertName_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorName.Text=string.Empty;
            InsertName.BorderBrush = ContinueToRequest.BorderBrush;

        }

        private void InsertFamilyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorFamilyName.Text = string.Empty;
            InsertFamilyName.BorderBrush = ContinueToRequest.BorderBrush;

        }

        private void InsertPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorPhone.Text = string.Empty;
            InsertPhoneNumber.BorderBrush = ContinueToRequest.BorderBrush;
        }

        private void BackToFersonalDedails_Click(object sender, RoutedEventArgs e)
        {
            NewGuestGrid.Visibility = Visibility.Visible;
            NewRequestGrid.Visibility = Visibility.Collapsed;
        }
        private bool allDetailsValid()
        {
            return true;
        }
        private void EndGuestRequest_Click(object sender, RoutedEventArgs e)
        {
            if(allDetailsValid())
            {
                Guest guest = new Guest();
               // guest.ID = int.Parse(InsertID.Text);
                guest.MailAddress = InsertEmail.Text;
                guest.PrivateName = InsertName.Text;
                guest.FamilyName = InsertFamilyName.Text;
                guest.passward = InsertPassword.Password;
                //MyBl.AddGuest(guest);
                GuestRequest request=new GuestRequest();
                request.guest = guest;
                
                MyBl.AddRequest(request);
            }

        }

        private void GuestEntery_Click(object sender, RoutedEventArgs e)
        {
            GuestEntery guest = new GuestEntery();
            guest.Show();
        }
    }
}
