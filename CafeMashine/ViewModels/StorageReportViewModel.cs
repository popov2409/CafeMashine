using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CafeMashine.Models;

namespace CafeMashine.ViewModels
{
    //DateTime now = DateTime.Now;
    //DateTime first = new DateTime(now.Year, now.Month, 1);
    //DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1);

    public class StorageReportViewModel:BaseViewModel
    {
        private List<User> _users;
        private List<IngredientCount> _ingredientCounts;
        private List<Ingredient> _ingredients;
        private List<Record> _records;
        private List<Avtomat> _avtomats;
        private List<UserAvtomat> _userAvtomats;

        private Avtomat _selectedAvtomat;
        private User _selectedUser;
        private DateTime _startDate;
        private DateTime _endDate;
        private Grid _resGrid;
        private int _typeReport;


        public StorageReportViewModel(int typeReport)
        {
            LoadList();
            _resGrid = new Grid();
            _typeReport = typeReport;
        }

        async void LoadList()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).Where(c => !c.Name.Equals("Storage")).ToList();
            _ingredientCounts = (await IngredientCountDataStore.GetItemsAsync(true))
                .OrderBy(c => DateTime.Parse(c.Date)).ToList();
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
            _records = (await RecordDataStore.GetItemsAsync(true)).ToList();
            _avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
            _userAvtomats = (await UserAvtomatDataStore.GetItemsAsync(true)).ToList();
        }

        public List<User> Users => _users;

        private List<Avtomat> _userAvtomates;
        public List<Avtomat> Avtomats => _userAvtomates;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                _userAvtomates=new List<Avtomat>();
                foreach (UserAvtomat userAvtomat in _userAvtomats.Where(c=>c.User==_selectedUser.Id))
                {
                    _userAvtomates.Add(_avtomats.First(c => c.Id == userAvtomat.Avtomat));
                }
                
                OnPropertyChanged("UserName");
                OnPropertyChanged("ReportGrid");
                OnPropertyChanged("Avtomats");
            }
        }

        public Avtomat SelectedAvtomat
        {
            get => _selectedAvtomat;
            set
            {
                _selectedAvtomat = value;
                OnPropertyChanged("ReportGrid");
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value; 
                OnPropertyChanged("ReportGrid");
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged("ReportGrid");
            }
        }

        private List<string> dates;
        private List<StorageReportStruct> structs;

        public string UserName => _selectedUser == null ? "" : _selectedUser.Name;

        private void CreateReport()
        {
            dates = new List<string>();

            if (_selectedUser == null || _startDate == null || _endDate == null)
            {
                return;
            }
            else
            {
                List<Avtomat> avtomats=new List<Avtomat>();
                object recs = new object();
                switch (_typeReport)
                {
                    case 0:
                    {
                        recs = _ingredientCounts.Where(c =>
                            c.User == _selectedUser.Id && DateTime.Parse(c.Date) >= _startDate &&
                            DateTime.Parse(c.Date) <= _endDate).ToList();
                        if ((recs as List<IngredientCount>).Count == 0) return;
                        dates = (recs as List<IngredientCount>).Select(c => c.Date).Distinct().ToList();
                        break;
                    }
                    case 1:
                    {
                        recs = _records.Where(c =>
                            c.User == _selectedUser.Id && DateTime.Parse(c.Date) >= _startDate &&
                            DateTime.Parse(c.Date) <= _endDate).ToList();
                        if ((recs as List<Record>).Count == 0) return;
                        dates = (recs as List<Record>).Select(c => c.Avtomat).Distinct().ToList();
                        avtomats = dates.Select(date => _avtomats.First(c => c.Id == date)).ToList();
                        dates = avtomats.OrderBy(c => c.Value).Select(c => c.Id).ToList();
                            break;
                    }
                    case 2:
                    {
                        recs = _records.Where(c =>
                            c.User == _selectedUser.Id && DateTime.Parse(c.Date) >= _startDate &&
                            DateTime.Parse(c.Date) <= _endDate).ToList();
                        if ((recs as List<Record>).Count == 0) return;
                        dates = (recs as List<Record>).Select(c => c.Date).Distinct().ToList();
                        break;
                    }
                    case 3:
                    {
                        if (_selectedAvtomat == null) return;
                        recs = _records.Where(c =>
                            c.Avtomat == _selectedAvtomat.Id && c.User == _selectedUser.Id &&
                            DateTime.Parse(c.Date) >= _startDate &&
                            DateTime.Parse(c.Date) <= _endDate).ToList();
                        if ((recs as List<Record>).Count == 0) return;
                        dates = (recs as List<Record>).Select(c => c.Date).Distinct().ToList();
                        
                        break;
                    }
                }

                structs = new List<StorageReportStruct>();
                foreach (Ingredient ingredient in _ingredients)
                {
                    StorageReportStruct st = new StorageReportStruct()
                        {Ingredient = ingredient.Value, Values = new List<int>()};
                    foreach (string date in dates)
                    {
                        switch (_typeReport)
                        {
                            case 0:
                            {
                                st.Values.Add((recs as List<IngredientCount>)
                                    .Where(c => c.Date == date && c.Ingredient == ingredient.Id)
                                    .Sum(c => c.Count));
                                break;
                            }
                            case 1:
                            {
                                st.Values.Add((recs as List<Record>)
                                    .Where(c => c.Avtomat == date && c.Ingredient == ingredient.Id)
                                    .Sum(c => c.Count));
                                break;
                            }
                            case 2:
                            {
                                st.Values.Add((recs as List<Record>)
                                    .Where(c => c.Date == date && c.Ingredient == ingredient.Id)
                                    .Sum(c => c.Count));
                                break;
                            }
                            case 3:
                            {
                                goto case 2;
                            }
                        }

                    }

                    structs.Add(st);
                }

                if (_typeReport == 1)
                {
                    dates = avtomats.OrderBy(c => c.Value).Select(c => c.Value).ToList();
                }


            }
        }

        public Grid ReportGrid
        {
            get
            {
                _resGrid.Children.Clear();
                _resGrid.ColumnDefinitions.Clear();
                _resGrid.RowDefinitions.Clear();
                CreateReport();
                if(dates==null||dates.Count==0) return new Grid();
                _resGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                _resGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                //№
                _resGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(60)});
                TextBox tb0_0 = new TextBox()
                    {Text = "№", Style = (Style) App.Current.Resources["TableHeadersTextBoxStyle"]};
                Grid.SetRow(tb0_0,0);
                Grid.SetColumn(tb0_0,0);
                Grid.SetRowSpan(tb0_0,2);
                _resGrid.Children.Add(tb0_0);
                //Ингредиент
                _resGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto, MinWidth = 100});
                TextBox tb0_1 = new TextBox()
                    {Text = "Ингредиенты", Style = (Style) App.Current.Resources["TableHeadersTextBoxStyle"]};
                Grid.SetRow(tb0_1, 0);
                Grid.SetColumn(tb0_1, 1);
                Grid.SetRowSpan(tb0_1, 2);
                _resGrid.Children.Add(tb0_1);
                //Итого
                _resGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                TextBox tb0_2 = new TextBox()
                    {Text = "Итого", Style = (Style) App.Current.Resources["SummarilyTextBoxStyle"]};
                Grid.SetRow(tb0_2, 0);
                Grid.SetColumn(tb0_2, 2);
                Grid.SetRowSpan(tb0_2, 2);
                _resGrid.Children.Add(tb0_2);
                

                int column = 3;
                foreach (string date in dates)
                {
                    _resGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                    TextBox tbD = new TextBox()
                        { Text = _typeReport!=1?date.Substring(0,5):date, Style = (Style)App.Current.Resources["TableHeadersTextBoxStyle"] };
                    Grid.SetRow(tbD, 1);
                    Grid.SetColumn(tbD, column);
                    _resGrid.Children.Add(tbD);
                    column++;
                }

                //выдано
                TextBox tb0_3 = new TextBox()
                    { Style = (Style)App.Current.Resources["TableHeadersTextBoxStyle"] };
                switch (_typeReport)
                {
                    case 0:
                    {
                        tb0_3.Text = "Выдано";
                        break;
                    }
                    case 1:
                    {
                        tb0_3.Text = "Автомат";
                        break;
                    }
                    case 2:
                    {
                        tb0_3.Text = "Число";
                        break;
                    }
                    case 3:
                    {
                        tb0_3.Text = _selectedAvtomat.Value;
                        break;
                    }
                }
                Grid.SetRow(tb0_3, 0);
                Grid.SetColumn(tb0_3, 3);
                Grid.SetColumnSpan(tb0_3, dates.Count);
                _resGrid.Children.Add(tb0_3);

                int row = 1;
                
                foreach (StorageReportStruct reportStruct in structs)
                {
                    _resGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    column = 0;
                    TextBox numBox = new TextBox()
                        { Text = row.ToString(), Style = (Style)App.Current.Resources["NumberTextBoxStyle"] };
                    Grid.SetRow(numBox, row+1);
                    Grid.SetColumn(numBox, column);
                    _resGrid.Children.Add(numBox);
                    column++;

                    TextBox ingBox = new TextBox()
                        { Text = reportStruct.Ingredient, Style = (Style)App.Current.Resources["IngredientValueTextBoxStyle"] };
                    Grid.SetRow(ingBox, row + 1);
                    Grid.SetColumn(ingBox, column);
                    _resGrid.Children.Add(ingBox);
                    column++;

                    TextBox sumBox= new TextBox()
                        { Text = reportStruct.Values.Sum().ToString(), Style = (Style)App.Current.Resources["SummarilyTextBoxStyle"] };
                    Grid.SetRow(sumBox, row+1);
                    Grid.SetColumn(sumBox, column);
                    _resGrid.Children.Add(sumBox);
                    column++;

                    foreach (int value in reportStruct.Values)
                    {
                        TextBox valBox = new TextBox()
                            { Text = value.ToString(), Style = (Style)App.Current.Resources["TableHeadersTextBoxStyle"] };
                        Grid.SetRow(valBox, row + 1);
                        Grid.SetColumn(valBox, column);
                        _resGrid.Children.Add(valBox);
                        column++;
                    }
                    row++;
                }

                return _resGrid;
            }
        }
    }

    public struct StorageReportStruct
    {
        public string Ingredient { get; set; }
        public List<int> Values { get; set; }

    }

    
}
