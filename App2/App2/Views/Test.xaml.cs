using App2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        private List<Monkey> _listProd;
        private const string monkeyUrl = "https://montemagno.com/monkeys.json";
        private readonly HttpClient httpClient = new HttpClient();

        public ObservableCollection<Monkey> Monkeys { get; set; } = new ObservableCollection<Monkey>();
        public Test()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            LoadData();
        }
        protected async void LoadData()
        {
            var monkeyJson = await httpClient.GetStringAsync(monkeyUrl);
            var monkeys = JsonConvert.DeserializeObject<Monkey[]>(monkeyJson);


            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }

            _listProd = Monkeys.ToList();

            _data.ItemsSource = _listProd;

            Device.StartTimer(TimeSpan.FromSeconds(4), (Func<bool>)(() =>
            {
                _data.Position = (_data.Position + 2) % _listProd.Count;
                return true;
            }));
        }
    }
}