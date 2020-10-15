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
        private User _selectedUser;
        private DateTime _startDate;
        private DateTime _endDate;
        private Grid _resGrid;


        public StorageReportViewModel()
        {
            LoadList();
            _resGrid = new Grid();
        }

        async void LoadList()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).Where(c => !c.Name.Equals("Storage")).ToList();
            _ingredientCounts = (await IngredientCountDataStore.GetItemsAsync(true))
                .OrderBy(c => DateTime.Parse(c.Date)).ToList();
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
        }

        public List<User> Users => _users;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value; 
                OnPropertyChanged("UserName");
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

        public string UserName => _selectedUser==null?"":_selectedUser.Name;

        private void CreateReport()
        {
            dates = new List<string>();

            if (_selectedUser == null || _startDate == null || _endDate == null)
            {
                return;
            }
            else
            {
                var recs = _ingredientCounts.Where(c =>
                    c.User == _selectedUser.Id && DateTime.Parse(c.Date) >= _startDate &&
                    DateTime.Parse(c.Date) <= _endDate).ToList();
                if (recs.Count == 0)
                {
                    return;
                }
                else
                {
                    dates = recs.Select(c => c.Date).Distinct().ToList();
                    structs = new List<StorageReportStruct>();
                    foreach (Ingredient ingredient in _ingredients)
                    {
                        StorageReportStruct st = new StorageReportStruct()
                            {Ingredient = ingredient.Value, Values = new List<int>()};
                        foreach (string date in dates)
                        {
                            st.Values.Add(recs.Where(c => c.Date == date && c.Ingredient == ingredient.Id)
                                .Sum(c => c.Count));
                        }

                        structs.Add(st);
                    }
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
                        { Text = date.Substring(0,5), Style = (Style)App.Current.Resources["TableHeadersTextBoxStyle"] };
                    Grid.SetRow(tbD, 1);
                    Grid.SetColumn(tbD, column);
                    _resGrid.Children.Add(tbD);
                    column++;
                }

                //выдано
                TextBox tb0_3 = new TextBox()
                    { Text = "Выдано", Style = (Style)App.Current.Resources["TableHeadersTextBoxStyle"] };
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
