using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        IBL MyBl = FactorySingletonBL.GetInstance;
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

            //Task.Run(() => FactorySingletonBL.GetInstance.checksExpiredOrders());
           // Task.Run(() => FactorySingletonBL.GetInstance.checksExpiredRequests());
           // Task task = Task.Run(() => Updateorders());

            //orders.ItemsSource = MyBl.GetListOfOrders();
            //units.ItemsSource = MyBl.AllUnitsOfOneHost(1);
        }
        public void  Updateorders()
        {
            try
            {
                units.ItemsSource = FactorySingletonBL.GetInstance.AllUnitsOfOneHost(currentHost.HostKey);
                orders.ItemsSource = FactorySingletonBL.GetInstance.GetAllOrdersOfHost(currentHost.HostKey);
            }
            catch
            {

            }
        }
        public List<HostingUnit> MyHostingUnits
        {
            get
            {
                return MyBl.GetListOfUnits(); //MyBl.AllUnitsOfOneHost(tempHost.HostKey);
            }
        }
        Host tempHost = new Host();
        GuestRequest currentRequest;
        Guest currentGuest;
        HostingUnit currentUnit;
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
            else if (FactorySingletonBL.GetInstance.GetGuest(InsertEmail.Text)!=null)
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
            if (bankNumber.Text.Length == 0)
            {
                flag = false;
                bankNumber.BorderBrush = Brushes.Red;
                errorBankNumber.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(bankNumber.Text);
                }
                catch (Exception)
                {
                    flag = false;
                    bankNumber.BorderBrush = Brushes.Red;
                    errorBankNumber.Text = "תווים לא תקינים";
                }
            if (bankName.Text.Length == 0)
            {
                flag = false;
                bankName.BorderBrush = Brushes.Red;
                ErrorBankName.Text = "שדה חובה";
            }
            if (branchNumber.Text.Length == 0)
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
            if (branchAddress.Text.Length == 0)
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
            if (acountNumber.Text.Length == 0)
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
        /*private void EndGuestRequest_Click(object sender, RoutedEventArgs e)
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

        }*/
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
                catch (FormatException)
                {
                    insertNumOFAdults.Clear();
                    //ErrorNumOfAdults.Visibility = Visibility.Visible;
                }
                try
                {
                    currentRequest.Children = int.Parse(insertNumOfChildren.Text);
                }
                catch (FormatException)
                {
                    //ErrorNumOfChildren.Visibility = Visibility.Visible;
                    insertNumOfChildren.Clear();
                }
                currentRequest.Area = (Areas)insertArea.SelectedIndex;
                currentRequest.Type = (Types)insertTheType.SelectedIndex;
                currentRequest.EntryDate = insertRequestDates.SelectedDates.First();
                currentRequest.ReleaseDate = insertRequestDates.SelectedDates.Last();
                //currentRequest.Pool = (Options)numberOfOption(insertPool);
                try
                {
                    FactorySingletonBL.GetInstance.AddRequest(currentRequest);
                    //MyBl.AddGuest(currentGuest);
                }
                catch (Exception)
                {

                }
                NewRequestGrid.Visibility = Visibility.Collapsed;
                UserGuest.Visibility = Visibility.Visible;
                UserGuestRequest.ItemsSource = FactorySingletonBL.GetInstance.GetListOfRequests();
                //welcomeGuest.Text = currentGuest.PrivateName;
                //guestpersonalDedails.Text = currentGuest.ToString();
                //requestDetails.Text = currentRequest.ToString();

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
            UserGuest.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
            currentGuest = null;
            currentRequest = null;
        }

        private void GuestSignUp_Click(object sender, RoutedEventArgs e)
        {
            NewGuestGrid.Visibility = Visibility.Visible;
            StartGrid.Visibility = Visibility.Collapsed;
            GuestTryIn.Visibility = Visibility.Collapsed;
        }
        private void HostSignIn_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Collapsed;
            HostEntranceGrid.Visibility = Visibility.Visible;
        }
        public void GuestSignIn_Click(object sender, RoutedEventArgs e)
        {
            //StartGrid.Visibility = Visibility.Collapsed;
            GuestTryIn.Visibility = Visibility.Visible;
            
        }
        private void HostSignUp_Click(object sender, RoutedEventArgs e)
        {
            HostSignUpGrid.Visibility = Visibility.Visible;
            StartGrid.Visibility = Visibility.Collapsed;
            WhiteGrid.Visibility = Visibility.Visible;
            HostEntranceGrid.Visibility = Visibility.Collapsed;
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
            clearHostSignUpGrid();
            HostAlreadyExists.Visibility = Visibility.Collapsed;
            AlreadyExists_Host.Visibility = Visibility.Collapsed;
            SignUpHost.Visibility = Visibility.Visible;
            InvalidID.Visibility = Visibility.Collapsed;
            InvalidPhone.Visibility = Visibility.Collapsed;
            WrongEmail.Visibility = Visibility.Collapsed;
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
                //MessageBox.Show(a.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                //WrongEmail = new TextBlock();
            }
        }
        private void SignUpHost_Click(object sender, RoutedEventArgs e)
        {
            bool isvalid = true;

            try
            {
                InvalidID.Visibility = Visibility.Collapsed;
                tempHost.HostKey = Int32.Parse(HostID.Text);
                


            }
            catch (FormatException)
            {
                InvalidID.Visibility = Visibility.Visible;
                HostID.Clear();
                isvalid = false;
                
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
                isvalid = false;

            }
            if (insertHostPassword.Password.Length==0)
            {
                isvalid = false;
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
            else if(isvalid)
            {
                currentHost = tempHost;
                orders.ItemsSource = null;
                units.ItemsSource = null;
                HostSignUpGrid.Visibility = Visibility.Collapsed;
                WhiteGrid.Visibility = Visibility.Collapsed;
                hostAcount.Visibility = Visibility.Visible;
                clearHostSignUpGrid();
                

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
            UserGuest.Visibility = Visibility.Collapsed;
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
            catch (Exception)
            {

            }
        }
        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            Order order = (Order)((Button)sender).DataContext;
            //orders.DataContext = (Order)((Button)sender).DataContext;
            //MyBl.ChangeStatusOfOrder((Order)((Button)sender).DataContext, StatusO.MailSent);
            /*sendingEmail.Text = */
            
            //SendEmail.Visibility = Visibility.Collapsed;
            //SendEmail.IsEnabled = true;
            MailMessage mail = new MailMessage();
            //Order order = (Order)((Button)sender).DataContext;
            string SiteMail = "mmm1.ppp112345@gmail.com";// BL.BL_imp.loggedInUser.Email;
            GuestRequest request = MyBl.GetListOfRequests().Where(x => x.GuestRequestKey == order.GuestRequestKey).FirstOrDefault();
            string GuestMail = request.guest.MailAddress;
            string PasswordMail = "mp1234567";//BL.BL_imp.loggedInUser.Password;

            mail.To.Add(GuestMail);
            mail.From = new MailAddress(SiteMail);
            mail.Subject = "הצעת ארוח ממערכת גן עדן";
            currentUnit=MyBl.GetUnit(order.HostingUnitKey);
            mail.Body = GetMailBody(request, currentUnit);
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(SiteMail, PasswordMail);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                MyBl.SendingMail((Order)((Button)sender).DataContext);
                //sendingEmail.Text+= ((Order)((Button)sender).DataContext).ToString();
                //((Button)sender).Visibility = Visibility.Collapsed;
                orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private string GetMailBody(GuestRequest request, HostingUnit unit)
        {
            return string.Format(@"
                    <html>
                        <body>
                            שלום {0} ,<br/>
                            נמצאה התאמה מיחידה  {1}
                        </body>
                    </html>
            ", request.guest.PrivateName + " " + request.guest.FamilyName,
            unit.HostingUnitName);
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
                Host host = MyBl.GetHost(i);
                if (hostEnteryPassword.Password == host.password)
                {
                    hostAcount.Visibility = Visibility.Visible;
                    HostEntranceGrid.Visibility = Visibility.Collapsed;
                    StartGrid.Visibility = Visibility.Collapsed;
                    currentHost = host;
                    units.ItemsSource = MyBl.AllUnitsOfOneHost(i);
                    orders.ItemsSource = MyBl.GetAllOrdersOfHost(i);
                }
                else
                {
                    errorHostKey.Text = "הסיסמא שגויה";
                    errorHostKey.Visibility = Visibility.Visible;
                }



            }
            catch (Exception)
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
                List<Order> orders = FactorySingletonBL.GetInstance.GetAllOrdersOfHost(currentHost.HostKey);
                HostingUnit unit=(HostingUnit)((Button)sender).DataContext;
                var result=MessageBox.Show("?האם אתה בטוח שברצונך למחוק יחידה זו", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result==MessageBoxResult.Yes)
                {
                    if (!MyBl.DelUnit(unit))
                    {
                        MessageBox.Show("אין אפשרות למחוק יחידה זו", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        foreach (Order order in orders)
                            if (order.HostingUnitKey == unit.HostingUnitKey)
                                FactorySingletonBL.GetInstance.DelOrder(order);
                        Updateorders();
                        
                    }
                }
                
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }

        }
        private void DelRequest_Click(object sender, RoutedEventArgs e)
        {
            GuestRequest request = (GuestRequest)((Button)sender).DataContext;
            try
            {
                FactorySingletonBL.GetInstance.DelRequest(request);
                UserGuestRequest.ItemsSource = FactorySingletonBL.GetInstance.GetRequestsOfGuest(currentGuest.MailAddress);
            }
            catch(Exception)
            {

            }
            
        }

        private void BackToUserHost_Click(object sender, RoutedEventArgs e)
        {
            updatOrAddUnit.Visibility = Visibility.Collapsed;
            hostAcount.Visibility = Visibility.Visible;
            Maxguests.BorderBrush = updatUnit.BorderBrush;
            nameOfUnit.BorderBrush = updatUnit.BorderBrush;
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
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);

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
                    Maxguests.BorderBrush = updatUnit.BorderBrush;
                    nameOfUnit.BorderBrush= updatUnit.BorderBrush;


                }

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);

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
        private void clearHostSignUpGrid()
        {
            HostID.Text = "מספר זהות";
            HostName.Text = "שם פרטי";
            HostFamilyName.Text = "שם משפחה";
            HostTelephone.Text = "מספר טלפון";
            HostMail.Text = "כתובת אי מייל";
            insertHostPassword.Clear();
        }
        private void clearNewRequestGrid()
        {
            insertArea.SelectedIndex = 0;
            insertArea.SelectedIndex = 0;
            insertNumOFAdults.Text = "";
            insertNumOfChildren.Text = "";
            insertRequestDates.SelectedDates.Clear();

        }
        private void clearUpdatGuestGrid()
        {
            updateName.BorderBrush = ContinueToRequest.BorderBrush;
            errorUpdateName.Text = "";
            updateFamilyName.BorderBrush = ContinueToRequest.BorderBrush;
            errorUpdateFamilyName.Text = "";
            updatePhoneNumber.BorderBrush = ContinueToRequest.BorderBrush;
            errorUpdatePhone.Text = "";
            updateEmail.BorderBrush = ContinueToRequest.BorderBrush;
            errorUpdateMail.Text = "";
            updatebankNumber.BorderBrush = ContinueToRequest.BorderBrush;
            errorUpdateBankNumber.Text = "";
            updatebankName.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorUpdateBankName.Text = "";
            updatebranchNumber.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorUpdateBranchName.Text = "";
            updatebranchAddress.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorUpdateBranchAddress.Text = "";
            updatebranchCity.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorUpdateBranchCity.Text = "";
            updateacountNumber.BorderBrush = ContinueToRequest.BorderBrush;
            ErrorUpdateNumberAcount.Text = "";
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
        private void Master_Click(object sender, RoutedEventArgs e)
        {
            UsernamePassword.Visibility = Visibility.Visible;
        }
        private void ReturnToTheStart_Click(object sender, RoutedEventArgs e)
        {
            ToReturnQuestion.Visibility = Visibility.Visible;
        }
        private void ReturnFromMasters_Click(object sender, RoutedEventArgs e)
        {
            MastersGrid.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
        }
        private void ReturnToMasters_Click(object sender, RoutedEventArgs e)
        {
            ToReturnQuestion.Visibility = Visibility.Collapsed;
        }
        
        private void ContinueToMasterPage_Click(object sender, RoutedEventArgs e)
        {
            if (MUsername.Text == "User" && MPassword.Password == "Password")
            {
                MastersGrid.Visibility = Visibility.Visible;
                StartGrid.Visibility = Visibility.Collapsed;
                Tables.DataContext = (Order)((Button)sender).DataContext;
                Tables.ItemsSource = FactorySingletonBL.GetInstance.GetListOfOrders();
                unitsMaster.ItemsSource = FactorySingletonBL.GetInstance.GetListOfUnits();
                requestsMaster.ItemsSource = FactorySingletonBL.GetInstance.GetListOfRequests();


            }
            else
            {
                ErrorPasswordTextBlock.Visibility = Visibility.Visible;
                MUsername.Clear();
                MPassword.Clear();
            }
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortByAreas.IsSelected == true)
            foreach (var nameGroup in FactorySingletonBL.GetInstance.GetUnitssGroupingByAreas())
            {
                if (nameGroup.Key == Areas.Center)
                    unitsMaster.ItemsSource = nameGroup;
            }
        }

        private void GuestTryInButtom_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = FactorySingletonBL.GetInstance.GetGuest(GuestEmail.Text);
            if(guest==null|| guest.passward != GuestPassword.Password)
            {
                wrongGuestPasswordOrMail.Text = "שם משתמש או סיסמא לא נכונים";
            }
            else if(guest.passward!=GuestPassword.Password)
            {

            }
            else
            {
                GuestTryIn.Visibility = Visibility.Collapsed;
                UserGuest.Visibility = Visibility.Visible;
                currentGuest = guest;
                setGuestDetails();
                UserGuestRequest.ItemsSource = FactorySingletonBL.GetInstance.GetRequestsOfGuest(GuestEmail.Text);
            }
        }

        private void BackFromGuestTryIN_Click(object sender, RoutedEventArgs e)
        {
            GuestTryIn.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
        }
        private  void setGuestDetails()
        {
            updateName.Text = currentGuest.PrivateName;
            updateFamilyName.Text = currentGuest.FamilyName;
            updateEmail.Text = currentGuest.MailAddress;
        }

        private void BackToUserGuest_Click(object sender, RoutedEventArgs e)
        {
            UpdatingGuestGrid.Visibility = Visibility.Collapsed;
            UserGuest.Visibility = Visibility.Visible;
            clearUpdatGuestGrid();

        }

        private void ToUpdateGuest_Click(object sender, RoutedEventArgs e)
        {
            setGuestDetails();
            UserGuest.Visibility = Visibility.Collapsed;
            UpdatingGuestGrid.Visibility = Visibility.Visible;



        }
        private void toUpdateRequest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetUpdateGuest_Click(object sender, RoutedEventArgs e)
        {
            clearUpdatGuestGrid();
            setGuestDetails();
        }

        private void UpdateGuest_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            
            if (updateEmail.Text.Length == 0)
            {
                flag = false;
                updateEmail.BorderBrush = Brushes.Red;
                errorUpdateMail.Text = "שדה חובה";
            }
            else if (!Validation.EmailIsValid(updateEmail.Text))
            {
                flag = false;
                errorUpdateMail.Text = "כתובת אימייל לא תקינה";
                updateEmail.BorderBrush = Brushes.Red;
            }
            else if (FactorySingletonBL.GetInstance.GetGuest(updateEmail.Text) != null)
            {
                if(FactorySingletonBL.GetInstance.GetGuest(updateEmail.Text) !=currentGuest)
                {
                    flag = false;
                    errorUpdateMail.Text = "כתובת אימייל כבר קיימת במערכת";
                    updateEmail.BorderBrush = Brushes.Red;
                }
            }
            if (updatePhoneNumber.Text.Length == 0)
            {
                flag = false;
                updatePhoneNumber.BorderBrush = Brushes.Red;
                errorUpdatePhone.Text = "שדה חובה";
            }
            else if (!Validation.IsValidePhoneNumber(updatePhoneNumber.Text))
            {
                flag = false;
                errorUpdatePhone.Text = "מספר טלפון לא תקין";
                updatePhoneNumber.BorderBrush = Brushes.Red;
            }
            if (updateName.Text.Length == 0)
            {
                flag = false;
                updateName.BorderBrush = Brushes.Red;
                errorUpdateName.Text = "שדה חובה";
            }
            if (updateFamilyName.Text.Length == 0)
            {
                flag = false;
                updateFamilyName.BorderBrush = Brushes.Red;
                errorUpdateFamilyName.Text = "שדה חובה";
            }
            if (updatebankNumber.Text.Length == 0)
            {
                flag = false;
                updatebankNumber.BorderBrush = Brushes.Red;
                errorUpdateBankNumber.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(updatebankNumber.Text);
                }
                catch (Exception)
                {
                    flag = false;
                    updatebankNumber.BorderBrush = Brushes.Red;
                    errorUpdateBankNumber.Text = "תווים לא תקינים";
                }
            if (updatebankName.Text.Length == 0)
            {
                flag = false;
                updatebankName.BorderBrush = Brushes.Red;
                ErrorUpdateBankName.Text = "שדה חובה";
            }
            if (updatebranchNumber.Text.Length == 0)
            {
                flag = false;
                updatebranchNumber.BorderBrush = Brushes.Red;
                ErrorUpdateBranchName.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(updatebranchNumber.Text);
                }
                catch (Exception)
                {
                    flag = false;
                    updatebranchNumber.BorderBrush = Brushes.Red;
                    ErrorUpdateBranchName.Text = "תווים לא תקינים";
                }
            if (updatebranchAddress.Text.Length == 0)
            {
                flag = false;
                updatebranchAddress.BorderBrush = Brushes.Red;
                ErrorUpdateBranchAddress.Text = "שדה חובה";
            }
            if (updatebranchCity.Text.Length == 0)
            {
                flag = false;
                updatebranchCity.BorderBrush = Brushes.Red;
                ErrorUpdateBranchCity.Text = "שדה חובה";
            }
            if (updateacountNumber.Text.Length == 0)
            {
                flag = false;
                updateacountNumber.BorderBrush = Brushes.Red;
                ErrorUpdateNumberAcount.Text = "שדה חובה";
            }
            else try
                {
                    int.Parse(updateacountNumber.Text);
                }
                catch
                {
                    flag = false;
                    updateacountNumber.BorderBrush = Brushes.Red;
                    ErrorUpdateNumberAcount.Text = "תווים לא תקינים";
                }
            if (flag)
            {
                string mail = currentGuest.MailAddress;
                BankBranch branch = new BankBranch();
                //currentGuest.ID = int.Parse(InsertID.Text);
                currentGuest.PrivateName = updateName.Text;
                currentGuest.FamilyName = updateFamilyName.Text;
                currentGuest.MailAddress = updateEmail.Text;
                currentGuest.PhoneNumber = updatePhoneNumber.Text;
                currentGuest.passward = updatePassword.Password;
                branch.BankName = updatebankName.Text;
                branch.BankNumber = int.Parse(updatebankNumber.Text);
                branch.BranchAddress = updatebranchAddress.Text;
                branch.BranchCity = updatebranchCity.Text;
                branch.BranchNumber = int.Parse(updatebranchNumber.Text);
                currentGuest.BankBranchDetails = branch;
                currentGuest.BankAccountNumber = int.Parse(updateacountNumber.Text);
                UserGuest.Visibility = Visibility.Visible;
                UpdatingGuestGrid.Visibility = Visibility.Collapsed;
                foreach(GuestRequest request in FactorySingletonBL.GetInstance.GetRequestsOfGuest(mail))
                {
                    request.guest = currentGuest;
                    FactorySingletonBL.GetInstance.UpdateRequest(request);
                }
            }
                
        }

        private void BackFromHostEntery_Click(object sender, RoutedEventArgs e)
        {
            HostEntranceGrid.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
        }

        private void HostID_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HostID.Clear();
        }

        private void HostName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HostName.Clear();
        }

        private void HostFamilyName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HostFamilyName.Clear();
        }

        private void HostTelephone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HostTelephone.Clear();
        }

        private void HostMail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HostMail.Clear();
        }

        private void HostAcount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Maxguests_TextChanged(object sender, TextChangedEventArgs e)
        {
            Maxguests.BorderBrush = updatUnit.BorderBrush;
        }

        private void NameOfUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameOfUnit.BorderBrush = updatUnit.BorderBrush;
        }
    }
    
    

}

