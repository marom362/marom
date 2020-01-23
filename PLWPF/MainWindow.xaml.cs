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
                insertArea.Items.Add(newItem);
                
            }
            for (int i = 0; i < 5; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (Areas)i;
                areaOfUnit.Items.Add(newItem);
            }
            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (Types)i;
                insertTheType.Items.Add(newItem);
            }
            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (Types)i;
                typeOfUnit.Items.Add(newItem);
            }




            //orders.ItemsSource = MyBl.GetListOfOrders();
            //units.ItemsSource = MyBl.AllUnitsOfOneHost(1);
        }
        public List<HostingUnit> MyHostingUnits
        {
            get
            {
                return MyBl.GetListOfUnits(); //MyBl.AllUnitsOfOneHost(tempHost.HostKey);
            }
        }
        Host tempHost = new Host() { PrivateName = "Meir", HostKey = 1 };
        GuestRequest currentRequest;
        Guest currentGuest;
        HostingUnit currentUnit = new HostingUnit();
        Order order = new Order();
        Host currentHost = new Host();



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
            bool flag = true;
            try
            {
                /*if (InsertID.Text.Length == 0)
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
                else if (MyBl.GuestIsExist(int.Parse(InsertID.Text)))
                {
                    flag = false;
                    InsertID.BorderBrush = Brushes.Red;
                    errorID.Text = "מספר תעודת כבר קיים במערכת";
                }*/

            }
            catch (Exception)
            {
                flag = false;
                InsertID.BorderBrush = Brushes.Red;
                errorID.Text = "מספר תעודת הזהות אינו תקין";
            }
            if (InsertEmail.Text.Length == 0)
            {
                flag = false;
                InsertEmail.BorderBrush = Brushes.Red;
                errorMail.Text = "שדה חובה";
            }
            else if (!Validation.EmailIsValid(InsertEmail.Text))
            {
                flag = false;
                errorMail.Text = "כתובת אימייל לא תקינה";
                InsertEmail.BorderBrush = Brushes.Red;
            }
            else if (MyBl.mailGuestIsExist(InsertEmail.Text))
            {
                flag = false;
                errorMail.Text = "כתובת אימייל כבר קיימת במערכת";
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
            if (InsertName.Text.Length == 0)
            {
                flag = false;
                InsertName.BorderBrush = Brushes.Red;
                errorName.Text = "שדה חובה";
            }
            if (InsertFamilyName.Text.Length == 0)
            {
                flag = false;
                InsertFamilyName.BorderBrush = Brushes.Red;
                errorFamilyName.Text = "שדה חובה";
            }
            if(bankNumber.Text.Length==0)
            {
                flag = false;
                bankNumber.BorderBrush = Brushes.Red;
                errorBankNumber.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(bankNumber.Text);
                }
                catch(Exception)
                {
                    flag = false;
                    bankNumber.BorderBrush = Brushes.Red;
                    errorBankNumber.Text = "תווים לא תקינים";
                }
            if(bankName.Text.Length==0)
            {
                flag = false;
                bankName.BorderBrush = Brushes.Red;
                ErrorBankName.Text = "שדה חובה";
            }
            if(branchNumber.Text.Length==0)
            {
                flag = false;
                branchNumber.BorderBrush = Brushes.Red;
               ErrorBranchName.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(branchNumber.Text);
                }
                catch (Exception)
                {
                    flag = false;
                    branchNumber.BorderBrush = Brushes.Red;
                    ErrorBranchName.Text = "תווים לא תקינים";
                }
            if(branchAddress.Text.Length==0)
            {
                flag = false;
                branchAddress.BorderBrush = Brushes.Red;
                ErrorBranchAddress.Text = "שדה חובה";
            }
            if (branchCity.Text.Length == 0)
            {
                flag = false;
                branchCity.BorderBrush = Brushes.Red;
                ErrorBranchCity.Text = "שדה חובה";
            }
            if(acountNumber.Text.Length==0)
            {
                flag = false;
                acountNumber.BorderBrush = Brushes.Red;
                ErrorNumberAcount.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(acountNumber.Text);
                }
                catch
                {
                    flag = false;
                    acountNumber.BorderBrush = Brushes.Red;
                    ErrorNumberAcount.Text = "תווים לא תקינים";
                }
            if (flag)
            {
                currentGuest = new Guest();
                BankBranch branch = new BankBranch();
                //currentGuest.ID = int.Parse(InsertID.Text);
                currentGuest.PrivateName = InsertName.Text;
                currentGuest.FamilyName = InsertFamilyName.Text;
                currentGuest.MailAddress = InsertEmail.Text;
                currentGuest.PhoneNumber = InsertPhoneNumber.Text;
                currentGuest.passward = InsertPassword.Password;
                branch.BankName = bankName.Text;
                branch.BankNumber = int.Parse(bankNumber.Text);
                branch.BranchAddress = branchAddress.Text;
                branch.BranchCity = branchCity.Text;
                branch.BranchNumber = int.Parse(branchNumber.Text);
                currentGuest.BankBranchDetails = branch;
                currentGuest.BankAccountNumber = int.Parse(acountNumber.Text);
                NewRequestGrid.Visibility = Visibility.Visible;
                NewGuestGrid.Visibility = Visibility.Collapsed;
            }
        }
        private bool NewGuestDetailsOK()
        {
            if (MyBl.mailGuestIsExist(InsertEmail.Text) || CheckEmail(InsertEmail.Text))
                return false;
            //if (MyBl.GuestIsExist(int.Parse(InsertID.Text)))
            return true;
            

        }

        private void InsertID_TextChanged(object sender, TextChangedEventArgs e)
        {
            InsertID.BorderBrush = ContinueToRequest.BorderBrush;
            errorID.Text = string.Empty;
        }

        private void InsertName_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorName.Text = string.Empty;
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
            bool flag = true;
            if (insertNumOFAdults.Text.Length == 0)
            {
                flag = false;
                insertNumOFAdults.BorderBrush = Brushes.Red;
            }
            if (insertNumOFAdults.Text.Length == 0)
            {
                flag = false;
               insertNumOfChildren.BorderBrush = Brushes.Red;
            }
            return flag;
        }
        private void EndGuestRequest_Click(object sender, RoutedEventArgs e)
        {
            if (allDetailsValid())
            {
                currentRequest = new GuestRequest();
                currentRequest.guest = currentGuest;
                try
                {
                    currentRequest.Adults = int.Parse(insertNumOFAdults.Text);
                }
                catch(Exception)
                {

                }
                try
                {
                    currentRequest.Children = int.Parse(insertNumOfChildren.Text);
                }
                catch (Exception)
                {

                }
                currentRequest.Area = (Areas)insertArea.SelectedIndex;
                currentRequest.Type = (Types)insertTheType.SelectedIndex;
                try
                {
                    currentRequest.EntryDate = insertRequestDates.SelectedDates.First();
                    currentRequest.ReleaseDate = insertRequestDates.SelectedDates.Last();
                }
                catch(Exception)
                {

                }
                                //currentRequest.Pool = (Options)numberOfOption(insertPool);
                try
                {
                    MyBl.AddRequest(currentRequest);
                    //MyBl.AddGuest(currentGuest);
                    NewRequestGrid.Visibility = Visibility.Collapsed;
                    RequestGrid.Visibility = Visibility.Visible;
                    welcomeGuest.Text = currentGuest.PrivateName;
                    guestpersonalDedails.Text = currentGuest.ToString();
                    requestDetails.Text = currentRequest.ToString();
                    clearNewRequestGrid();
                    clearNewGuestGrid();
                    //currentRequest.Pool = insertPool;
                }
                catch (Exception)
                {

                }
                

            }

        }
        private int numberOfOption(CheckBox a)
        {
            if (a.Content.ToString() == "checked")
                return 0;
            if (a.Content.ToString() == "unchecked")
                return 1;
            else
                return 2;

        }

        private void GuestEntery_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LogeOutRequest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuestSignUp_Click(object sender, RoutedEventArgs e)
        {
            NewGuestGrid.Visibility = Visibility.Visible;
            StartGrid.Visibility = Visibility.Hidden;
        }
        private void HostSignIn_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Collapsed;
            HostEntranceGrid.Visibility = Visibility.Visible;
        }
        public void GuestSignIn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void HostSignUp_Click(object sender, RoutedEventArgs e)
        {
            HostSignUpGrid.Visibility = Visibility.Visible;
            StartGrid.Visibility = Visibility.Collapsed;
            WhiteGrid.Visibility = Visibility.Visible;
            tempHost.HostKey = 0;
            tempHost.FamilyName = "\0";
            tempHost.FhoneNumber = "\0";
            tempHost.MailAddress = "\0";

        }

        //SignUpHost
        private void ReturnFromSignUpHost_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Visible;
            HostSignUpGrid.Visibility = Visibility.Collapsed;
            WhiteGrid.Visibility = Visibility.Collapsed;
        }
        private void Host_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CheckEmail(HostMail.Text) || HostMail.Text.Length == 0)
                {
                    WrongEmail.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WrongEmail.Visibility = Visibility.Visible;
                }
            }
            catch (NullReferenceException)
            {
                WrongEmail = new TextBlock();
            }
        }
        private void SignUpHost_Click(object sender, RoutedEventArgs e)
        {
            //bool invalid = false;

            try
            {
                InvalidID.Visibility = Visibility.Collapsed;
                tempHost.HostKey = Int32.Parse(HostID.Text);
                //invalid = false;

            }
            catch (FormatException)
            {
                InvalidID.Visibility = Visibility.Visible;
                HostID.Clear();
                return;
            }
            try
            {
                InvalidPhone.Visibility = Visibility.Collapsed;
                int phone = Int32.Parse(HostTelephone.Text);
                tempHost.FhoneNumber = HostTelephone.Text;

            }
            catch (FormatException)
            {
                InvalidPhone.Visibility = Visibility.Visible;
                HostTelephone.Clear();
            }
            tempHost.PrivateName = HostName.Text;
            tempHost.FamilyName = HostFamilyName.Text;
            tempHost.MailAddress = HostMail.Text;
            tempHost.password = insertHostPassword.Password;
            bool added = MyBl.AddHost(tempHost);
            if (!added)
            {
                HostAlreadyExists.Visibility = Visibility.Visible;
                AlreadyExists_Host.Visibility = Visibility.Visible;
                SignUpHost.Visibility = Visibility.Collapsed;
            }
            else
            {
                currentHost = tempHost;
                orders.ItemsSource =null;
                HostSignUpGrid.Visibility = Visibility.Collapsed;
                WhiteGrid.Visibility = Visibility.Collapsed;
                updatOrAddUnit.Visibility = Visibility.Visible;

            }
        }

        
        private void WatchYourRequest_Click(object sender, RoutedEventArgs e)
        {



        }

        private void ToGuestPersonalDedails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuestlogOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentRequest = null;
            currentGuest = null;
            InsertEmail.Text = string.Empty;
            InsertName.Text = string.Empty;
            InsertID.Text = string.Empty;
            InsertFamilyName.Text = string.Empty;
            InsertPassword.Password = string.Empty;
            insertNumOFAdults.Text = string.Empty;
            insertNumOfChildren.Text = string.Empty;
            GuestGrid.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
        }

        private void HostEnter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        private void OrderDetails_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility =
                    row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }
        private void closeOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                MyBl.OrderClosed((Order)((Button)sender).DataContext);
                orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);

            }
            catch(Exception)
            {

            }
        }
        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //orders.DataContext = (Order)((Button)sender).DataContext;
                //MyBl.ChangeStatusOfOrder((Order)((Button)sender).DataContext, StatusO.MailSent);
                /*sendingEmail.Text = */MyBl.SendingMail((Order)((Button)sender).DataContext);
                //sendingEmail.Text+= ((Order)((Button)sender).DataContext).ToString();
                //((Button)sender).Visibility = Visibility.Collapsed;
                orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                //SendEmail.Visibility = Visibility.Collapsed;
                //SendEmail.IsEnabled = true;
            }
            catch(Exception )
            {

            }
            
        }

        private void addUnit_Click(object sender, RoutedEventArgs e)
        {
            updatOrAddUnit.Visibility = Visibility.Visible;
            hostAcount.Visibility = Visibility.Collapsed;
            AddUnit.Visibility = Visibility.Visible;
            currentUnit = new HostingUnit();
            updatUnit.Visibility = Visibility.Collapsed;
            nameOfUnit.Text = string.Empty;
            Maxguests.Text = string.Empty;
            isPool.IsChecked = false;
            isAttractions.IsChecked = false;
            isJacuzz.IsChecked = false;
            isGarden.IsChecked = false;
            areaOfUnit.SelectedIndex = 0;
            typeOfUnit.SelectedIndex = 0;
        }

        private void HostTrySignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = int.Parse(insertYourKey.Text);
                units.ItemsSource= MyBl.AllUnitsOfOneHost(i);
                orders.ItemsSource = MyBl.GetAllOrdersOfHost(i);
                Host host = MyBl.GetHost(i);
                if (hostEnteryPassword.Password==host.password)
                {
                    hostAcount.Visibility = Visibility.Visible;
                    HostEntranceGrid.Visibility = Visibility.Collapsed;
                    StartGrid.Visibility = Visibility.Collapsed;
                    currentHost = host;
                }
                else
                {
                    errorHostKey.Text = "הסיסמא שגויה";
                    errorHostKey.Visibility = Visibility.Visible;
                }
                    


            }
            catch(Exception)
            {
                errorHostKey.Text = "מספר אורח לא קיים";
                errorHostKey.Visibility = Visibility.Visible;
            }
        }

        private void InsertYourKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorHostKey.Text = string.Empty;
        }

        private void HostLogOut_Click(object sender, RoutedEventArgs e)
        {
            hostAcount.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
            insertYourKey.Text = string.Empty;
            hostEnteryPassword.Password = string.Empty;
            currentHost = null;
        }
        private void DelUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MyBl.DelUnit((HostingUnit)((Button)sender).DataContext))
                {
                    units.ItemsSource = MyBl.AllUnitsOfOneHost(currentHost.HostKey);
                    orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                }

                else
                {
                    canNotBeDeletedGrid.Visibility = Visibility.Visible;
                    CanNotBeDeleted.Text = "אין אפשרות למחוק יחידה זו";
                }
            }
            catch(Exception)
            {
                canNotBeDeletedGrid.Visibility = Visibility.Visible;
                CanNotBeDeleted.Text = "you can't delete this unit";
            }
            
        }

        private void BackToUserHost_Click(object sender, RoutedEventArgs e)
        {
            updatOrAddUnit.Visibility = Visibility.Collapsed;
            hostAcount.Visibility = Visibility.Visible;
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckUnitDetails())
                {
                    MyBl.AddHostingUnit(currentUnit);
                    units.ItemsSource = MyBl.AllUnitsOfOneHost(currentHost.HostKey);
                    orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                    updatOrAddUnit.Visibility = Visibility.Collapsed;
                    hostAcount.Visibility = Visibility.Visible;
                }
            }
            catch(Exception)
            {

            }
                
            


        }
        private bool CheckUnitDetails()
        {
            bool flag = true;
            if (nameOfUnit.Text == string.Empty)
            {
                nameOfUnit.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (Maxguests.Text == string.Empty)
            {
                Maxguests.BorderBrush = Brushes.Red;
                flag = false;
            }
            try
            {
                int maxG = int.Parse(Maxguests.Text);
            }
            catch (Exception)
            {
                Maxguests.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (flag)
            {
                currentUnit.Area = (Areas)areaOfUnit.SelectedIndex;
                currentUnit.Type = (Types)typeOfUnit.SelectedIndex;
                currentUnit.Pool = isPool.IsChecked.Value;
                currentUnit.Jacuzz = isJacuzz.IsChecked.Value;
                currentUnit.Garden = isGarden.IsChecked.Value;
                currentUnit.ChildrenAtraction = isAttractions.IsChecked.Value;
                try
                {
                    currentUnit.numOfMaxGuests = int.Parse(Maxguests.Text);
                }
                catch (Exception)
                {
                    Maxguests.BorderBrush = Brushes.Red;
                    return false;
                }
                currentUnit.HostingUnitName = nameOfUnit.Text;
                currentUnit.Owner = currentHost;
                
            }
            return flag;
        }
        private void UpdatUnit_Click(object sender, RoutedEventArgs e)
        {
            currentUnit = (HostingUnit)((Button)sender).DataContext;
            nameOfUnit.Text = currentUnit.HostingUnitName;
            Maxguests.Text = currentUnit.numOfMaxGuests.ToString();
            isPool.IsChecked = currentUnit.Pool;
            isJacuzz.IsChecked = currentUnit.Jacuzz;
            isGarden.IsChecked = currentUnit.Garden;
            isAttractions.IsChecked = currentUnit.ChildrenAtraction;
            updatOrAddUnit.Visibility = Visibility.Visible;
            hostAcount.Visibility = Visibility.Collapsed;
            AddUnit.Visibility = Visibility.Collapsed;
            updatUnit.Visibility = Visibility.Visible;

            /*try
            {
                if (MyBl.Unit((HostingUnit)((Button)sender).DataContext))
                {
                    units.ItemsSource = MyBl.AllUnitsOfOneHost(currentHost.HostKey);
                    orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                }

                else
                {
                    canNotBeDeletedGrid.Visibility = Visibility.Visible;
                    CanNotBeDeleted.Text = "אין אפשרות למחוק יחידה זו";
                }
            }
            catch (Exception)
            {
                canNotBeDeletedGrid.Visibility = Visibility.Visible;
                CanNotBeDeleted.Text = "you can't delete this unit";
            }*/

        }

        private void OKNoDelet_Click(object sender, RoutedEventArgs e)
        {
            canNotBeDeletedGrid.Visibility = Visibility.Collapsed;
        }

        private void updatUnit_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckUnitDetails())
                {
                    MyBl.UpdateHostingUnit(currentUnit);
                    updatOrAddUnit.Visibility = Visibility.Collapsed;
                    hostAcount.Visibility = Visibility.Visible;
                    orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                    units.ItemsSource = MyBl.AllUnitsOfOneHost(currentHost.HostKey);
                    currentUnit = new HostingUnit();
                }

            }
            catch(Exception)
            {

            }
        }

        private void BackToStartGuest_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Visible;
            NewGuestGrid.Visibility = Visibility.Collapsed;
            clearNewGuestGrid();
            clearNewRequestGrid();


        }
        private void clearNewGuestGrid()
        {
            InsertName.Text = "";
            InsertName.BorderBrush = ContinueToRequest.BorderBrush;
            errorName.Text = "";
            InsertFamilyName.Text = "";
            InsertFamilyName.BorderBrush = ContinueToRequest.BorderBrush;
            errorFamilyName.Text = "";
            InsertPhoneNumber.Text = "";
            InsertPhoneNumber.BorderBrush = ContinueToRequest.BorderBrush;
            errorPhone.Text = "";
            InsertEmail.Text = "";
            InsertEmail.BorderBrush = ContinueToRequest.BorderBrush;
            errorMail.Text = "";
            InsertID.Text = "";
            InsertPassword.Password = "";
            bankNumber.Text = "";
            bankNumber.BorderBrush = ContinueToRequest.BorderBrush;
            errorBankNumber.Text = "";
            bankName.Text = "";
            bankName.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBankName.Text = "";
            branchNumber.Text = "";
            branchNumber.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBranchName.Text = "";
            branchAddress.Text = "";
            branchAddress.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBranchAddress.Text = "";
            branchCity.Text = "";
            branchCity.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBranchCity.Text = "";
            acountNumber.Text = "";
            acountNumber.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorNumberAcount.Text = "";
        }
        private void clearNewRequestGrid()
        {
            insertArea.SelectedIndex = 0;
            insertArea.SelectedIndex = 0;
            insertNumOFAdults.Text = "";
            insertNumOfChildren.Text = "";
            insertRequestDates.SelectedDates.Clear();
            
        }

        private void BankNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            bankNumber.BorderBrush = ContinueToRequest.BorderBrush;
            errorBankNumber.Text = "";
        }

        private void BankName_TextChanged(object sender, TextChangedEventArgs e)
        {
            bankName.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBankName.Text = "";
        }

        private void BranchNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            branchNumber.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBranchName.Text = "";
        }

        private void BranchAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            branchAddress.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBranchAddress.Text = "";
        }

        private void BranchCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            branchCity.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorBranchCity.Text = "";
        }

        private void AcountNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            acountNumber.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorNumberAcount.Text = "";
        }
    }
}

