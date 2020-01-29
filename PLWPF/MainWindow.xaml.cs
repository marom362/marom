using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
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
            //Task.Run(() => MyBl.checksExpiredOrders());
            //Task.Run(() => MyBl.checksExpiredRequests());
            //Task.Run(() => Updateorders());
            InitializeComponent();
            hostBranchAcountCmb.ItemsSource= MyBl.getListOfBankBranches();
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
            new Thread(() =>
            {
                do
                {
                    MyBl.checksExpiredOrders();
                    var orders = MyBl.GetListOfOrders();
                    MyBl.checksExpiredRequests();
                    /*foreach (var item in orders)
                        if(item.Status = StatusO.MailSent && item.OrderDate >item.OrderDate.AddDays(30))
                        {

                        }*/
                    Thread.Sleep(100 * 60 * 60 * 24);
                } while (true);
            }).Start();

            //Task.Run(() => MyBl.checksExpiredOrders());
            //Task.Run(() => MyBl.checksExpiredRequests());
            //Task.Run(() => Updateorders());

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
        public List<BankBranch> BranchsList
        {
            get
            {
                return MyBl.getListOfBankBranches(); 
                
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
            
            if (flag)
            {
                currentGuest = new Guest();
                //currentGuest.ID = int.Parse(InsertID.Text);
                currentGuest.PrivateName = InsertName.Text;
                currentGuest.FamilyName = InsertFamilyName.Text;
                currentGuest.MailAddress = InsertEmail.Text;
                currentGuest.PhoneNumber = InsertPhoneNumber.Text;
                currentGuest.passward = InsertPassword.Password;
                NewRequestGrid.Visibility = Visibility.Visible;
                NewGuestGrid.Visibility = Visibility.Collapsed;
                BackToFersonalDedails.Visibility = Visibility.Visible;
                EndGuestRequest.Visibility = Visibility.Visible;
                backToUserGuest.Visibility = Visibility.Collapsed;
                updateRequest.Visibility = Visibility.Collapsed;
                addRequest.Visibility = Visibility.Collapsed;
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
                try
                {
                    currentRequest.Area = (Areas)insertArea.SelectedIndex;
                    currentRequest.Type = (Types)insertTheType.SelectedIndex;
                    currentRequest.EntryDate = insertRequestDates.SelectedDates.First();
                    currentRequest.ReleaseDate = insertRequestDates.SelectedDates.Last();
                    if (poolN.IsChecked == true)
                        currentRequest.Pool = Options.NotIntresting;
                    else if (poolY.IsChecked == true)
                        currentRequest.Pool = Options.Must;
                    else
                        currentRequest.Pool = Options.Possible;
                    if (jacuzzN.IsChecked == true)
                        currentRequest.Jacuzz = Options.NotIntresting;
                    else if (jacuzzY.IsChecked == true)
                        currentRequest.Jacuzz = Options.Must;
                    else
                        currentRequest.Jacuzz = Options.Possible;
                    if (gardenN.IsChecked == true)
                        currentRequest.Garden = Options.NotIntresting;
                    else if (gardenY.IsChecked == true)
                        currentRequest.Garden = Options.Must;
                    else
                        currentRequest.Garden = Options.Possible;
                    if (attractionsN.IsChecked == true)
                        currentRequest.ChildrensAttractions = Options.NotIntresting;
                    else if (attractionsY.IsChecked == true)
                        currentRequest.ChildrensAttractions = Options.Must;
                    else
                        currentRequest.ChildrensAttractions = Options.Possible;



                    //currentRequest.Pool = (Options)numberOfOption(insertPool);
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
                UserGuestRequest.ItemsSource = MyBl.GetRequestsOfGuest(currentGuest.MailAddress);
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
            clearGuestTryIn();
        }
        private void HostSignIn_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Collapsed;
            HostEntranceGrid.Visibility = Visibility.Visible;
        }
        public void GuestSignIn_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Collapsed;
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
            hostBranchAcountCmb.DataContext = MyBl.getListOfBankBranches();
            clearHostEntery();

        }

        //SignUpHost
        private void ReturnFromSignUpHost_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Visible;
            HostSignUpGrid.Visibility = Visibility.Collapsed;
            WhiteGrid.Visibility = Visibility.Collapsed;
            clearHostSignUpGrid();
            
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
                currentHost = MyBl.GetHost(Int32.Parse(HostID.Text));
                HostAlreadyExists.Visibility = Visibility.Visible;
                AlreadyExists_Host.Visibility = Visibility.Visible;
                SignUpHost.Visibility = Visibility.Collapsed;
                clearNewGuestGrid();
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
        private void UnitDetails_Click(object sender, RoutedEventArgs e)
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
        private void RequestDetails_Click(object sender, RoutedEventArgs e)
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

                int num=MyBl.OrderClosed((Order)((Button)sender).DataContext);
                orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                MessageBox.Show(" עמלה בסך " + num + " שח הועברה מחשבונך בעקבות סגרת העיסקה " , "", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception)
            {

            }
        }
        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string b=MyBl.SendingMail((Order)((Button)sender).DataContext);
                orders.ItemsSource = MyBl.GetAllOrdersOfHost(currentHost.HostKey);
                MessageBox.Show(b, "", MessageBoxButton.OK,MessageBoxImage.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);

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
                Host host = MyBl.GetHost(i);
                if (hostEnteryPassword.Password == host.password)
                {
                    hostAcount.Visibility = Visibility.Visible;
                    HostEntranceGrid.Visibility = Visibility.Collapsed;
                    StartGrid.Visibility = Visibility.Collapsed;
                    currentHost = host;
                    units.ItemsSource = MyBl.AllUnitsOfOneHost(i);
                    orders.ItemsSource = MyBl.GetAllOrdersOfHost(i);
                    clearHostEntery();
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
                    //else
                    //{
                    //    foreach (Order order in orders)
                    //        if (order.HostingUnitKey == unit.HostingUnitKey)
                    //            FactorySingletonBL.GetInstance.DelOrder(order);
                    //    Updateorders();
                        
                    //}
                }
                
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }

        }
        ////private void DelRequest_Click(object sender, RoutedEventArgs e)
        //{
        //    GuestRequest request = (GuestRequest)((Button)sender).DataContext;
        //    try
        //    {
        //        FactorySingletonBL.GetInstance.DelRequest(request);
        //        UserGuestRequest.ItemsSource = FactorySingletonBL.GetInstance.GetRequestsOfGuest(currentGuest.MailAddress);
        //    }
        //    catch(Exception)
        //    {

        //    }
            
        //}
        

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
            
        }
        private void clearHostSignUpGrid()
        {
            HostID.Text = "מספר זהות";
            HostName.Text = "שם פרטי";
            HostFamilyName.Text = "שם משפחה";
            HostTelephone.Text = "מספר טלפון";
            HostMail.Text = "כתובת אי מייל";
            insertHostPassword.Clear();
            WrongEmail.Visibility = Visibility.Collapsed;
            InvalidID.Visibility = Visibility.Collapsed;
            InvalidPhone.Visibility = Visibility.Collapsed;
            HostAlreadyExists.Visibility = Visibility.Collapsed;
            AlreadyExists_Host.Visibility = Visibility.Collapsed;
            SignUpHost.Visibility = Visibility.Visible;
        }
        private void clearNewRequestGrid()
        {
            insertArea.SelectedIndex = 0;
            insertArea.SelectedIndex = 0;
            insertNumOFAdults.Text = "";
            insertNumOfChildren.Text = "";
            insertCity.Clear();
            poolN.IsChecked = false;
            poolY.IsChecked = false;
            poolP.IsChecked = false;
            gardenN.IsChecked = false;
            gardenY.IsChecked = false;
            gardenP.IsChecked = false;
            jacuzzN.IsChecked = false;
            jacuzzY.IsChecked = false;
            jacuzzP.IsChecked = false;
            attractionsN.IsChecked = false;
            attractionsY.IsChecked = false;
            attractionsP.IsChecked = false;
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
            updatePassword.BorderBrush = ContinueToRequest.BorderBrush;
            errorUpdatePassword.Text = "";
            setGuestDetails();
            
        }
        private void clearGuestTryIn()
        {
            GuestEmail.Clear();
            GuestPassword.Clear();
            wrongGuestPasswordOrMail.Text = "";
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
                wrongGuestPasswordOrMail.Text = " אימייל או סיסמא לא נכונים";
            }
            else if(guest.passward!=GuestPassword.Password)
            {

            }
            else
            {
                GuestTryIn.Visibility = Visibility.Collapsed;
                UserGuest.Visibility = Visibility.Visible;
                currentGuest = guest;
                UserGuestRequest.ItemsSource = FactorySingletonBL.GetInstance.GetRequestsOfGuest(GuestEmail.Text);
                clearGuestTryIn();
            }
        }

        private void BackFromGuestTryIN_Click(object sender, RoutedEventArgs e)
        {
            GuestTryIn.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
            clearGuestTryIn();
        }
        private  void setGuestDetails()
        {
            updateName.Text = currentGuest.PrivateName;
            updateFamilyName.Text = currentGuest.FamilyName;
            updateEmail.Text = currentGuest.MailAddress;
            updatePhoneNumber.Text = currentGuest.PhoneNumber;
            updatePassword.Password = currentGuest.passward;
        }
        private void setRequestDetails()
        {
            
            //insertArea.SelectedIndex = int.Parse((Areas)currentRequest.Area);
            //insertype.SelectedItem = currentRequest.Type;
            insertNumOFAdults.Text = currentRequest.Adults.ToString();
            insertNumOfChildren.Text = currentRequest.Children.ToString(); ;
            //insertRequestDates.SelectedDates.First(currentRequest.EntryDate) = currentRequest.EntryDate; ;
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
            EndGuestRequest.Visibility = Visibility.Collapsed;
            BackToFersonalDedails.Visibility = Visibility.Collapsed;
            addRequest.Visibility = Visibility.Collapsed;
            backToUserGuest.Visibility = Visibility.Visible;
            updateRequest.Visibility = Visibility.Visible;
            setRequestDetails();
            NewRequestGrid.Visibility = Visibility.Visible;
            UserGuest.Visibility = Visibility.Collapsed;
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
            if(updatePassword.Password.Length==0)
            {
                flag = false;
                updatePassword.BorderBrush = Brushes.Red;
                errorUpdatePassword.Text = "שדה חובה";
            }
            
            if (flag)
            {
                string mail = currentGuest.MailAddress;
                //currentGuest.ID = int.Parse(InsertID.Text);
                currentGuest.PrivateName = updateName.Text;
                currentGuest.FamilyName = updateFamilyName.Text;
                currentGuest.MailAddress = updateEmail.Text;
                currentGuest.PhoneNumber = updatePhoneNumber.Text;
                currentGuest.passward = updatePassword.Password;
                UserGuest.Visibility = Visibility.Visible;
                UpdatingGuestGrid.Visibility = Visibility.Collapsed;
                clearUpdatGuestGrid();
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
            clearHostEntery();
        }
        private void clearHostEntery()
        {
            hostEnteryPassword.Clear();
            insertYourKey.Clear();
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

        private void AlreadyExists_Host_Click(object sender, RoutedEventArgs e)
        {
            clearHostSignUpGrid();
            HostSignUpGrid.Visibility = Visibility.Collapsed;
            HostEntranceGrid.Visibility = Visibility.Visible;
            insertYourKey.Text = currentHost.HostKey.ToString();

        }

        private void HostBranchAcountCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataContext = MyBl.getListOfBankBranches();
        }

        private void ToAddRequest_Click(object sender, RoutedEventArgs e)
        {
            EndGuestRequest.Visibility = Visibility.Collapsed;
            BackToFersonalDedails.Visibility = Visibility.Collapsed;
            updateRequest.Visibility = Visibility.Collapsed;
            addRequest.Visibility = Visibility.Visible;
            backToUserGuest.Visibility = Visibility.Visible;
            UserGuest.Visibility = Visibility.Collapsed;
            NewRequestGrid.Visibility = Visibility.Visible;
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            
            if (allDetailsValid())
            {
                currentRequest = new GuestRequest();
                currentRequest.guest = currentGuest;
                try
                {
                    currentRequest.Adults = int.Parse(insertNumOFAdults.Text);
                    if (currentRequest.Adults < 1)
                        throw new Exception("kkkkkk");
                        
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                try
                {
                    currentRequest.Area = (Areas)insertArea.SelectedIndex;
                    currentRequest.Type = (Types)insertTheType.SelectedIndex;
                    currentRequest.EntryDate = insertRequestDates.SelectedDates.First();
                    currentRequest.ReleaseDate = insertRequestDates.SelectedDates.Last();
                    if (poolN.IsChecked == true)
                        currentRequest.Pool = Options.NotIntresting;
                    else if (poolY.IsChecked == true)
                        currentRequest.Pool = Options.Must;
                    else
                        currentRequest.Pool = Options.Possible;
                    if (jacuzzN.IsChecked == true)
                        currentRequest.Jacuzz = Options.NotIntresting;
                    else if (jacuzzY.IsChecked == true)
                        currentRequest.Jacuzz = Options.Must;
                    else
                        currentRequest.Jacuzz = Options.Possible;
                    if (gardenN.IsChecked == true)
                        currentRequest.Garden = Options.NotIntresting;
                    else if (gardenY.IsChecked == true)
                        currentRequest.Garden = Options.Must;
                    else
                        currentRequest.Garden = Options.Possible;
                    if (attractionsN.IsChecked == true)
                        currentRequest.ChildrensAttractions = Options.NotIntresting;
                    else if (attractionsY.IsChecked == true)
                        currentRequest.ChildrensAttractions = Options.Must;
                    else
                        currentRequest.ChildrensAttractions = Options.Possible;
                }
                catch(Exception)
                {
                    MessageBox.Show("אחד מהפרטים חסרים או שגויים", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                try
                {
                    MyBl.AddRequest(currentRequest);
                }
                catch (Exception)
                {

                }
                NewRequestGrid.Visibility = Visibility.Collapsed;
                UserGuest.Visibility = Visibility.Visible;
                UserGuestRequest.ItemsSource = MyBl.GetRequestsOfGuest(currentGuest.MailAddress);
                //welcomeGuest.Text = currentGuest.PrivateName;
                //guestpersonalDedails.Text = currentGuest.ToString();
                //requestDetails.Text = currentRequest.ToString();

            }


            clearNewRequestGrid();
        }

        private void BackToUserGuest_Click_1(object sender, RoutedEventArgs e)
        {
            clearNewRequestGrid();
            UserGuest.Visibility = Visibility.Visible;
            NewRequestGrid.Visibility = Visibility.Collapsed;
        }

        private void UpdateRequest_Click(object sender, RoutedEventArgs e)
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
                try
                {
                    currentRequest.Area = (Areas)insertArea.SelectedIndex;
                    currentRequest.Type = (Types)insertTheType.SelectedIndex;
                    currentRequest.EntryDate = insertRequestDates.SelectedDates.First();
                    currentRequest.ReleaseDate = insertRequestDates.SelectedDates.Last();
                    if (poolN.IsChecked == true)
                        currentRequest.Pool = Options.NotIntresting;
                    else if (poolY.IsChecked == true)
                        currentRequest.Pool = Options.Must;
                    else
                        currentRequest.Pool = Options.Possible;
                    if (jacuzzN.IsChecked == true)
                        currentRequest.Jacuzz = Options.NotIntresting;
                    else if (jacuzzY.IsChecked == true)
                        currentRequest.Jacuzz = Options.Must;
                    else
                        currentRequest.Jacuzz = Options.Possible;
                    if (gardenN.IsChecked == true)
                        currentRequest.Garden = Options.NotIntresting;
                    else if (gardenY.IsChecked == true)
                        currentRequest.Garden = Options.Must;
                    else
                        currentRequest.Garden = Options.Possible;
                    if (attractionsN.IsChecked == true)
                        currentRequest.ChildrensAttractions = Options.NotIntresting;
                    else if (attractionsY.IsChecked == true)
                        currentRequest.ChildrensAttractions = Options.Must;
                    else
                        currentRequest.ChildrensAttractions = Options.Possible;
                }
                catch (Exception)
                {
                    MessageBox.Show("אחד מהפרטים חסרים או שגויים", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                try
                {
                    MyBl.UpdateRequest(currentRequest);
                    NewRequestGrid.Visibility = Visibility.Collapsed;
                    UserGuest.Visibility = Visibility.Visible;
                    UserGuestRequest.ItemsSource = MyBl.GetRequestsOfGuest(currentGuest.MailAddress);
                    //welcomeGuest.Text = currentGuest.PrivateName;
                    //guestpersonalDedails.Text = currentGuest.ToString();
                    //requestDetails.Text = currentRequest.ToString();
                    clearNewRequestGrid();
                    UserGuest.Visibility = Visibility.Visible;
                    NewRequestGrid.Visibility = Visibility.Collapsed;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }
            

        }
    }
    
    

}

